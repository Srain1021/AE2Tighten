using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace AE2Tightening.Configura
{
    public class Configs
    {
        public static AppConfig FileConfigs { get; set; }

        public static AppConfig ReadJsonConfig(string jsonFile)
        {
            try
            {
                string jsonStr = File.ReadAllText(jsonFile, Encoding.UTF8);
                FileConfigs = JsonConvert.DeserializeObject<AppConfig>(jsonStr);
                return FileConfigs;
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        //public static string SerialzeToJson(object obj)
        //{
             
        //}

    }
}
