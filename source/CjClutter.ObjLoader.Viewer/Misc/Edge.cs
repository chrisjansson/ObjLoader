using OpenTK;

namespace CjClutter.ObjLoader.Viewer
{
    public struct Edge
    {
        public Vector3 Vertex1 { get; set; }
        public Vector3 Vertex2 { get; set; }

        public Edge(Vector3 vertex1, Vector3 vector2) : this()
        {
            Vertex1 = vertex1;
            Vertex2 = vector2;
        }

        public static bool operator ==(Edge e1, Edge e2)
        {
            var sameOrderEqual = e1.Vertex1.Equals(e2.Vertex1) && e1.Vertex2.Equals(e2.Vertex2);
            var oppositeOrderEqual = e1.Vertex1.Equals(e2.Vertex2) && e1.Vertex2.Equals(e2.Vertex1);

            return sameOrderEqual || oppositeOrderEqual;
        }

        public static bool operator !=(Edge e1, Edge e2)
        {
            return !(e1 == e2);
        }

        public bool Equals(Edge other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != typeof (Edge)) return false;
            return Equals((Edge) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Vertex1.GetHashCode()*397) ^ Vertex2.GetHashCode();
            }
        }
    }
}