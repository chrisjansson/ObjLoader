using ObjLoader.Loader.Data.VertexData;

namespace ObjLoader.Loader.Data
{
    public interface INormalDataStore
    {
        Normal GetNormal(int i);
        void AddNormal(Normal normal);
    }
}