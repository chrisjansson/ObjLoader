using CjClutter.ObjLoader.Viewer.Misc;

namespace CjClutter.ObjLoader.Viewer.Extensions
{
    public static class EdgeExtensions
    {
        public static bool AreEqual(this Edge e, Edge edge)
        {
            bool sameOrderEqual = e.Vertex1 == edge.Vertex1 && e.Vertex2 == edge.Vertex2;
            bool oppositeOrderEqal = e.Vertex1 == edge.Vertex2 && e.Vertex2 == edge.Vertex1;

            return sameOrderEqual || oppositeOrderEqal;
        }
    }
}