using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBusLib
{
  public class ConnectionExceptionEventArg : EventArgs
  {
    public string ErrorMessage { get; set; }

    public ConnectionExceptionEventArg(string errorMessage)
    {
      ErrorMessage = errorMessage;
    }
  }
}
