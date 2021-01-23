namespace AE2Tightening.Configura
{ 
    public class TightenConfig
    {
        public bool Available { get; set; }
        /// <summary>
        /// 是否写入条码
        /// </summary>
        public bool CodeRequest { get; set; }

        public string CodeType { get; set; }
        /// <summary>
        /// 拧紧编号
        /// </summary>
        public int ToolId { get; set; }
        /// <summary>
        /// 拧紧机别名
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 网络地址
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

    }
}
