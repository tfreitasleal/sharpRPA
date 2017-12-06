using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace sharpRPA.Core
{
    public static class Common
    {
        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
        public static string GetScriptFolderPath()
        {
            return GetAppFolderPath() + "My Scripts\\";
        }
        public static string GetAppFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\sharpRPA\\";
        }
        public static List<IGrouping<Attribute,Type>> GetGroupedCommands()
        {

                var groupedCommands = Assembly.GetExecutingAssembly().GetTypes()
                              .Where(t => t.Namespace == "sharpRPA.Core.AutomationCommands")
                              .Where(t => t.Name != "ScriptCommand")
                              .Where(t => t.IsAbstract == false)
                              .Where(t => t.BaseType.Name == "ScriptCommand")
                              .Where(t => CommandEnabled(t))
                              .GroupBy(t => t.GetCustomAttribute(typeof(Core.AutomationCommands.Attributes.ClassAttributes.Group)))
                              .ToList();

                return groupedCommands;

        }
        private static bool CommandEnabled(Type cmd)
        {
            var scriptCommand = (Core.AutomationCommands.ScriptCommand)Activator.CreateInstance(cmd);
            return scriptCommand.CommandEnabled;
        }



    }

    [Serializable]
    public class ApplicationSettings
    {
        public ServerSettings ServerSettings { get; set; } = new ServerSettings();
        public void Save(ApplicationSettings appSettings)
        {
            var savePath = Core.Common.GetAppFolderPath() + "AppSettings.xml";
            var fileStream = System.IO.File.Create(savePath);

            //output to xml file
            XmlSerializer serializer = new XmlSerializer(typeof(ApplicationSettings));
            serializer.Serialize(fileStream, appSettings);
            fileStream.Close();
        }
        public ApplicationSettings GetOrCreateApplicationSettings()
        {
            var savePath = Core.Common.GetAppFolderPath() + "AppSettings.xml";

            ApplicationSettings appSettings;
            if (System.IO.File.Exists(savePath))
            {
                //open file and return it or return new settings on error
                var fileStream = System.IO.File.Open(savePath, FileMode.Open);

                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ApplicationSettings));
                    appSettings = (ApplicationSettings)serializer.Deserialize(fileStream);
                }
                catch (Exception)
                {
                    appSettings = new ApplicationSettings();
                }

                fileStream.Close();


            }
            else
            {
                appSettings = new ApplicationSettings();
            }

            return appSettings;
         
        }
    }
    [Serializable]
    public class ServerSettings
    {
        public bool ServerConnectionEnabled { get; set; }
        public bool ConnectToServerOnStartup { get; set; }
        public bool RetryServerConnectionOnFail { get; set; }
        public string ServerURL { get; set; }
    }
}
