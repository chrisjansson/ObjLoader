using NUnit.Framework;
using ObjLoader.Loader.Data;
using FluentAssertions;
using FluentAssertions.Assertions;

namespace ObjLoader.Loader.TypeParsers
{
    [TestFixture]
    public class NormalParserTests
    {
        private const string NormalLine = "vn 1.000000 2.000000 -1.000000";

        private NormalParser _normalParser;
        private NormalGroupMock _normalGroupMock;

        [SetUp]
         public void SetUp()
        {
            _normalGroupMock = new NormalGroupMock();

            _normalParser = new NormalParser(_normalGroupMock);
        }

        [Test]
        public void CanParse_returns_true_on_normal_line()
        {
            bool canParse = _normalParser.CanParse(NormalLine);
            canParse.Should().BeTrue();
        }

        [Test]
        public void CanParse_returns_false_on_non_normal_line()
        {
            const string invalidNormal = "vt 0.498993 0.250415";

            bool canParse = _normalParser.CanParse(invalidNormal);
            canParse.Should().BeFalse();
        }

        [Test]
        public void Parses_normal_line_correctly()
        {
            _normalParser.Parse(NormalLine);

            var parsedNormal = _normalGroupMock.ParsedNormal;
            parsedNormal.X.Should().BeApproximately(1, 0.000001f);
            parsedNormal.Y.Should().BeApproximately(2, 0.000001f);
            parsedNormal.Z.Should().BeApproximately(-1, 0.000001f);
        }
    }

    public class NormalGroupMock : INormalGroup
    {
        public Normal ParsedNormal { get; set; }

        public Normal GetNormal(int i)
        {
            throw new System.NotImplementedException();
        }

        public void AddNormal(Normal normal)
        {
            ParsedNormal = normal;
        }
    }
}