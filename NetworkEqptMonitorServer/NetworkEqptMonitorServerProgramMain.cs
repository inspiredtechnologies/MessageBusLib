using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using MessageBusLib;
using SystemConfiguration;

namespace NetworkEqptMonitorServer
{
  class NetworkEqptMonitorServerProgramMain
  {
    static string _MessageBusBrokerUri = NetworkEqptMonitorConfig.MessageBusBrokerUri;
    static TopicConnectionFactory _MessageBus = null;
    static string _MessageBusClientId = string.Empty;

    static EquipmentHealthMonitor _HealthMonitoringService = null;

    static Topic _EquipmentStatusUpdateTopic = null;
    static Topic _EquipmentConfigurationTopic = null;
    static Topic _ConfigurationUpdateRequestTopic = null;
    static Topic _ServerHeartBeatTopic = null;

    static bool _Terminate;
    static DateTime _LastReportHealthTime;
    static object _SyncObject = new object();

    static void Main(string[] args)
    {
      Mutex appMutex = null;
      bool createdNew = false;

      try
      {
        // Only allow one instance to be running.
        appMutex = new Mutex(true,
          "NetworkEqptMonitor-F2322708-F172-46fd-9BF8-3D5AE30AF6FE", out createdNew);
        if (createdNew)
        {
          System.AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;

          // Allow the process instance to start.
          if (!ConnectToMessageBus())
          {
            Console.WriteLine("Error: Could not connect to the Message Bus");
            Environment.Exit(1);
          }
          else
          {
            if (!PublishSubscribeTopics())
            {
              Console.WriteLine("Error: Topics subscription failure");
              Environment.Exit(1);
            }
          }

          AnnounceAssetsConfigurationUpdate();

          _HealthMonitoringService = new EquipmentHealthMonitor();
          _HealthMonitoringService.Equipment = NetworkEqptMonitorConfig.Equipment;
          _HealthMonitoringService.EquipmentStatusUpdated += HealthMonitoringService_EquipmentStatusUpdated;
          _HealthMonitoringService.HealthCheckInterval = NetworkEqptMonitorConfig.EquipmentHealthCheckInterval;
          _HealthMonitoringService.Start();

          _Terminate = false;
          _LastReportHealthTime = DateTime.Now;
          Console.CancelKeyPress += delegate
          {
            _Terminate = true;
            CleanUp();
          };

          // Allow the process instance to start.
          Console.WriteLine("Network Equipment Monitor starting up...");
          if (_MessageBus != null)
          {
            _ServerHeartBeatTopic.Value = HealthState.Normal.ToString();
            _MessageBus.PublishUpdate(_ServerHeartBeatTopic);
          }

          while (!_Terminate)
          {
            Thread.Sleep(1000);

            if (_MessageBus != null)
            {
              _ServerHeartBeatTopic.Value = HealthState.Normal.ToString();
              _MessageBus.PublishUpdate(_ServerHeartBeatTopic);
            }
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
      finally
      {
        GC.KeepAlive(appMutex);
      }
    }

    private static void InitializeEqptHealthStates()
    {
      if (NetworkEqptMonitorConfig.Equipment != null)
      {
        foreach (NetworkEqpt eqpt in NetworkEqptMonitorConfig.Equipment)
        {
          eqpt.Health = HealthState.Unknown;
        }
      }
    }

    private static void AnnounceAssetsConfigurationUpdate()
    {
      try
      {
        InitializeEqptHealthStates();

        if (NetworkEqptMonitorConfig.Equipment != null)
        {
          _EquipmentConfigurationTopic.Value = Helper.SerializeObject(NetworkEqptMonitorConfig.Equipment);
          if (_MessageBus.PublishUpdate(_EquipmentConfigurationTopic))
          {
            Console.WriteLine("Equipment configuration broadcasted");
          }
        }
      }
      catch
      {
        throw;
      }
    }

    static void CleanUp()
    {
      Console.WriteLine("Process cleaning up...");
      if (_MessageBus != null)
      {
        _ServerHeartBeatTopic.Value = HealthState.Fault.ToString();
        _MessageBus.PublishUpdate(_ServerHeartBeatTopic);
      }

      if (_HealthMonitoringService != null)
      {
        _HealthMonitoringService.Stop();
        _HealthMonitoringService.EquipmentStatusUpdated -= HealthMonitoringService_EquipmentStatusUpdated;
        _HealthMonitoringService = null;
      }
      if (_MessageBus != null)
      {
        _MessageBus.TopicUpdatedEvent -= MessageBus_TopicUpdatedEvent;
        _MessageBus.ConnectionExceptionEvent -= MessageBus_ConnectionExceptionEvent;
        UnpublishUnsubscribeTopics();
        _MessageBus.Dispose();
        _MessageBus = null;
      }
    }

    static bool ConnectToMessageBus()
    {
      try
      {
        _MessageBusClientId = Guid.NewGuid().ToString();
        _MessageBus = new TopicConnectionFactory(_MessageBusBrokerUri, _MessageBusClientId, MessageBusType.ActiveMQ);
        _MessageBus.TopicUpdatedEvent += MessageBus_TopicUpdatedEvent;
        _MessageBus.ConnectionExceptionEvent += MessageBus_ConnectionExceptionEvent;
        return _MessageBus.Connect();
      }
      catch
      {
        throw;
      }
    }

    static bool PublishSubscribeTopics()
    {
      _EquipmentStatusUpdateTopic = new Topic(NetworkEqptMonitorConfig.EquipmentStatusUpdateTopicName, string.Empty);
      _EquipmentConfigurationTopic = new Topic(NetworkEqptMonitorConfig.EquipmentConfigurationTopicName, string.Empty);
      _ServerHeartBeatTopic = new Topic(NetworkEqptMonitorConfig.NmsHeartBeatTopicName, string.Empty);
      _ConfigurationUpdateRequestTopic = new Topic(NetworkEqptMonitorConfig.ConfigurationUpdateRequestTopicName, string.Empty);

      return _MessageBus.Publish(_EquipmentStatusUpdateTopic) && _MessageBus.Publish(_EquipmentConfigurationTopic) &&
        _MessageBus.Publish(_ServerHeartBeatTopic) && _MessageBus.Subscribe(_ConfigurationUpdateRequestTopic);
    }

    static void UnpublishUnsubscribeTopics()
    {
      _MessageBus.UnSubscribe(_ConfigurationUpdateRequestTopic);
      _MessageBus.UnPublish(_EquipmentConfigurationTopic);
      _MessageBus.UnPublish(_EquipmentStatusUpdateTopic);
      _MessageBus.UnPublish(_ServerHeartBeatTopic);
    }

    static void MessageBus_TopicUpdatedEvent(object sender, TopicUpdatedEventArg e)
    {
      ThreadPool.QueueUserWorkItem(delegate
      {
        try
        {
          if (e.UpdatedTopic.Name.Equals(NetworkEqptMonitorConfig.ConfigurationUpdateRequestTopicName, StringComparison.InvariantCulture))
          {
            HandleConfigurationConfigUpdate(e.UpdatedTopic.Value);
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.ToString());
        }
      }, null);
    }

    static void HandleConfigurationConfigUpdate(string rawValue)
    {
      try
      {
        Collection<NetworkEqpt> newEqptConfig = (Collection<NetworkEqpt>)Helper.DeserializeObject(typeof(Collection<NetworkEqpt>),
          rawValue);
        if (newEqptConfig != null)
        {
          NetworkEqptMonitorConfig.Equipment = newEqptConfig;
          if (NetworkEqptMonitorConfig.SaveEquipmentSettings())
          {
            AnnounceAssetsConfigurationUpdate();
            if (_HealthMonitoringService != null)
            {
              lock (_HealthMonitoringService)
              {
                _HealthMonitoringService.Stop();
                Thread.Sleep(5000);
                _HealthMonitoringService.Equipment = newEqptConfig;
                _HealthMonitoringService.Start();
              }
            }
          }
        }
      }
      catch
      {
        throw;
      }
    }

    static void MessageBus_ConnectionExceptionEvent(object sender, ConnectionExceptionEventArg e)
    {
      Console.WriteLine(e.ErrorMessage);
    }

    static void HealthMonitoringService_EquipmentStatusUpdated(object sender, EquipmentStatusUpdatedEventArg e)
    {
      lock (_SyncObject)
      {
        Console.Write(">>> " + e.UpdatedEquipment.Name + " health status: ");
        switch (e.UpdatedEquipment.Health)
        {
          case HealthState.Fault:
            Console.ForegroundColor = ConsoleColor.Red;
            break;

          case HealthState.Degraded:
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            break;

          case HealthState.Normal:
            Console.ForegroundColor = ConsoleColor.Green;
            break;

          default:
            Console.ForegroundColor = ConsoleColor.Gray;
            break;
        }
        Console.WriteLine(e.UpdatedEquipment.Health.ToString());
        Console.ResetColor();

        NetworkEqpt eqpt = e.UpdatedEquipment;
        if (eqpt != null)
        {
          _EquipmentStatusUpdateTopic.Value = Helper.SerializeObject(eqpt);
          _MessageBus.PublishUpdate(_EquipmentStatusUpdateTopic);
        }
      }
    }

    static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
    {
      Console.WriteLine(e.ExceptionObject.ToString());

      if (_MessageBus != null)
      {
        _ServerHeartBeatTopic.Value = HealthState.Fault.ToString();
        _MessageBus.PublishUpdate(_ServerHeartBeatTopic);
      }
      Environment.Exit(1);
    }

  }
}
