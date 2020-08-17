using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Threading.Tasks;

namespace FacebookLogic
{
    public class AppSettings
    {
        public bool RememberUser { get; set; }

        public string LatestAccessToken { get; set; }

        public static AppSettings LoadFromFile()
        {
            AppSettings appSettings = null;

            if (File.Exists(@"Login.xml"))
            {
                using (Stream stream = new FileStream(@"Login.xml", FileMode.Open))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(AppSettings));
                    appSettings = serializer.Deserialize(stream) as AppSettings;
                }
            }
            else
            {
                appSettings = new AppSettings();
            }    

            return appSettings;
        }

        public void SaveToFile()
        {
            if (File.Exists(@"Login.xml"))
            {
                using (Stream stream = new FileStream(@"Login.xml", FileMode.Truncate))
                {
                    XmlSerializer serializer = new XmlSerializer(this.GetType());
                    serializer.Serialize(stream, this);
                }
            }
            else
            {
                using (Stream stream = new FileStream(@"Login.xml", FileMode.CreateNew))
                {
                    XmlSerializer serializer = new XmlSerializer(this.GetType());
                    serializer.Serialize(stream, this);
                }
            }
        }
    }
}
