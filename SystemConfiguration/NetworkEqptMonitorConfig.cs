using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemConfiguration
{
  public static class NetworkEqptMonitorConfig
  {
    public const string EquipmentStatusUpdateTopicName = "EquipmentStatusUpdate";
    public const string EquipmentConfigurationTopicName = "EquipmentConfiguration";
    public const string ConfigurationUpdateRequestTopicName = "ConfigurationUpdateRequest";
    public const string NmsHeartBeatTopicName = "NmsHeartBeat";

    static SystemConfiguration _AppSettings = new SystemConfiguration(@"NetworkEqptMonitor");
    static string _NmsServerIpAddress = "127.0.0.1";
    static string _MessageBusBrokerUri;
    static int _EquipmentHealthCheckInterval;
    static Collection<NetworkEqpt> _Equipment;

    // ---------------------------------------------------------------------

    #region Public Properties

    public static string NmsServerIpAddress
    {
      get { return _NmsServerIpAddress; }
    }

    public static string MessageBusBrokerUri
    {
      get { return _MessageBusBrokerUri; }
    }

    public static int EquipmentHealthCheckInterval
    {
      get { return _EquipmentHealthCheckInterval; }
    }

    public static Collection<NetworkEqpt> Equipment
    {
      get { return _Equipment; }
      set { _Equipment = value; }
    }

    #endregion

        // ---------------------------------------------------------------------

    static NetworkEqptMonitorConfig()
    {
      LoadApplicationSettings();
    }

    static bool LoadApplicationSettings()
    {
      try
      {
        if (_AppSettings == null)
          throw new InvalidOperationException();

        if (!_AppSettings.Load())
          return false;
        else
        {
          if (!string.IsNullOrEmpty(_AppSettings.MessageBusBrokerUri))
          {
            _MessageBusBrokerUri = _AppSettings.MessageBusBrokerUri;
          }
          _Equipment = _AppSettings.Equipment;
          _NmsServerIpAddress = _AppSettings.NmsServerIpAddress;
          _EquipmentHealthCheckInterval = _AppSettings.EquipmentHealthCheckInterval;

          return true;
        }
      }
      catch
      {
        throw;
      }
    }

    public static bool SaveEquipmentSettings()
    {
      try
      {
        if (_AppSettings == null)
          throw new InvalidOperationException();

        _AppSettings.Equipment = _Equipment;
        return _AppSettings.Save();
      }
      catch
      {
        throw;
      }
    }
  }
}
