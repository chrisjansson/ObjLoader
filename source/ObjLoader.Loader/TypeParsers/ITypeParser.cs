namespace ObjLoader.Loader.TypeParsers
{
    public interface ITypeParser
    {
        void Parse(string line);
        bool CanParse(string line);
    }
}