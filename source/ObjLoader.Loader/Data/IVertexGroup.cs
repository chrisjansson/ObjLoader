using ObjLoader.Loader.Data.VertexData;

namespace ObjLoader.Loader.Data
{
    public interface IVertexGroup
    {
        Vertex GetVertex(int i);
        void AddVertex(Vertex vertex);
    }
}