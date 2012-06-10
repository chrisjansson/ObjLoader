using System.Collections.Generic;
using System.Linq;
using OpenTK;

namespace CjClutter.ObjLoader.Viewer.Misc
{
    public class Monkey
    {
        private Face _face;
        private Vector3 _vertex0;
        private Vector3 _vertex1;
        private Vector3 _vertex2;
        private Vector3 _vertex3;
        private List<Face> _faces;

        public List<Face> SplitQuad(Face face)
        {
            _face = face;
            _faces = new List<Face>();

            GetCorners();

            var width = _vertex1 - _vertex0;
            var height = _vertex3 - _vertex0;

            var deltaWidth = width / 2;
            var deltaHeight = height / 2;

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    var vertex0 = _vertex0 + deltaWidth * i + deltaHeight * j;
                    var vertex1 = _vertex0 + deltaWidth * (i+1) + deltaHeight * j;
                    var vertex2 = _vertex0 + deltaWidth * (i+1) + deltaHeight * (j+1);
                    var vertex3 = _vertex0 + deltaWidth * i + deltaHeight * (j+1);

                    var newFace = new Face(new List<Vector3> {vertex0, vertex1, vertex2, vertex3});
                    _faces.Add(newFace);
                }
            }

            return _faces;
        }

        private void GetCorners()
        {
            var vertices = _face.Vertices.ToList();

            _vertex0 = vertices[0];
            _vertex1 = vertices[1];
            _vertex2 = vertices[2];
            _vertex3 = vertices[3];
        }
    }
}