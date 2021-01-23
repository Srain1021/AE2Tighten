using AE2Tightening.Frame;
using AE2Tightening.Frame.Data;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace AE2Tightening.Configura
{
    public class Configs
    {

        public XmlParamter FileConfigs { get; set; }

        public static Configs ReadJsonConfig(LogHandler log)
        {
            try
            {
                Configs config = new Configs();
                string jsonStr = File.ReadAllText("Config.json", Encoding.UTF8);
                config.FileConfigs = JsonConvert.DeserializeObject<XmlParamter>(jsonStr);
                return config;
            }
            catch (System.Exception e)
            {
                log.Err("读取配置文件异常。", e);
                return null;
            }
        }

    }
}
