using GodSharp.Balluff;
using GodSharp.Balluff.Commands;
namespace AE2Tightening.Configura
{
    public class RfidConfig
    {
        public string DeviceId { get; set; }

        public ProtocolType ProtocolType { get; set; }

        public AutoOutputType OutputType { get; set; }

        public AutoOutputOptions OutputOptions { get; set; }

        public string Host { get; set; }

        public int RemotePort { get; set; }

        public bool Available { get; set; }

        public string Misc { get; set; }
    }

    /// <summary>
    /// The type of rfid host.
    /// </summary>
    public enum RfidHostType
    {
        Serial,
        TcpClient,
        TcpServer
    }
}
