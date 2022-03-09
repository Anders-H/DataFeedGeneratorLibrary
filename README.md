# DataFeedGeneratorLibrary

A simple library for generating any kind one dimensional data feed like RSS, HTML or JSON.

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
