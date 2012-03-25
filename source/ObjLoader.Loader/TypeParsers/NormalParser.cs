using System;
using ObjLoader.Loader.Common;
using ObjLoader.Loader.Data;

namespace ObjLoader.Loader.TypeParsers
{
    public class NormalParser : ITypeParser
    {
        private readonly INormalGroup _normalGroup;

        public NormalParser(INormalGroup normalGroup)
        {
            _normalGroup = normalGroup;
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
            _normalGroup.AddNormal(normal);
        }
    }
}