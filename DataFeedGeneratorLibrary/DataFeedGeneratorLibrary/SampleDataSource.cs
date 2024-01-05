namespace DataFeedGeneratorLibrary;

public class SampleDataSource : IDataSource
{
    private readonly List<string[]> _records;

    public SampleDataSource()
    {
        _records = new List<string[]>();
    }

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

    public int RecordCount =>
        _records.Count;

    public int ColumnCount =>
        _records.Count > 0 ? _records[0].Length : 0;

    public void AddData(string[] record)
    {
        if (_records.Count <= 0)
        {
            _records.Add(record);
            return;
        }

        if (_records[0].Length != record.Length)
            throw new SystemException($@"Column count mismatch. Expected {_records[0].Length} columns, got {record.Length} columns.");

        _records.Add(record);
    }

    public string[] GetData(int recordNumber) =>
        _records[recordNumber];

    public string GetData(int recordNumber, int columnNumber) =>
        _records[recordNumber][columnNumber];
}