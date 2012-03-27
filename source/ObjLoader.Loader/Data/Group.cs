using System.Collections.Generic;

namespace ObjLoader.Loader.Data
{
    public class Group : IFaceGroup
    {
        private readonly List<Face> _faces = new List<Face>();
        
        public Group(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

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