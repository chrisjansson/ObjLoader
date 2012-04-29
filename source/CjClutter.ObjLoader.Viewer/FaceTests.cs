using System.Collections.Generic;
using NUnit.Framework;
using OpenTK;
using FluentAssertions;
using FluentAssertions.Assertions;

namespace CjClutter.ObjLoader.Viewer
{
    [TestFixture]
    public class FaceTests
    {
        private Face _face;
        private Vector3 _vertex0;
        private Vector3 _vertex1;
        private Vector3 _vertex2;
        private Vector3 _vertex3;

        [SetUp]
        public void SetUp()
        {
            _vertex0 = new Vector3(-1, -1, 0);
            _vertex1 = new Vector3(-1, 1, 0);
            _vertex2 = new Vector3(1, 1, 0);
            _vertex3 = new Vector3(1, -1, 0);

            _face = new Face(new List<Vector3> {_vertex0, _vertex1, _vertex2, _vertex3});
        }

        [Test]
        public void Returns_correct_number_of_edges()
        {
            var edges = _face.Edges;

            edges.Should().HaveCount(4);
        }

        [Test]
        public void Returns_correct_edges()
        {
            var edges = _face.Edges;

            var edge0 = new Edge(_vertex0, _vertex1);
            var edge1 = new Edge(_vertex1, _vertex2);
            var edge2 = new Edge(_vertex2, _vertex3);
            var edge3 = new Edge(_vertex3, _vertex0);

            edges.Should().Contain(edge => edge.AreEqual(edge0));
            edges.Should().Contain(edge => edge.AreEqual(edge1));
            edges.Should().Contain(edge => edge.AreEqual(edge2));
            edges.Should().Contain(edge => edge.AreEqual(edge3));
        }

        [Test]
        public void Returns_correct_face_point()
        {
            var facePoint = _face.FacePoint;

            facePoint.X.Should().BeApproximately(0, 0.00001f);
            facePoint.Y.Should().BeApproximately(0, 0.00001f);
            facePoint.Z.Should().BeApproximately(0, 0.00001f);
        }
    }
}