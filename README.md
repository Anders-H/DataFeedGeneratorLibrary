# DataFeedGeneratorLibrary

```
Install-Package DataFeedGenerator
```

A simple library for generating any kind one dimensional data feed like RSS, HTML or JSON.

The `Generator` class constructor takes a data source and a template. The `Generate` method generates the string output, as [shown here](https://github.com/Anders-H/DataFeedGeneratorLibrary/blob/main/Examples/Program.cs).

The data source is any class that implemates the `IDataSource` interface, typically a list of string arrays. A sample implementation that can be populated with custom data is [shown here](https://github.com/Anders-H/DataFeedGeneratorLibrary/blob/main/DataFeedGeneratorLibrary/SampleDataSource.cs).

Finally, the `Template` class has a header string that represents the start of the output, a record string that will be repeated for each record in the data source, and a footer that represents the end of the output.

Value placeholders in the template is a column index surrounded by double parentheses. The value of the first column is referred to as `((0))`, the second is referred to as `((1))`, and so on.

Special place holders are `((#))` for one based record number (shown in the CSV example) and `((sep))` for record separator (shown in the JSON example).

Also, the generator can group on one column.

The following samples uses the sample data returned from the static method `CreateRecordCollection`. Implementation:

```
public static SampleDataSource CreateRecordCollection()
{
    var x = new SampleDataSource();
    x.AddData(new[] { "Deep Purple", "Machine head", "1972" });
    x.AddData(new[] { "Kansas", "Song for America", "1975" });
    x.AddData(new[] { "Kansas", "Leftoverture", "1976" });
    x.AddData(new[] { "Pink Floyd", "The dark side of the moon", "1973" });
    x.AddData(new[] { "Pink Floyd", "The wall", "1979" });
    x.AddData(new[] { "Queen", "A night at the opera", "1975" });
    x.AddData(new[] { "Yes", "Close to the edge", "1972" });
    return x;
}
```

## CSV sample

```
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
```

### Output

```
Counter; Title; Artist; Year
1; "Deep Purple"; "Machine head"; 1972
2; "Kansas"; "Song for America"; 1975
3; "Kansas"; "Leftoverture"; 1976
4; "Pink Floyd"; "The dark side of the moon"; 1973
5; "Pink Floyd"; "The wall"; 1979
6; "Queen"; "A night at the opera"; 1975
7; "Yes"; "Close to the edge"; 1972
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
            <td>Kansas</td><td>Leftoverture</td><td>1976</td>
         </tr>
         <tr>
            <td>Pink Floyd</td><td>The dark side of the moon</td><td>1973</td>
         </tr>
         <tr>
            <td>Pink Floyd</td><td>The wall</td><td>1979</td>
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
      "Artist": "Kansas",
      "Title": "Leftoverture",
      "Year": 1976
    },
    {
      "Artist": "Pink Floyd",
      "Title": "The dark side of the moon",
      "Year": 1973
    },
    {
      "Artist": "Pink Floyd",
      "Title": "The wall",
      "Year": 1979
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

## Group example

```
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
);

template.Group = group;
template.GroupBy = 0;

var g = new Generator(
    SampleDataSource.CreateRecordCollection(),
    template
);
```

```
<html>
   <head>
      <title>My record collection</title>
   </head>
   <body>
      <table>
         <tr>
            <td><b>Title</b></td><td><b>Year</b></td>
         </tr>
         <tr>
            <td style="text-align: center;"><b>Deep Purple</b></tr> <!-- NEW GROUP -->
         </tr>
         <tr>
            <td>Machine head</td><td>1972</td>
         </tr>
         <tr>
            <td style="text-align: center;"><b>Kansas</b></tr> <!-- NEW GROUP -->
         </tr>
         <tr>
            <td>Song for America</td><td>1975</td>
         </tr>
         <tr>
            <td>Leftoverture</td><td>1976</td>
         </tr>
         <tr>
            <td style="text-align: center;"><b>Pink Floyd</b></tr> <!-- NEW GROUP -->
         </tr>
         <tr>
            <td>The dark side of the moon</td><td>1973</td>
         </tr>
         <tr>
            <td>The wall</td><td>1979</td>
         </tr>
         <tr>
            <td style="text-align: center;"><b>Queen</b></tr> <!-- NEW GROUP -->
         </tr>
         <tr>
            <td>A night at the opera</td><td>1975</td>
         </tr>
         <tr>
            <td style="text-align: center;"><b>Yes</b></tr> <!-- NEW GROUP -->
         </tr>
         <tr>
            <td>Close to the edge</td><td>1972</td>
         </tr>
      </table>
   </body>
</html>
```