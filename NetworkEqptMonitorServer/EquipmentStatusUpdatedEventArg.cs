using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemConfiguration;

namespace NetworkEqptMonitorServer
{
  public class EquipmentStatusUpdatedEventArg : EventArgs
  {
    public NetworkEqpt UpdatedEquipment { get; set; }

    public EquipmentStatusUpdatedEventArg(NetworkEqpt equipment)
    {
      UpdatedEquipment = equipment;
    }
  }
}
