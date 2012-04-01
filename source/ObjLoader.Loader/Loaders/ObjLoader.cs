using System.Collections.Generic;
using System.IO;
using ObjLoader.Loader.TypeParsers;

namespace ObjLoader.Loader.Loaders
{
    public class ObjLoader
    {
        private readonly IFaceParser _faceParser;
        private readonly IGroupParser _groupParser;
        private readonly INormalParser _normalParser;
        private readonly ITextureParser _textureParser;
        private readonly IVertexParser _vertexParser;

        private List<ITypeParser> _typeParsers;
        private StreamReader _lineStreamReader;

        private readonly List<string> _unrecognizedLines = new List<string>();

        public ObjLoader(
            IFaceParser faceParser,
            IGroupParser groupParser,
            INormalParser normalParser,
            ITextureParser textureParser,
            IVertexParser vertexParser)
        {
            _faceParser = faceParser;
            _groupParser = groupParser;
            _normalParser = normalParser;
            _textureParser = textureParser;
            _vertexParser = vertexParser;

            SetupTypeParsers();
        }

        private void SetupTypeParsers()
        {
            _typeParsers = new List<ITypeParser>
                               {
                                   _vertexParser,
                                   _faceParser,
                                   _groupParser,
                                   _normalParser,
                                   _textureParser
                               };
        }

        public void Load(Stream lineStream)
        {
            _lineStreamReader = new StreamReader(lineStream);

            while(!_lineStreamReader.EndOfStream)
            {
                ParseLine();
            }
        }

        private void ParseLine()
        {
            var currentLine = _lineStreamReader.ReadLine();

            if (string.IsNullOrEmpty(currentLine) || currentLine[0] == '#')
            {
                return;
            }

            var keyword = currentLine.Split(new[] {' '}, 2)[0];

            foreach (var typeParser in _typeParsers)
            {
                if(typeParser.CanParse(keyword))
                {
                    typeParser.Parse(currentLine);
                    return;
                }
            }

            _unrecognizedLines.Add(currentLine);
        }
    }
}