using System.Linq;
using CjClutter.ObjLoader.Viewer.Extensions;

namespace CjClutter.ObjLoader.Viewer
{
    public static class FaceExtensions
    {
        public static bool AreNeighbours(this Face f, Face face)
        {
            var ownEdges = f.Edges;
            var faceEdges = face.Edges;

            return ownEdges.Any(edge => faceEdges.Any(e => e.AreEqual(edge)));
        }

        public static bool HasEdge(this Face f, Edge edge)
        {
            var edges = f.Edges;
            return edges.Any(e => e == edge);
        }
    }
}