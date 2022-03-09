# DataFeedGeneratorLibrary

For .NET Framework 4.8.

A simple library for generating any kind one dimensional data feed like RSS, HTML or JSON.

The `Generator` class constructor takes a data source and a template. The `Generate` method generates the string output, as [shown here](https://github.com/Anders-H/DataFeedGeneratorLibrary/blob/main/Examples/Program.cs).

The data source is any class that implemates the `IDataSource` interface, typically a list of string arrays. A sample implementation that can be populated with custom data is [shown here](https://github.com/Anders-H/DataFeedGeneratorLibrary/blob/main/DataFeedGeneratorLibrary/SampleDataSource.cs).

Finally, the `Template` class has a header string that represents the start of the output, a record string that will be repeated for each record in the data source, and a footer that represents the end of the output.

Value placeholders in the template is a column index surrounded by double parentheses.

The following samples uses the sample data returned from the static method `CreateRecordCollection`. Implementation:

```
public static SampleDataSource CreateRecordCollection()
{
    var x = new SampleDataSource();
    x.AddData(new[] { "Deep Purple", "Machine head", "1972" });
    x.AddData(new[] { "Kansas", "Song for America", "1975" });
    x.AddData(new[] { "Pink Floyd", "The dark side of the moon", "1973" });
    x.AddData(new[] { "Queen", "A night at the opera", "1975" });
    x.AddData(new[] { "Yes", "Close to the edge", "1972" });
    return x;
}
```

## CSV sample

```
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
```

### Output

```
Title; Artist; Year
"Deep Purple"; "Machine head"; 1972
"Kansas"; "Song for America"; 1975
"Pink Floyd"; "The dark side of the moon"; 1973
"Queen"; "A night at the opera"; 1975
"Yes"; "Close to the edge"; 1972
```

## HTML sample

```
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
```

### Output

```
<html>
   <head>
      <title>My record collection</title>
   </head>
   <body>
      <table>
         <tr>
            <td><b>Artist</b></td><td><b>Title</b></td><td><b>Year</b></td>
         </tr>

         <tr>
            <td>Deep Purple</td><td>Machine head</td><td>1972</td>
         </tr>

         <tr>
            <td>Kansas</td><td>Song for America</td><td>1975</td>
         </tr>

         <tr>
            <td>Pink Floyd</td><td>The dark side of the moon</td><td>1973</td>
         </tr>

         <tr>
            <td>Queen</td><td>A night at the opera</td><td>1975</td>
         </tr>

         <tr>
            <td>Yes</td><td>Close to the edge</td><td>1972</td>
         </tr>

      </table>
   </body>
</html>
```

## JSON example

This example defines a custom record separator and uses it between records. The placeholder for the separator in the template is `((sep))`.

```
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
);

template.Separator = ",";

var g = new Generator(
    SampleDataSource.CreateRecordCollection(),
    template
);
```

### Output

```
{
  "Records": [
    {
      "Artist": "Deep Purple",
      "Title": "Machine head",
      "Year": 1972
    },
    {
      "Artist": "Kansas",
      "Title": "Song for America",
      "Year": 1975
    },
    {
      "Artist": "Pink Floyd",
      "Title": "The dark side of the moon",
      "Year": 1973
    },
    {
      "Artist": "Queen",
      "Title": "A night at the opera",
      "Year": 1975
    },
    {
      "Artist": "Yes",
      "Title": "Close to the edge",
      "Year": 1972
    }
  ]
}
```