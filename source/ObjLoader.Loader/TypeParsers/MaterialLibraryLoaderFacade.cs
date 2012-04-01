using System.IO;
using ObjLoader.Loader.Loaders;

namespace ObjLoader.Loader.TypeParsers
{
    public class MaterialLibraryLoaderFacade : IMaterialLibraryLoaderFacade
    {
        private readonly IMaterialLibraryLoader _loader;

        public MaterialLibraryLoaderFacade(IMaterialLibraryLoader loader)
        {
            _loader = loader;
        }

        public void Load(string materialFileName)
        {
            using (var fileStream = new FileStream("buddha2.obj", FileMode.Open, FileAccess.Read))
            {
                _loader.Load(fileStream);
            }
        }
    }
}