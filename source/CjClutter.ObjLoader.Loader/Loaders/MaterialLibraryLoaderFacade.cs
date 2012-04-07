using System;
using System.IO;

namespace ObjLoader.Loader.Loaders
{
    public class MaterialLibraryLoaderFacade : IMaterialLibraryLoaderFacade
    {
        private readonly IMaterialLibraryLoader _loader;
        private readonly Func<string, Stream> _openStream;

        public MaterialLibraryLoaderFacade(IMaterialLibraryLoader loader, Func<string, Stream> openStream)
        {
            _loader = loader;
            _openStream = openStream;
        }

        public void Load(string materialFileName)
        {
            using (var fileStream = _openStream(materialFileName))
            {
                if (fileStream != null)
                {
                    _loader.Load(fileStream);    
                }
            }
        }
    }
}