using System;
using ObjLoader.Loader.Data;
using ObjLoader.Loader.Common;
using ObjLoader.Loader.Data.VertexData;

namespace ObjLoader.Loader.TypeParsers
{
    public class VertexParser : IVertexParser
    {
        private readonly IVertexDataStore _vertexDataStore;

        public VertexParser(IVertexDataStore vertexDataStore)
        {
            _vertexDataStore = vertexDataStore;
        }

        public bool CanParse(string keyword)
        {
            return keyword.EqualsInvariantCultureIgnoreCase("v");
        }

        public void Parse(string line)
        {
            string[] parts = line.Split(new[]{' '}, StringSplitOptions.RemoveEmptyEntries);

            var x = parts[1].ParseInvariantFloat();
            var y = parts[2].ParseInvariantFloat();
            var z = parts[3].ParseInvariantFloat();

            var vertex = new Vertex(x, y, z);
            _vertexDataStore.AddVertex(vertex);
        }
    }
}