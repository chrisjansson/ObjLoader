using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Linq;

namespace CjClutter.ObjLoader.Viewer
{
    public class Catmull
    {
        public IEnumerable<Face> CreateCubeMesh()
        {
            //Front
            var vertex0 = new Vector3(-0.5f, -0.5f, 0.5f);
            var vertex1 = new Vector3(-0.5f, 0.5f, 0.5f);
            var vertex2 = new Vector3(0.5f, 0.5f, 0.5f);
            var vertex3 = new Vector3(0.5f, -0.5f, 0.5f);

            //Rear
            var vertex4 = new Vector3(-0.5f, -0.5f, -0.5f);
            var vertex5 = new Vector3(-0.5f, 0.5f, -0.5f);
            var vertex6 = new Vector3(0.5f, 0.5f, -0.5f);
            var vertex7 = new Vector3(0.5f, -0.5f, -0.5f);

            var front = new Face(new List<Vector3> { vertex0, vertex1, vertex2, vertex3 });
            var back = new Face(new List<Vector3> { vertex4, vertex5, vertex6, vertex7 });

            var left = new Face(new List<Vector3> { vertex4, vertex5, vertex1, vertex0 });
            var right = new Face(new List<Vector3> { vertex7, vertex3, vertex2, vertex6 });

            var top = new Face(new List<Vector3> { vertex1, vertex5, vertex6, vertex2 });
            var bottom = new Face(new List<Vector3> { vertex0, vertex4, vertex7, vertex3 });

            return new List<Face> {front, back, left, right, top, bottom};
        }

        public void Monkey()
        {
            var mesh = CreateCubeMesh().ToList();

            var edges = mesh.SelectMany(f => f.Edges).Distinct();

            var edgePoints = new List<Vector3>();
            foreach (var edge in edges)
            {
                var edge1 = edge;
                var facePoints = mesh
                    .Where(f => f.HasEdge(edge1))
                    .Select(x => x.FacePoint)
                    .ToList();

                var sum = new Vector3();
                sum += edge1.Vertex1;
                sum += edge1.Vertex2;
                sum += facePoints[0];
                sum += facePoints[1];

                var edgePoint = sum/4;
            }
        }
    }
}