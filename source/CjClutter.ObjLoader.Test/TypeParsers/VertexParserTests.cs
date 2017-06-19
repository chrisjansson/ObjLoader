using FluentAssertions;
using NUnit.Framework;
using ObjLoader.Loader.Data;
using ObjLoader.Loader.Data.DataStore;
using ObjLoader.Loader.Data.VertexData;
using ObjLoader.Loader.TypeParsers;

namespace ObjLoader.Test.TypeParsers
{
    [TestFixture]
    public class VertexParserTests
    {
        private VertexDataStoreMock _vertexDataStoreMock;
        private VertexParser _vertexParser;

        [SetUp]
        public void SetUp()
        {
            _vertexDataStoreMock = new VertexDataStoreMock();

            _vertexParser = new VertexParser(_vertexDataStoreMock);
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
            const string vertexLine = "0.123 0.234 0.345";
            _vertexParser.Parse(vertexLine);

            var parsedNormal = _vertexDataStoreMock.ParsedVertex;
            parsedNormal.X.Should().BeApproximately(0.123f, 0.000001f);
            parsedNormal.Y.Should().BeApproximately(0.234f, 0.000001f);
            parsedNormal.Z.Should().BeApproximately(0.345f, 0.000001f);
        }

        class VertexDataStoreMock : IVertexDataStore
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