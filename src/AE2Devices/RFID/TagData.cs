namespace AE2Devices
{
    public class TagData
    {
        public string EngineCode { get; set; }

        public string EngineMto { get; set; }
        /// <summary>
        /// 吊具号
        /// </summary>
        public string SpreaderNo { get; set; }

        public TagData()
        {

        }

        public TagData(string code)
        {
            if (string.IsNullOrEmpty(code))
                return;
            if (code.Length <= 12)
            {
                EngineCode = code.Trim();
            }
            else if (code.Length <= 19)
            {
                EngineCode = code.Substring(0, 12).Trim();
                EngineMto = code.Substring(12);
            }
            else
            {
                EngineCode = code.Substring(0, 12).Trim();
                EngineMto = code.Substring(12, 7);
                SpreaderNo = code.Substring(19).Trim();
            }
        }

        public bool Equals(TagData obj)
        {
            if (obj == null)
                return false;
            return obj.EngineCode == EngineCode && obj.EngineMto == EngineMto && obj.SpreaderNo == SpreaderNo;
        }
        public TagData Clone()
        {
            var tag = new TagData();
            tag.EngineCode = EngineCode;
            tag.EngineMto = EngineMto;
            tag.SpreaderNo = SpreaderNo;
            return tag;
        }
    }
}
