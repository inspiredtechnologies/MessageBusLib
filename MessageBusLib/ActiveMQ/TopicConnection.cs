using System;
using System.Collections.Generic;
using System.Text;

using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using Apache.NMS;

namespace MessageBusLib.ActiveMQ
{
  class TopicConnection : IDisposable
  {
    private readonly IConnection _Connection;
    private readonly ISession _Session;
    private bool _IsDisposed = false;
    private string _LastTopicValue = string.Empty;

    private List<TopicPublisher> _Publishers = new List<TopicPublisher>();
    private List<TopicSubscriber> _Subscribers = new List<TopicSubscriber>();

    // --------------------------------------------------------------------------------

    #region Public Methods

    public void Publish(Topic topic)
    {
      try
      {
        ITopic newTopic = new ActiveMQTopic(topic.Name);
        IMessageProducer producer = _Session.CreateProducer(newTopic);
        TopicPublisher newPublisher = new TopicPublisher(producer);
        newPublisher.TopicName = topic.Name;
        _Publishers.Add(newPublisher);
      }
      catch
      {
        throw;
      }
    }

    public void UnPublish(Topic topic)
    {
      if (topic == null || string.IsNullOrEmpty(topic.Name))
        throw new ArgumentNullException();

      TopicPublisher publisherToBeRemoved = null;

      try
      {
        foreach (TopicPublisher publisher in _Publishers)
        {
          if (publisher.TopicName.Equals(topic.Name, StringComparison.InvariantCultureIgnoreCase))
          {
            publisherToBeRemoved = publisher;
            break;
          }
        }
        if (publisherToBeRemoved != null)
        {
          _Publishers.Remove(publisherToBeRemoved);
          publisherToBeRemoved.Dispose();
          publisherToBeRemoved = null;
        }
      }
      catch
      {
        throw;
      }
    }

    public void Subscribe(string consumerId, Topic topic, bool getLatestTopicValue)
    {
      try
      {
        string options = getLatestTopicValue ? "?consumer.retroactive=true" : string.Empty;
        //ITopic newTopic = new ActiveMQTopic(topic.Name);
        //IMessageConsumer consumer = _Session.CreateDurableConsumer(newTopic, consumerId, "2 > 1", false);
        ITopic newTopic = new ActiveMQTopic(topic.Name + options);
        IMessageConsumer consumer = _Session.CreateConsumer(newTopic);
        TopicSubscriber newSubscriber = new TopicSubscriber(consumer);
        newSubscriber.TopicName = topic.Name;
        newSubscriber.OnMessageReceived += new MessageReceivedDelegate(OnMessage);
        _Subscribers.Add(newSubscriber);
      }
      catch
      {
        throw;
      }
    }

    public void UnSubscribe(Topic topic)
    {
      if (topic == null || string.IsNullOrEmpty(topic.Name))
        throw new ArgumentNullException();

      TopicSubscriber subscriberToBeRemoved = null;

      try
      {
        foreach (TopicSubscriber subscriber in _Subscribers)
        {
          if (subscriber.TopicName.Equals(topic.Name, StringComparison.InvariantCultureIgnoreCase))
          {
            subscriberToBeRemoved = subscriber;
            break;
          }
        }
        if (subscriberToBeRemoved != null)
        {
          _Subscribers.Remove(subscriberToBeRemoved);
          subscriberToBeRemoved.Dispose();
          subscriberToBeRemoved = null;
        }
      }
      catch
      {
        throw;
      }
    }

    public bool PublishUpdate(Topic topic)
    {
      if (topic == null || string.IsNullOrEmpty(topic.Name))
        throw new ArgumentNullException();

      try
      {
        foreach (TopicPublisher publisher in _Publishers)
        {
          if (publisher.TopicName.Equals(topic.Name, StringComparison.InvariantCultureIgnoreCase))
          {
            return publisher.SendMessage(topic.Value);
          }
        }
        return false;
      }
      catch
      {
        throw;
      }
    }

    #endregion

    // --------------------------------------------------------------------------------

    public delegate void TopicUpdatedDelegate(string topicName, string message);
    public event TopicUpdatedDelegate OnMessageReceived;

    public delegate void ConnectionExceptionDelegate(string eventMessage);
    public event ConnectionExceptionDelegate OnConnectionException;

    // --------------------------------------------------------------------------------

    public TopicConnection(IConnectionFactory connectionFactory, string clientId)
    {
      _Connection = connectionFactory.CreateConnection();
      _Connection.ClientId = clientId;
      _Connection.Start();
      _Connection.ExceptionListener += new ExceptionListener(OnException);
      _Connection.ConnectionInterruptedListener += new ConnectionInterruptedListener(OnConnectionInterrupted);
      _Connection.ConnectionResumedListener += new ConnectionResumedListener(OnConnectionResumed);
      _Session = _Connection.CreateSession();
    }

    public void OnMessage(string topicName, string message)
    {
      OnMessageReceived(topicName, message);
    }

    public void OnException(Exception e)
    {
      //Console.WriteLine(e.Message);
      OnConnectionException(e.Message);
    }

    public void OnConnectionInterrupted()
    {

    }

    public void OnConnectionResumed()
    {

    }

    #region IDisposable Members

    public void Dispose()
    {
      if (!_IsDisposed)
      {
        _Session.Dispose();
        _Connection.Dispose();

        foreach (TopicPublisher publisher in _Publishers)
        {
          publisher.Dispose();
        }
        _Publishers.Clear();

        foreach (TopicSubscriber subscriber in _Subscribers)
        {
          subscriber.OnMessageReceived -= new MessageReceivedDelegate(OnMessage);
          subscriber.Dispose();
        }
        _Subscribers.Clear();

        _IsDisposed = true;
      }
    }

    #endregion
  }

}
