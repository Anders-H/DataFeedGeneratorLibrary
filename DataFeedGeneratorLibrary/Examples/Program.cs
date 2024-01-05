using DataFeedGeneratorLibrary;

CsvExample();
HtmlExample();
JsonExample();
GroupExample();

void CsvExample()
{
    const string header = @"Counter; Title; Artist; Year
";
    const string record = @"((#)); ""((0))""; ""((1))""; ((2))
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

void HtmlExample()
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
         </tr>";

        const string record = @"
         <tr>
            <td>((0))</td><td>((1))</td><td>((2))</td>
         </tr>";

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

void JsonExample()
{
    const string header = @"{
  ""Records"": [";

    const string record = @"
    {
      ""Artist"": ""((0))"",
      ""Title"": ""((1))"",
      ""Year"": ((2))
    }((sep))";

    const string footer = @"
  ]
}";

    var template = new Template(
        header,
        record,
        footer
    )
    {
        Separator = ","
    };

    var g = new Generator(
        SampleDataSource.CreateRecordCollection(),
        template
    );

    Console.WriteLine(g.Generate());
}

void GroupExample()
{
    const string header = @"
<html>
   <head>
      <title>My record collection</title>
   </head>
   <body>
      <table>
         <tr>
            <td><b>Title</b></td><td><b>Year</b></td>
         </tr>";

        const string group = @"
         <tr>
            <td style=""text-align: center;""><b>((0))</b></tr> <!-- NEW GROUP -->
         </tr>";

        const string record = @"
         <tr>
            <td>((1))</td><td>((2))</td>
         </tr>";

        const string footer = @"
      </table>
   </body>
</html>
";

    var template = new Template(
        header,
        record,
        footer
    )
    {
        Group = group,
        GroupBy = 0
    };

    var g = new Generator(
        SampleDataSource.CreateRecordCollection(),
        template
    );

    Console.WriteLine(g.Generate());
}