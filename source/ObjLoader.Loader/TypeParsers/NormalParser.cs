using System;
using ObjLoader.Loader.Common;
using ObjLoader.Loader.Data;
using ObjLoader.Loader.Data.VertexData;

namespace ObjLoader.Loader.TypeParsers
{
    public class NormalParser : INormalParser
    {
        private readonly INormalDataStore _normalDataStore;

        public NormalParser(INormalDataStore normalDataStore)
        {
            _normalDataStore = normalDataStore;
        }

        public bool CanParse(string keyword)
        {
            return keyword.EqualsInvariantCultureIgnoreCase("vn");
        }
        
        public void Parse(string line)
        {
            string[] parts = line.Split(' ');

            float x = parts[1].ParseInvariantFloat();
            float y = parts[2].ParseInvariantFloat();
            float z = parts[3].ParseInvariantFloat();

            var normal = new Normal(x, y, z);
            _normalDataStore.AddNormal(normal);
        }
    }
}