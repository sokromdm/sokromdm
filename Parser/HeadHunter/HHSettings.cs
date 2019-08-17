namespace TestTaskHHru.Parser.HeadHunter
{
    class HHSettings : IParserSettings
    {
        public HHSettings(int start, int end)
        {
            StartPoint = start;
            EndPoint = end;
        }

        public string BaseUrl { get; set; } = "https://yaroslavl.hh.ru";

        public string Prefix { get; set; } = "/catalog/Prodazhi/page-{CurrentId}";

        public int StartPoint { get; set; }

        public int EndPoint { get; set; }

    }
}
