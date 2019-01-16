using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBusLib
{
  public class TopicUpdatedEventArg : EventArgs
  {
    public Topic UpdatedTopic { get; set; }

    public TopicUpdatedEventArg(Topic updatedTopic)
    {
      UpdatedTopic = updatedTopic;
    }
  }
}
