namespace ObjLoader.Loader.TypeParsers
{
    public interface ITypeParser
    {
        bool CanParse(string keyword);
        void Parse(string line);
    }
}