using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlProxy;

namespace FacebookLogic
{
    public class AppSettings
    {
        public bool RememberUser { get; set; }

        public string LatestAccessToken { get; set; }

        private static XmlSerializerProxy s_XmlSerializer =
            new XmlSerializerProxy(typeof(AppSettings));

        public static AppSettings LoadFromFile()
        {
            AppSettings appSettings = null;
            if (File.Exists(@"Login.xml"))
            {
                using (Stream stream = new FileStream(@"Login.xml", FileMode.Open))
                {
                    appSettings = s_XmlSerializer.Deserialize(stream) as AppSettings;
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
                    s_XmlSerializer.Serialize(stream, this);
                }
            }
            else
            {
                using (Stream stream = new FileStream(@"Login.xml", FileMode.CreateNew))
                {
                    s_XmlSerializer.Serialize(stream, this);
                }
            }
        }
    }
}
