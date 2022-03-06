using System;
using System.Collections.Generic;

namespace DataFeedGeneratorLibrary
{
    public class SampleDataSource : IDataSource
    {
        private readonly List<string[]> _records;

        public SampleDataSource()
        {
            _records = new List<string[]>();
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
}