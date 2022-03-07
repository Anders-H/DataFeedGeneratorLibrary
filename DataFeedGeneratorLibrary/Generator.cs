using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataFeedGeneratorLibrary
{
    public class Generator
    {
        private readonly IDataSource _dataSource;
        private readonly Template _template;

        public Generator(IDataSource dataSource, Template template)
        {
            _dataSource = dataSource;
            _template = template;
        }

        public string Generate()
        {
            var s = new StringBuilder();
            s.Append(Process(_template.Header, _dataSource.GetData(0)));

            for (var i = 0; i < _dataSource.RecordCount; i++)
                s.Append(Process(_template.Record, _dataSource.GetData(i)));
            
            s.Append(Process(_template.Footer, _dataSource.GetData(_dataSource.RecordCount - 1)));

            return s.ToString();
        }

        public void GenerateToFile(string outputFilename)
        {
            var data = Generate();
            using (var sw = new StreamWriter(outputFilename, false, Encoding.UTF8))
            {
                sw.Write(data);
                sw.Flush();
                sw.Close();
            }
        }

        private string Process(string template, string[] data)
        {
            var index = 0;

            foreach (var d in data)
            {
                template = template.Replace($@"(({index}))", data[index]);
                index++;
            }

            return template;
        }
    }
}