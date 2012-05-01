namespace CjClutter.ObjLoader.Viewer
{
    public partial class ShellView
    {
        public ShellView()
        {
            InitializeComponent();
        }

        //private void Render(Face face)
        //{
        //    GL.Begin(BeginMode.Quads);
        //    GL.Color3(GetRandomColor());
        //    foreach (var vector3 in face.Vertices)
        //    {
        //        //vector3.Normalize();
        //        //GL.Vertex3(vector3);
        //        GL.Vertex3(MapCubeToSphere(vector3 * 2));
        //    }

        //    GL.End();
        //}

        //private Color GetRandomColor()
        //{
        //    var r = _random.Next(256);
        //    var g = _random.Next(256);
        //    var b = _random.Next(256);

        //    return Color.FromArgb(r, g, b);
        //}

        //private Vector3 MapCubeToSphere(Vector3 coordinate)
        //{
        //    var xSquared = coordinate.X * coordinate.X;
        //    var ySquared = coordinate.Y * coordinate.Y;
        //    var zSquared = coordinate.Z * coordinate.Z;

        //    var xBelowSqrt = 1 - ySquared / 2 - zSquared / 2 + (ySquared * zSquared) / 3;
        //    var x = coordinate.X * Math.Sqrt(xBelowSqrt);

        //    var yBelowSqrt = 1 - zSquared / 2 - xSquared / 2 + (zSquared * xSquared) / 3;
        //    var y = coordinate.Y * Math.Sqrt(yBelowSqrt);

        //    var zBelowSqrt = 1 - xSquared / 2 - ySquared / 2 + (xSquared * ySquared) / 3;
        //    var z = coordinate.Z * Math.Sqrt(zBelowSqrt);

        //    return new Vector3((float)x, (float)y, (float)z);

        //}
        

        //private void Render(global::ObjLoader.Loader.Data.Elements.Face face)
        //{
        //    for (int i = 0; i < face.Count; i++)
        //    {
        //        var faceVertex = face[i];

        //        var vertex = _loadResult.Vertices[faceVertex.VertexIndex - 1];
        //        GL.Vertex3(vertex.X, vertex.Y, vertex.Z);
        //    }
        //}

        //private List<Face> SubdivideQuadMesh(List<Face> mesh, int subdivisionLevel)
        //{
        //    if (subdivisionLevel == 0)
        //    {
        //        return mesh;
        //    }

        //    var monkey = new Monkey();
        //    var faces = new List<Face>();
        //    foreach (var face in mesh)
        //    {
        //        var splitQuad = monkey.SplitQuad(face);
        //        faces.AddRange(splitQuad);
        //    }

        //    return SubdivideQuadMesh(faces, subdivisionLevel - 1);
        //}

        //private void Update()
        //{
        //    _angle += 1.0;
        //    Render();
        //}
    }
}
