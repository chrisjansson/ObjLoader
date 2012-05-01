using System.Collections.Generic;
using OpenTK;

namespace CjClutter.ObjLoader.Viewer
{
    public class Mesh
    {
        public Mesh()
        {
            Triangles = new List<Vector3>();
            Normals = new List<Vector3>();
        }

        public string Name { get; set; }

        public List<Vector3> Triangles { get; set; }
        public List<Vector3> Normals { get; set; }
    }
}