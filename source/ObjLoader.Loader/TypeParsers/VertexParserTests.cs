using FluentAssertions;
using FluentAssertions.Assertions;
using NUnit.Framework;
using ObjLoader.Loader.Data;

namespace ObjLoader.Loader.TypeParsers
{
    [TestFixture]
    public class VertexParserTests
    {
        private VertexGroupMock _vertexGroupMock;
        private VertexParser _vertexParser;

        [SetUp]
        public void SetUp()
        {
            _vertexGroupMock = new VertexGroupMock();

            _vertexParser = new VertexParser(_vertexGroupMock);
        }

        [Test]
        public void CanParse_returns_false_on_non_vertex_line()
        {
            const string invalidVertex = "vt";

            bool canParse = _vertexParser.CanParse(invalidVertex);
            canParse.Should().BeFalse();
        }

        [Test]
        public void CanParse_returns_true_on_vertex_line()
        {
            const string vertexLine = "v";

            bool canParse = _vertexParser.CanParse(vertexLine);
            canParse.Should().BeTrue();
        }

        [Test]
        public void Parses_vertex_line_correctly()
        {
            const string vertexLine = "v 0.123 0.234 0.345";
            _vertexParser.Parse(vertexLine);

            var parsedNormal = _vertexGroupMock.ParsedVertex;
            parsedNormal.X.Should().BeApproximately(0.123f, 0.000001f);
            parsedNormal.Y.Should().BeApproximately(0.234f, 0.000001f);
            parsedNormal.Z.Should().BeApproximately(0.345f, 0.000001f);
        }

        class VertexGroupMock : IVertexGroup
        {
            public Vertex ParsedVertex { get; set; }

            public Vertex GetVertex(int i)
            {
                throw new System.NotImplementedException();
            }

            public void AddVertex(Vertex vertex)
            {
                ParsedVertex = vertex;
            }
        }
        
    }
}