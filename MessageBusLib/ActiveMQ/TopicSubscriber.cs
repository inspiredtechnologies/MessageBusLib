using System;
using System.Collections.Generic;
using System.Text;

using Apache.NMS.ActiveMQ;
using Apache.NMS;

namespace MessageBusLib.ActiveMQ
{
  public delegate void MessageReceivedDelegate(string topicName, string message);

  class TopicSubscriber : IDisposable
  {
    private readonly IMessageConsumer _Consumer;
    private bool _IsDisposed = false;

    public string TopicName { get; set; }

    public event MessageReceivedDelegate OnMessageReceived;

    public TopicSubscriber(IMessageConsumer consumer)
    {
      _Consumer = consumer;
      _Consumer.Listener += new MessageListener(OnMessage);
    }

    public void OnMessage(IMessage message)
    {
      ITextMessage textMessage = message as ITextMessage;
      if (OnMessageReceived != null)
      {
        OnMessageReceived(TopicName, textMessage.Text);
      }
    }

    #region IDisposable Members

    public void Dispose()
    {
      if (!_IsDisposed)
      {
        _Consumer.Dispose();
        _IsDisposed = true;
      }
    }

    #endregion
  }
}
