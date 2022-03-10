namespace DataFeedGeneratorLibrary
{
    public class Template
    {
        public string Header { get; set; }
        public string Record { get; set; }
        public string Footer { get; set; }
        public string Group { get; set; }
        public int GroupBy { get; set; }
        public string Separator { get; set; }

        public Template() : this("", "", "")
        {
        }

        public Template(string header, string record, string footer)
        {
            Header = header;
            Record = record;
            Footer = footer;
            Group = "";
            GroupBy = -1;
            Separator = "";
        }
    }
}