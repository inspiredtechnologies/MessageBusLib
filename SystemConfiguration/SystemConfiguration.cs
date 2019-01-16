using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SystemConfiguration
{
  public class SystemConfiguration
  {
    private const string ConfigurationFileExtension = @".cfg";

    private string _AppConfigFolder = Environment.GetFolderPath(
      Environment.SpecialFolder.CommonApplicationData) + @"\NetworkEqptMonitor\";

    private bool _AppSettingsChanged = false;
    private string _NmsServerIpAddress = "127.0.0.1";
    private string _MessageBusBrokerUri;
    private int _EquipmentHealthCheckInterval;
    private Collection<NetworkEqpt> _Equipment;

    // --------------------------------------------------------

    #region Public Properties

    public string ConfigurationFileName { get; set; }

    public string NmsServerIpAddress
    {
      get { return _NmsServerIpAddress; }
      set
      {
        _NmsServerIpAddress = value;
        _AppSettingsChanged = true;
      }
    }

    public string MessageBusBrokerUri
    {
      get { return _MessageBusBrokerUri; }
      set
      {
        _MessageBusBrokerUri = value;
        _AppSettingsChanged = true;
      }
    }

    public int EquipmentHealthCheckInterval
    {
      get { return _EquipmentHealthCheckInterval; }
      set
      {
        _EquipmentHealthCheckInterval = value;
        _AppSettingsChanged = true;
      }
    }

    public Collection<NetworkEqpt> Equipment
    {
      get { return _Equipment; }
      set
      {
        _Equipment = value;
        _AppSettingsChanged = true;
      }
    }

    #endregion

    // --------------------------------------------------------

    #region Public Methods

    public bool Load()
    {
      XmlSerializer serializer = null;
      FileStream fileStream = null;
      bool fileExists = false;

      try
      {
        // Create an XmlSerializer for the SystemConfiguration type.
        serializer = new XmlSerializer(typeof(SystemConfiguration));
        FileInfo fi = new FileInfo(_AppConfigFolder + ConfigurationFileName + ConfigurationFileExtension);
        // If the config file exists, open it.
        if (fi.Exists)
        {
          fileStream = fi.OpenRead();
          // Create a new instance of the SystemConfiguration by deserializing the config file.
          SystemConfiguration appSettings = (SystemConfiguration)serializer.Deserialize(fileStream);
          // Assign property values to this instance of the SystemConfiguration class.
          _NmsServerIpAddress = appSettings.NmsServerIpAddress;
          _MessageBusBrokerUri = appSettings.MessageBusBrokerUri;
          _EquipmentHealthCheckInterval = appSettings.EquipmentHealthCheckInterval;
          _Equipment = appSettings.Equipment;
          fileExists = true;
        }
        else
        {
          // Create default config.
          _NmsServerIpAddress = "127.0.0.1";
          _MessageBusBrokerUri = "failover:(tcp://localhost:61616)?transport.timeout=3000";
          _EquipmentHealthCheckInterval = 15;
          _AppSettingsChanged = true;
          fileExists = Save();
        }
      }
      catch
      {
        throw;
      }
      finally
      {
        if (fileStream != null)
        {
          fileStream.Close();
        }
      }
      return fileExists;
    }

    public bool Save()
    {
      if (_AppSettingsChanged)
      {
        StreamWriter writer = null;
        XmlSerializer serializer = null;

        try
        {
          if (!Directory.Exists(_AppConfigFolder))
          {
            Directory.CreateDirectory(_AppConfigFolder);
          }

          // Create an XmlSerializer for the SystemConfiguration type.
          serializer = new XmlSerializer(typeof(SystemConfiguration));
          writer = new StreamWriter(_AppConfigFolder + ConfigurationFileName + ConfigurationFileExtension, false);
          // Serialize this instance of the SystemConfiguration class to the config file.
          serializer.Serialize(writer, this);
        }
        catch
        {
          throw;
        }
        finally
        {
          if (writer != null)
          {
            writer.Close();
          }
        }
      }
      return _AppSettingsChanged;
    }

    #endregion

    // ------------------------------------------------------------------

    public SystemConfiguration()
    {

    }

    public SystemConfiguration(string configurationFileName)
    {
      ConfigurationFileName = configurationFileName;
    }

  }
}
