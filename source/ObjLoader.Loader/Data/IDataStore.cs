using System.Collections.Generic;
using ObjLoader.Loader.Data.VertexData;

namespace ObjLoader.Loader.Data
{
    public interface IDataStore 
    {
        IList<Vertex> Vertices { get; }
        IList<Texture> Textures { get; }
        IList<Normal> Normals { get; }
        IList<Material> Materials { get; }
        IList<Group> Groups { get; }
    }
}