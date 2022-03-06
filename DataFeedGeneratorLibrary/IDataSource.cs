namespace DataFeedGeneratorLibrary
{
    public interface IDataSource
    {
        int RecordCount { get; }
        int ColumnCount { get; }
        string[] GetData(int recordNumber);
        string GetData(int recordNumber, int columnNumber);
    }
}