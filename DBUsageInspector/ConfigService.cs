using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBUsageInspector
{
    public static class ConfigService
    {
        public static void SaveConfig(IDictionary<string, string> settings)
        {
            FileInfo file = new FileInfo("./config.dbui");

            if (file.Exists)
            {
                file.Delete();
            }

            using (TextWriter textWriter = file.CreateText())
            {
                foreach (KeyValuePair<string, string> setting in settings)
                {
                    string json = JsonConvert.SerializeObject(setting);

                    textWriter.WriteLine(json);
                }
            }
        }

        public static IDictionary<string, string> GetConfigSettings()
        {
            IDictionary<string, string> returnValue = new Dictionary<string, string>();

            FileInfo file = new FileInfo("./config.dbui");

            if (file.Exists)
            {
                using (StreamReader streamReader = file.OpenText())
                {
                    while (!streamReader.EndOfStream)
                    {
                        string line = streamReader.ReadLine();

                        if (line.Trim() != string.Empty)
                        {
                            KeyValuePair<string, string> setting = new KeyValuePair<string, string>();
                            setting = (KeyValuePair<string, string>)JsonConvert.DeserializeObject(line, setting.GetType());

                            returnValue.Add(setting);
                        }
                    }
                }
            }

            return returnValue;
        }
    }
}
