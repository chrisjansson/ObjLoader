using FluentAssertions;
using NUnit.Framework;
using ObjLoader.Loader.Data;
using ObjLoader.Loader.Data.DataStore;
using ObjLoader.Loader.Data.VertexData;
using ObjLoader.Loader.TypeParsers;

namespace ObjLoader.Test.TypeParsers
{
    [TestFixture]
    public class TextureParserTests
    {
        private TextureDataStoreMock _textureDataStoreMock;
        private TextureParser _textureParser;

        [SetUp]
        public void SetUp()
        {
            _textureDataStoreMock = new TextureDataStoreMock();

            _textureParser = new TextureParser(_textureDataStoreMock);
        }

        [Test]
        public void CanParse_returns_true_on_normal_line()
        {
            const string textureKeyword = "vt";

            bool canParse = _textureParser.CanParse(textureKeyword);
            canParse.Should().BeTrue();
        }

        [Test]
        public void CanParse_returns_false_on_non_normal_line()
        {
            const string invalidKeyword = "vn";

            bool canParse = _textureParser.CanParse(invalidKeyword);
            canParse.Should().BeFalse();
        }

        [Test]
        public void Parses_normal_line_correctly()
        {
            const string textureLine = "0.500 -1.352";
            _textureParser.Parse(textureLine);

            var parsedNormal = _textureDataStoreMock.ParsedTexture;
            parsedNormal.X.Should().BeApproximately(0.5f, 0.000001f);
            parsedNormal.Y.Should().BeApproximately(-1.352f, 0.000001f);
        }

        class TextureDataStoreMock : ITextureDataStore
        {
            public Texture ParsedTexture { get; private set; }

            public Texture GetTexture(int i)
            {
                throw new System.NotImplementedException();
            }

            public void AddTexture(Texture texture)
            {
                ParsedTexture = texture;
            }
        }
    }
}