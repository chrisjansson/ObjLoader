using System.Collections.Generic;
using ObjLoader.Loader.Data.VertexData;

namespace ObjLoader.Loader.Data
{
    public class Group : IVertexGroup, ITextureGroup, INormalGroup, IFaceGroup
    {
        private readonly List<Vertex> _vertices = new List<Vertex>();
        private readonly List<Texture> _textures = new List<Texture>();
        private readonly List<Normal> _normals = new List<Normal>();
        private readonly List<Face> _faces = new List<Face>();
        
        public Group(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public Vertex GetVertex(int i)
        {
            return _vertices[i];
        }

        public void AddVertex(Vertex vertex)
        {
            _vertices.Add(vertex);
        }

        public Texture GetTexture(int i)
        {
            return _textures[i];
        }

        public void AddTexture(Texture texture)
        {
            _textures.Add(texture);
        }

        public Normal GetNormal(int i)
        {
            return _normals[i];
        }

        public void AddNormal(Normal normal)
        {
            _normals.Add(normal);
        }

        public Face GetFace(int i)
        {
            return _faces[i];
        }

        public void AddFace(Face face)
        {
            _faces.Add(face);
        }
    }
}