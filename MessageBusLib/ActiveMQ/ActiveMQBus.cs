using System;
using System.Collections.Generic;
using System.Text;

using Apache.NMS.ActiveMQ;
using Apache.NMS;
using MessageBusLib.ActiveMQ;

namespace MessageBusLib
{
  class ActiveMQBus : IMessageBus
  {
    private readonly IConnectionFactory _ConnectionFactory;
    private TopicConnection _Connection = null; 

    // --------------------------------------------------------

    #region Public Properties
    
    public string BrokerUri { get; set; }
    public string ClientId { get; set; }

    #endregion

    // --------------------------------------------------------

    #region Public Methods

    public bool Connect()
    {
      try
      {
        _Connection = new TopicConnection(_ConnectionFactory, ClientId);
        _Connection.OnMessageReceived += Subscriber_OnMessageReceived;
        _Connection.OnConnectionException += Connection_OnException;
        return true;
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
        if (_Connection != null)
        {
          _Connection.OnMessageReceived -= Subscriber_OnMessageReceived;
          _Connection.Dispose();
          _Connection = null;
        }
        return true;
      }
      catch
      {
        throw;
      }
    }

    public bool Publish(Topic topic)
    {
      if (topic == null || string.IsNullOrEmpty(topic.Name))
        throw new ArgumentNullException();

      try
      {
        _Connection.Publish(topic);
        return true;
      }
      catch
      {
      }
      return false;
    }

    public bool UnPublish(Topic topic)
    {
      if (topic == null || string.IsNullOrEmpty(topic.Name))
        throw new ArgumentNullException();

      try
      {
        _Connection.UnPublish(topic);
        return true;
      }
      catch
      {
      }
      return false;
    }

    public bool Subscribe(Topic topic, bool getLatestTopicValue = false)
    {
      if (topic == null || string.IsNullOrEmpty(topic.Name))
        throw new ArgumentNullException();

      try
      {
        _Connection.Subscribe(ClientId + "." + topic.Name, topic, getLatestTopicValue);
        return true;
      }
      catch
      {
      }
      return false;
    }

    public bool UnSubscribe(Topic topic)
    {
      if (topic == null || string.IsNullOrEmpty(topic.Name))
        throw new ArgumentNullException();

      try
      {
        _Connection.UnSubscribe(topic);
        return true;
      }
      catch
      {
      }
      return false;
    }

    public bool PublishUpdate(Topic topic)
    {
      try
      {
        return _Connection.PublishUpdate(topic);
      }
      catch (IOException exi)
      {
        if (exi.Message.StartsWith("Failover oneway timed out", StringComparison.InvariantCultureIgnoreCase))
        {
          OnConnectionExceptionEvent(new ConnectionExceptionEventArg(exi.Message));
          return false;
        }
        else
        {
          throw;
        }
      }
      catch
      {
        throw;
      }
    }
    
    #endregion

    // --------------------------------------------------------

    #region Public Events

    public event EventHandler<TopicUpdatedEventArg> SubscriptionUpdateEvent;
    public event EventHandler<ConnectionExceptionEventArg> ConnectionExceptionEvent;

    #endregion

    // --------------------------------------------------------

    public ActiveMQBus(string brokerUri, string clientId)
    {
      BrokerUri = brokerUri;
      ClientId = clientId;
      _ConnectionFactory = new ConnectionFactory(brokerUri);
    }

    ~ActiveMQBus()
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
        if (_Connection != null)
        {
          _Connection.Dispose();
          _Connection = null;
        }
      }
      // Free your own state (unmanaged objects).
      // Set large fields to null.
    }

    private void Subscriber_OnMessageReceived(string topicName, string message)
    {
      OnNewMessageEvent(new TopicUpdatedEventArg(new Topic(topicName, message)));
    }

    protected void OnNewMessageEvent(TopicUpdatedEventArg e)
    {
      EventHandler<TopicUpdatedEventArg> handler = SubscriptionUpdateEvent;
      if (handler != null)
      {
        handler(this, e);
      }
    }

    private void Connection_OnException(string eventMessage)
    {
      OnConnectionExceptionEvent(new ConnectionExceptionEventArg(eventMessage));
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
