using System.Collections.Generic;

namespace ObjLoader.Loader.Data
{
    public interface IGroupDataStore
    {
        void PushGroup(string groupName);
    }

    public class DataStore : IGroupDataStore, IVertexGroup, ITextureGroup, INormalGroup
    {
        private readonly List<Group> _groups = new List<Group>();
        private Group _currentGroup;

        public DataStore()
        {
            PushGroup("unnamed group");
        }

        public void PushGroup(string groupName)
        {
            _currentGroup = new Group(groupName);
            _groups.Add(_currentGroup);
        }

        public Vertex GetVertex(int i)
        {
            return _currentGroup.GetVertex(i);
        }

        public void AddVertex(Vertex vertex)
        {
            _currentGroup.AddVertex(vertex);
        }

        public Texture GetTexture(int i)
        {
            return _currentGroup.GetTexture(i);
        }

        public void AddTexture(Texture texture)
        {
            _currentGroup.AddTexture(texture);
        }

        public Normal GetNormal(int i)
        {
            return _currentGroup.GetNormal(i);
        }

        public void AddNormal(Normal normal)
        {
            _currentGroup.AddNormal(normal);
        }
    }
}