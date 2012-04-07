using System;
using System.IO;

namespace ObjLoader.Loader.Loaders
{
    public interface IObjLoaderFactory
    {
        IObjLoader Create(Func<string, Stream> openMaterialStreamFunc);
        IObjLoader Create();
    }
}