using System;
using System.Collections.Generic;
using ObjLoader.Loader.Data.Elements;
using ObjLoader.Loader.Loaders;
using OpenTK;

namespace CjClutter.ObjLoader.Viewer
{
    public class ObjToMehsConverter : IObjToMehsConverter
    {
        private LoadResult _loadResult;
        private Group _modelGroup;
        private List<Mesh> _meshes;
        private Mesh _mesh;
        private global::ObjLoader.Loader.Data.Elements.Face _face;

        public IList<Mesh> Convert(LoadResult loadResult)
        {
            _loadResult = loadResult;

            _meshes = new List<Mesh>();

            ConvertGroups();

            return _meshes;
        }

        private void ConvertGroups()
        {
            var groups = _loadResult.Groups;

            foreach (var modelGroup in groups)
            {
                _modelGroup = modelGroup;
                ConvertGroup();
            }
        }

        private void ConvertGroup()
        {
            //Group faces by material later or something
            _mesh = new Mesh();
            _mesh.Name = _modelGroup.Name;

            foreach (var face in _modelGroup.Faces)
            {
                _face = face;
                ConvertFace();
            }

            CalculateNormals();

            _meshes.Add(_mesh);
        }

        private void ConvertFace()
        {
            if (_face.Count != 3)
            {
                throw new NotImplementedException("Only triangles are supported");
            }

            for (int i = 0; i < _face.Count; i++)
            {
                var faceVertex = _face[i];

                var vertex = _loadResult.Vertices[faceVertex.VertexIndex - 1];
                _mesh.Triangles.Add(new Vector3(vertex.X, vertex.Y, vertex.Z));
            }
        }

        private void CalculateNormals()
        {
            for (int i = 0; i < _mesh.Triangles.Count; i += 3)
            {
                var a = _mesh.Triangles[i];
                var b = _mesh.Triangles[i + 1];
                var c = _mesh.Triangles[i + 2];

                var normal = Vector3.Cross(b - a, c - a);
                _mesh.Normals.Add(normal);
                _mesh.Normals.Add(normal);
                _mesh.Normals.Add(normal);
            }
        }
    }
}