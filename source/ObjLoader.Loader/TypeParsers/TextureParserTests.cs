using FluentAssertions;
using FluentAssertions.Assertions;
using NUnit.Framework;
using ObjLoader.Loader.Data;
using ObjLoader.Loader.Data.VertexData;

namespace ObjLoader.Loader.TypeParsers
{
    [TestFixture]
    public class TextureParserTests
    {
        private TextureGroupMock _textureGroupMock;
        private TextureParser _textureParser;

        [SetUp]
        public void SetUp()
        {
            _textureGroupMock = new TextureGroupMock();

            _textureParser = new TextureParser(_textureGroupMock);
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
            const string textureLine = "vt 0.500 -1.352";
            _textureParser.Parse(textureLine);

            var parsedNormal = _textureGroupMock.ParsedTexture;
            parsedNormal.X.Should().BeApproximately(0.5f, 0.000001f);
            parsedNormal.Y.Should().BeApproximately(-1.352f, 0.000001f);
        }

        class TextureGroupMock : ITextureGroup
        {
            public Texture ParsedTexture { get; set; }

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