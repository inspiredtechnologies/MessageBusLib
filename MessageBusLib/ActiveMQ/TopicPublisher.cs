using System;
using System.Collections.Generic;
using System.Text;

using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using Apache.NMS;

namespace MessageBusLib.ActiveMQ
{
  class TopicPublisher : IDisposable
  {
    private readonly IMessageProducer _Producer;
    private bool _IsDisposed = false;

    public string TopicName { get; set; }

    public TopicPublisher(IMessageProducer producer)
    {
      _Producer = producer;
    }

    public bool SendMessage(string message)
    {
      if (!_IsDisposed)
      {
        try
        {
          ITextMessage textMessage = new ActiveMQTextMessage(message);
          _Producer.Send(textMessage);
          return true;
        }
        catch (ConnectionClosedException exc)
        {
          Console.WriteLine(exc.ToString());
        }
        catch
        {
          throw;
        }
      }
      else
      {
        throw new ObjectDisposedException(GetType().FullName);
      }
      return false;
    }

    #region IDisposable Members

    public void Dispose()
    {
      if (!_IsDisposed)
      {
        _Producer.Dispose();
        _IsDisposed = true;
      }
    }

    #endregion
  }
}
