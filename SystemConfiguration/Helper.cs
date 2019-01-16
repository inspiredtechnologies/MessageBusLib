using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace SystemConfiguration
{
  public class Helper
  {
    public static object DeserializeObject(Type objectType, string serializedString)
    {
      object result = null;

      try
      {
        if (!string.IsNullOrEmpty(serializedString))
        {
          XmlSerializer deserializer = new XmlSerializer(objectType);
          using (StringReader textReader = new StringReader(serializedString))
          {
            result = deserializer.Deserialize(textReader);
            textReader.Close();
          }
        }
        return result;
      }
      catch
      {
        throw;
      }
    }

    public static string SerializeObject(object objectInstance)
    {
      string result = string.Empty;

      try
      {
        if (objectInstance != null)
        {
          XmlSerializer serializer = new XmlSerializer(objectInstance.GetType());
          using (StringWriter textWriter = new StringWriter())
          {
            serializer.Serialize(textWriter, objectInstance);
            result = textWriter.ToString();
            textWriter.Close();
          }
        }
        return result;
      }
      catch
      {
        throw;
      }
    }

  }
}
