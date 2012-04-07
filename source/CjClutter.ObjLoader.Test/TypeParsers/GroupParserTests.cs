using FluentAssertions;
using NUnit.Framework;
using ObjLoader.Loader.Data;
using ObjLoader.Loader.Data.DataStore;
using ObjLoader.Loader.TypeParsers;

namespace ObjLoader.Test.TypeParsers
{
    [TestFixture]
    public class GroupParserTests
    {
        private GroupDataStoreMock _groupDataStoreMock;
        private GroupParser _groupParser;

        [SetUp]
        public void SetUp()
        {
            _groupDataStoreMock = new GroupDataStoreMock();

            _groupParser = new GroupParser(_groupDataStoreMock);
        }

        [Test]
        public void CanParse_returns_true_on_normal_line()
        {
            const string groupKeyword = "g";

            bool canParse = _groupParser.CanParse(groupKeyword);
            canParse.Should().BeTrue();
        }

        [Test]
        public void CanParse_returns_false_on_non_normal_line()
        {
            const string invalidKeyword = "vt";

            bool canParse = _groupParser.CanParse(invalidKeyword);
            canParse.Should().BeFalse();
        }

        [Test]
        public void Parses_normal_line_correctly()
        {
            const string normalLine = "test group";
            _groupParser.Parse(normalLine);

            var parsedGroupName = _groupDataStoreMock.ParsedGroupName;
            parsedGroupName.Should().Be("test group");
        }
    }

    class GroupDataStoreMock : IGroupDataStore
    {
        public string ParsedGroupName { get; set; }
        
        public void PushGroup(string groupName)
        {
            ParsedGroupName = groupName;
        }
    }
}