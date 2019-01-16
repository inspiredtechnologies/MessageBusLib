using System;
using System.Collections.Generic;
using System.Text;

namespace MessageBusLib
{
  public enum MessageBusType
  {
    Undefined,
    ActiveMQ
  }

  public class TopicConnectionFactory : IDisposable
  {
    private IMessageBus _MessageBus = null;

    // ------------------------------------------------------------------

    #region Public Methods

    public bool Connect()
    {
      try
      {
        return _MessageBus.Connect();
      }
      catch
      {
        throw;
      }
    }

    public bool Disconnect()
    {
      try
      {
        return _MessageBus.Disconnect();
      }
      catch
      {
        throw;
      }
    }

    public bool Publish(Topic topic)
    {
      if (topic == null || topic.Value == null)
        throw new ArgumentNullException();
      if (_MessageBus == null)
        throw new InvalidOperationException("MessageBus has not been instantiated");

      try
      {
        return _MessageBus.Publish(topic);
      }
      catch
      {
        throw;
      }
    }

    public bool UnPublish(Topic topic)
    {
      if (topic == null || topic.Value == null)
        throw new ArgumentNullException();
      if (_MessageBus == null)
        throw new InvalidOperationException("MessageBus has not been instantiated");

      try
      {
        return _MessageBus.UnPublish(topic);
      }
      catch
      {
        throw;
      }
    }

    public bool Subscribe(Topic topic, bool getLatestTopicValue = false)
    {
      if (topic == null || string.IsNullOrEmpty(topic.Name))
        throw new ArgumentNullException();
      if (_MessageBus == null)
        throw new InvalidOperationException("MessageBus has not been instantiated");

      try
      {
        return _MessageBus.Subscribe(topic, getLatestTopicValue);
      }
      catch
      {
        throw;
      }
    }

    public bool UnSubscribe(Topic topic)
    {
      if (topic == null || string.IsNullOrEmpty(topic.Name))
        throw new ArgumentNullException();
      if (_MessageBus == null)
        throw new InvalidOperationException("MessageBus has not been instantiated");

      try
      {
        return _MessageBus.UnSubscribe(topic);
      }
      catch
      {
        throw;
      }
    }

    public bool PublishUpdate(Topic topic)
    {
      if (_MessageBus == null)
        throw new InvalidOperationException("MessageBus has not been instantiated");

      try
      {
        return _MessageBus.PublishUpdate(topic);
      }
      catch
      {
        throw;
      }
    }

    #endregion

    // ------------------------------------------------------------------

    #region Public Events

    public event EventHandler<TopicUpdatedEventArg> TopicUpdatedEvent;
    public event EventHandler<ConnectionExceptionEventArg> ConnectionExceptionEvent;

    #endregion

    // ------------------------------------------------------------------

    public TopicConnectionFactory(string brokerUri, string clientId, MessageBusType messageBusType)
    {
      if (string.IsNullOrEmpty(brokerUri))
        throw new ArgumentNullException("Invalid broker uri");

      switch (messageBusType)
      {
        case MessageBusType.ActiveMQ:
          _MessageBus = new ActiveMQBus(brokerUri, clientId);
          _MessageBus.SubscriptionUpdateEvent += MessageBus_SubscriptionUpdateEvent;
          _MessageBus.ConnectionExceptionEvent += MessageBus_ConnectionExceptionEvent;
          break;

        default:
          throw new ArgumentException("Do not know how to handle message bus of the type " + messageBusType.ToString());
      }
    }

    ~TopicConnectionFactory()
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
        // Release the disc recorder items
        if (_MessageBus != null)
        {
          _MessageBus.Dispose();
          _MessageBus = null;
        }
      }
      // Free your own state (unmanaged objects).
      // Set large fields to null.
    }

    private void MessageBus_SubscriptionUpdateEvent(object sender, TopicUpdatedEventArg e)
    {
      OnTopicUpdatedEvent(e);
    }

    private void MessageBus_ConnectionExceptionEvent(object sender, ConnectionExceptionEventArg e)
    {
      OnConnectionExceptionEvent(e);
    }

    protected void OnTopicUpdatedEvent(TopicUpdatedEventArg e)
    {
      EventHandler<TopicUpdatedEventArg> handler = TopicUpdatedEvent;
      if (handler != null)
      {
        handler(this, e);
      }
    }

    protected void OnConnectionExceptionEvent(ConnectionExceptionEventArg e)
    {
      EventHandler<ConnectionExceptionEventArg> handler = ConnectionExceptionEvent;
      if (handler != null)
      {
        handler(this, e);
      }
    }

  }
}
