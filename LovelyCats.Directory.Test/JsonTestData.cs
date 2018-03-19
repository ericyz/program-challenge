using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace LovelyCats.Directory.Test {
    public class JsonTestData<T> : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>();

        public JsonTestData()
        {
            var fileName = typeof(T).Name.ToLower();

            var filePath = System.IO.Directory.GetCurrentDirectory().TrimEnd('\\') + $"\\TestData\\{fileName}.json";

            if (!File.Exists(filePath)) throw new Exception($"Test data file {filePath} not found.");

            var records = JsonConvert.DeserializeObject<IEnumerable<T>>(File.ReadAllText(filePath));

            foreach (var record in records)
            {
                _data.Add(new Object[] { record });
            }
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}