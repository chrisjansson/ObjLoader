using ObjLoader.Loader.Common;
using ObjLoader.Loader.Data;

namespace ObjLoader.Loader.TypeParsers
{
    public class TextureParser : ITypeParser
    {
        private readonly ITextureGroup _textureGroup;

        public TextureParser(ITextureGroup textureGroup)
        {
            _textureGroup = textureGroup;
        }

        public bool CanParse(string keyword)
        {
            return keyword.EqualsInvariantCultureIgnoreCase("vt");
        }

        public void Parse(string line)
        {
            string[] parts = line.Split(' ');

            float x = parts[1].ParseInvariantFloat();
            float y = parts[2].ParseInvariantFloat();

            var texture = new Texture(x, y);
            _textureGroup.AddTexture(texture);
        }
    }
}