using System.IO;

namespace ObjLoader.Loader.Loaders
{
    public interface IObjLoader
    {
        ObjLoaderLoaderResult Load(Stream lineStream);
    }
}