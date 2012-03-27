using ObjLoader.Loader.Data.VertexData;

namespace ObjLoader.Loader.Data
{
    public interface IVertexDataStore
    {
        Vertex GetVertex(int i);
        void AddVertex(Vertex vertex);
    }
}