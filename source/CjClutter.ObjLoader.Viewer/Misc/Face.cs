using System.Collections.Generic;
using OpenTK;
using System.Linq;

namespace CjClutter.ObjLoader.Viewer
{
    public class Face
    {
        private readonly List<Vector3> _vertices;

        public Face(List<Vector3> vertices)
        {
            _vertices = vertices;
        }

        public List<Vector3> Vertices
        {
            get { return _vertices; }
        }

        public List<Edge> Edges
        {
            get { return GetEdges().ToList(); }
        }

        private IEnumerable<Edge> GetEdges()
        {
            int numberOfVertices = Vertices.Count;
            for (int i = 0; i < numberOfVertices; i++)
            {
                var vertex1 = Vertices[i % numberOfVertices];
                var vertex2 = Vertices[(i + 1)%numberOfVertices];
                yield return new Edge(vertex1, vertex2);
            }
        }

        public Vector3 FacePoint
        {
            get { return CalculateFacePoint(); }
        }

        private Vector3 CalculateFacePoint()
        {
            var sum = new Vector3();
            
            foreach (var vertex in Vertices)
            {
                sum += vertex;
            }

            return sum/Vertices.Count;
        }
    }
}