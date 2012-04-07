using ObjLoader.Loader.Common;
using ObjLoader.Loader.TypeParsers.Interfaces;

namespace ObjLoader.Loader.TypeParsers
{
    public abstract class TypeParserBase : ITypeParser
    {
        protected abstract string Keyword { get; }

        public bool CanParse(string keyword)
        {
            return keyword.EqualsInvariantCultureIgnoreCase(Keyword);
        }

        public abstract void Parse(string line);
    }
}