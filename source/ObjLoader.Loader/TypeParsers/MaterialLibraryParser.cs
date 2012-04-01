namespace ObjLoader.Loader.TypeParsers
{
    public class MaterialLibraryParser : TypeParserBase, IMtlLibParser
    {
        private readonly IMaterialLoader _loader;

        public MaterialLibraryParser(IMaterialLoader loader)
        {
            _loader = loader;
        }

        protected override string Keyword
        {
            get { return "mtllib"; }
        }

        public override void Parse(string line)
        {

        }
    }
}