using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBusLib
{
  public interface IMessageBus : IDisposable
  {
    string BrokerUri { get; set; }
    string ClientId { get; set; }

    bool Connect();
    bool Disconnect();
    bool Publish(Topic topic);
    bool UnPublish(Topic topic);
    bool Subscribe(Topic topic, bool getLatestTopicValue = false);
    bool UnSubscribe(Topic topic);
    bool PublishUpdate(Topic topic);

    event EventHandler<TopicUpdatedEventArg> SubscriptionUpdateEvent;
    event EventHandler<ConnectionExceptionEventArg> ConnectionExceptionEvent;
  }
}
