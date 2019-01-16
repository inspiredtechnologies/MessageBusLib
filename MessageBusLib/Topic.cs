using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBusLib
{
  public class Topic
  {
    public String Name { get; set; }
    public String Value { get; set; }

    public Topic()
    {
    }

    public Topic(string topicName, string value)
    {
      Name = topicName;
      Value = value;
    }
  }
}
