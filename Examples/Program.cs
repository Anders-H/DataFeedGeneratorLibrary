using System;
using DataFeedGeneratorLibrary;

namespace Examples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CsvExample();

            HtmlExample();

            Console.ReadLine();
        }

        private static void CsvExample()
        {
            const string header = @"Title; Artist; Year
";
            const string record = @"""((0))""; ""((1))""; ((2))
";
            var g = new Generator(
                SampleDataSource.CreateRecordCollection(),
                new Template(
                    header,
                    record,
                    ""
                )
            );

            Console.WriteLine(g.Generate());
        }

        private static void HtmlExample()
        {
            const string header = @"
<html>
   <head>
      <title>My record collection</title>
   </head>
   <body>
      <table>
         <tr>
            <td><b>Artist</b></td><td><b>Title</b></td><td><b>Year</b></td>
         </tr>
";

            const string record = @"
         <tr>
            <td>((0))</td><td>((1))</td><td>((2))</td>
         </tr>
";

            const string footer = @"
      </table>
   </body>
</html>
";

            var g = new Generator(
                SampleDataSource.CreateRecordCollection(),
                new Template(
                    header,
                    record,
                    footer
                )
            );

            Console.WriteLine(g.Generate());
        }
    }
}