using ObjLoader.Loader.Common;

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