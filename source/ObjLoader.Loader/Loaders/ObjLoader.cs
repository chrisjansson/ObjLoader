using System.Collections.Generic;
using ObjLoader.Loader.TypeParsers;
using ObjLoader.Loader.TypeParsers.Interfaces;

namespace ObjLoader.Loader.Loaders
{
    public class ObjLoader : LoaderBase
    {
        private readonly List<ITypeParser> _typeParsers = new List<ITypeParser>();

        private readonly List<string> _unrecognizedLines = new List<string>();

        public ObjLoader(
            IFaceParser faceParser, 
            IGroupParser groupParser, 
            INormalParser normalParser, 
            ITextureParser textureParser, 
            IVertexParser vertexParser, 
            IMaterialLibraryParser materialLibraryParser)
        {
            SetupTypeParsers(
                vertexParser, 
                faceParser, 
                normalParser, 
                textureParser, 
                groupParser, 
                materialLibraryParser);
        }

        private void SetupTypeParsers(params ITypeParser[] parsers)
        {
            foreach (var parser in parsers)
            {
                _typeParsers.Add(parser);
            }
        }

        protected override void BeforeLoad() {}

        protected override void ParseLine(string keyword, string data)
        {
            foreach (var typeParser in _typeParsers)
            {
                if (typeParser.CanParse(keyword))
                {
                    typeParser.Parse(data);
                    return;
                }
            }

            _unrecognizedLines.Add(keyword + " " + data);
        }

        protected override void AfterLoad() {}
    }
}