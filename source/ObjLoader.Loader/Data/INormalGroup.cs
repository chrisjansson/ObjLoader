using ObjLoader.Loader.Data.VertexData;

namespace ObjLoader.Loader.Data
{
    public interface INormalGroup
    {
        Normal GetNormal(int i);
        void AddNormal(Normal normal);
    }
}