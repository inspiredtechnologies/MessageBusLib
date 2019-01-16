using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SystemConfiguration
{
  [Serializable]
  public class NetworkEqpt
  {
    [NonSerialized]
    private Ping _PingService = null;

    // -------------------------------------------------------------------

    public string Name { get; set; }
    public string IpAddress { get; set; }
    public bool Enabled { get; set; }

    [Browsable(false)]
    public bool IsOnline { get; set; }
    [Browsable(false)]
    public HealthState Health;

    [XmlIgnore]
    [Browsable(false)]
    public int ConsecutiveFaultCount { get; set; }

    // -------------------------------------------------------------------

    #region Public Methods

    public HealthState CheckHealthStatus()
    {
      try
      {
        IsOnline = CheckReachability(IpAddress);
        this.Health = IsOnline ? HealthState.Normal : HealthState.Fault;
        return this.Health;
      }
      catch
      {
        throw;
      }
    }

    public void Copy(NetworkEqpt source)
    {
      this.Health = source.Health;
      this.IsOnline = source.IsOnline;
    }

    public NetworkEqpt Clone()
    {
      return (NetworkEqpt)this.MemberwiseClone();
    }

    public override bool Equals(Object obj)
    {
      // Check for null values and compare run-time types.
      if (obj == null || GetType() != obj.GetType())
        return false;

      NetworkEqpt eqpt = (NetworkEqpt)obj;
      return (eqpt.Name.Equals(this.Name, StringComparison.InvariantCulture) &&
        eqpt.IpAddress.Equals(this.IpAddress, StringComparison.InvariantCulture));
    }

    public override int GetHashCode()
    {
      return this.Name.GetHashCode() << 8 + this.IpAddress.GetHashCode();
    }

    #endregion

    // -------------------------------------------------------------------
 
    public NetworkEqpt()
    {
      _PingService = new Ping();
    }

    private bool CheckReachability(string ipAddress)
    {
      if (string.IsNullOrEmpty(ipAddress))
        throw new ArgumentNullException();
      if (_PingService == null)
        throw new InvalidOperationException("Network Ping service unavailable");

      try
      {
        PingReply response = _PingService.Send(ipAddress);
        return (response.Status == IPStatus.Success);
      }
      catch
      {
        throw;
      }
    }

  }
}
