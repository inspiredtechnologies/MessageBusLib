using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SystemConfiguration;

namespace NetworkEqptMonitorServer
{
  class EquipmentHealthMonitor : IDisposable
  {
    private const int DefaultInterval = 15;     // in seconds
    private const int StartCheckDelay = 3000;   // in milliseconds

    private Timer _HealthCheckTimer = null;
    private int _HealthCheckInterval = DefaultInterval;
    private bool _InOperation = false;

    public Collection<NetworkEqpt> Equipment { get; set; }

    // -----------------------------------------------------------------------

    public event EventHandler<EquipmentStatusUpdatedEventArg> EquipmentStatusUpdated;

    // -----------------------------------------------------------------------

    /// <summary>
    /// Get or set the equipment health checking interval in seconds.
    /// </summary>
    public int HealthCheckInterval
    {
      get { return _HealthCheckInterval; }
      set
      {
        if (value < 5)
          throw new ArgumentOutOfRangeException("Interval cannot be less than 5 seconds");

        _HealthCheckInterval = value;
        if (_HealthCheckTimer != null)
        {
          _HealthCheckTimer.Change(StartCheckDelay, value * 1000);
        }
      }
    }

    // -------------------------------------------------------------------------

    #region Public Methods

    public void Start()
    {
      try
      {
        _InOperation = false;

        if (_HealthCheckTimer == null)
        {
          _HealthCheckTimer = new Timer(DoHealthCheck, null, StartCheckDelay, HealthCheckInterval * 1000);
        }
        else
        {
          _HealthCheckTimer.Change(StartCheckDelay, _HealthCheckInterval * 1000);
        }
      }
      catch
      {
        throw;
      }
    }

    public void Stop()
    {
      try
      {
        if (_HealthCheckTimer == null)
          throw new InvalidOperationException();
        else
        {
          _HealthCheckTimer.Change(Timeout.Infinite, Timeout.Infinite);
          _InOperation = false;
        }
      }
      catch
      {
        throw;
      }
    }

    #endregion

    // -------------------------------------------------------------------------

    public EquipmentHealthMonitor()
    {

    }

    ~EquipmentHealthMonitor()
    {
      Dispose(false);
    }

    //Implement IDisposable.
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        // Free other state (managed objects).
        if (_HealthCheckTimer != null)
        {
          _HealthCheckTimer.Dispose();
          _HealthCheckTimer = null;
        }
      }
      // Free your own state (unmanaged objects).
      // Set large fields to null.
    }

    private void DoHealthCheck(object o)
    {
      if (_InOperation)
        return;
      else
        _InOperation = true;

      try
      {
        if (Equipment != null)
        {
          foreach (NetworkEqpt eqpt in Equipment)
          {
            if (eqpt.Enabled)
            {
              ThreadPool.QueueUserWorkItem(delegate
              {
                CheckEquipmentHealth(eqpt);
              });
              Thread.Sleep(20);
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
        _InOperation = false;
      }
    }

    private HealthState CheckEquipmentHealth(NetworkEqpt eqpt)
    {
      if (eqpt == null)
        return HealthState.Unknown;

      try
      {
        bool allowRetry = true;
        HealthState eqptHealth = HealthState.Unknown;

        while (true)
        {
          eqptHealth = eqpt.CheckHealthStatus();
          if (eqptHealth != HealthState.Normal)
          {
            if (eqptHealth == HealthState.Fault && allowRetry)
            {
              allowRetry = false;
              Thread.Sleep(3000);   // (1000);
              continue;
            }
            eqpt.ConsecutiveFaultCount++;
            break;
          }
          else
          {
            eqpt.ConsecutiveFaultCount = 0;
            break;
          }
        }

        OnEquipmentStatusUpdated(new EquipmentStatusUpdatedEventArg(eqpt));
        return eqptHealth;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      return HealthState.Unknown;
    }

    private int GetHealthIndex(HealthState health)
    {
      switch (health)
      {
        case HealthState.Normal:
          return 0;

        case HealthState.Degraded:
          return 1;

        case HealthState.Fault:
          return 2;

        default:
          return 0;
      }
    }

    protected void OnEquipmentStatusUpdated(EquipmentStatusUpdatedEventArg e)
    {
      EventHandler<EquipmentStatusUpdatedEventArg> handler = EquipmentStatusUpdated;
      if (handler != null)
        handler(this, e);
    }

  }
}
