using FluentAssertions;
using NUnit.Framework;
using ObjLoader.Loader.Loaders;
using ObjLoader.Loader.TypeParsers;

namespace ObjLoader.Test.TypeParsers
{
    [TestFixture]
    public class MaterialLibraryParserTests
    {
        private MaterialLibraryLoaderFacadeSpy _materialLibraryLoaderFacadeSpy;
        private MaterialLibraryParser _parser;

        [SetUp]
        public void SetUp()
        {
            _materialLibraryLoaderFacadeSpy = new MaterialLibraryLoaderFacadeSpy();
            _parser = new MaterialLibraryParser(_materialLibraryLoaderFacadeSpy);
        }

        [Test]
        public void CanParse_returns_true_on_mtlib_line()
        {
            const string groupKeyword = "mtllib";

            bool canParse = _parser.CanParse(groupKeyword);
            canParse.Should().BeTrue();
        }

        [Test]
        public void CanParse_returns_false_on_non_mtlib_line()
        {
            const string invalidKeyword = "vt";

            bool canParse = _parser.CanParse(invalidKeyword);
            canParse.Should().BeFalse();
        }

        [Test]
        public void Parses_mtlib_line_correctly()
        {
            const string faceLine = "cube.mtl";
            _parser.Parse(faceLine);

            _materialLibraryLoaderFacadeSpy.RequestedLoadFileName.Should().BeEquivalentTo("cube.mtl");
        }

        private class MaterialLibraryLoaderFacadeSpy : IMaterialLibraryLoaderFacade
        {
            public string RequestedLoadFileName { get; set; }
            
            public void Load(string materialFileName)
            {
                RequestedLoadFileName = materialFileName;
            }
        }
    }
}