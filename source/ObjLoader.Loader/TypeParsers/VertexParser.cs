using System;
using ObjLoader.Loader.Data;
using ObjLoader.Loader.Common;

namespace ObjLoader.Loader.TypeParsers
{
    public class VertexParser : ITypeParser
    {
        private readonly IVertexGroup _vertexGroup;

        public VertexParser(IVertexGroup vertexGroup)
        {
            _vertexGroup = vertexGroup;
        }

        public bool CanParse(string keyword)
        {
            return keyword.EqualsInvariantCultureIgnoreCase("v");
        }

        public void Parse(string line)
        {
            string[] parts = line.Split(' ');

            var x = parts[1].ParseInvariantFloat();
            var y = parts[2].ParseInvariantFloat();
            var z = parts[3].ParseInvariantFloat();

            var vertex = new Vertex(x, y, z);
            _vertexGroup.AddVertex(vertex);
        }
    }
}