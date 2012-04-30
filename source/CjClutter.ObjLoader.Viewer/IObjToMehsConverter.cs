using System.Collections.Generic;
using ObjLoader.Loader.Loaders;

namespace CjClutter.ObjLoader.Viewer
{
    public interface IObjToMehsConverter
    {
        IList<Mesh> Convert(LoadResult loadResult);
    }
}