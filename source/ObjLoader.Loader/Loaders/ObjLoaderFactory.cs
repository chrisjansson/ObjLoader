using System;
using System.IO;
using ObjLoader.Loader.Data.DataStore;
using ObjLoader.Loader.TypeParsers;

namespace ObjLoader.Loader.Loaders
{
    public class ObjLoaderFactory : IObjLoaderFactory
    {
        public IObjLoader Create(Func<string, Stream> openMaterialStreamFunc)
        {
            var dataStore = new DataStore();
            
            var faceParser = new FaceParser(dataStore);
            var groupParser = new GroupParser(dataStore);
            var normalParser = new NormalParser(dataStore);
            var textureParser = new TextureParser(dataStore);
            var vertexParser = new VertexParser(dataStore);

            var materialLibraryLoader = new MaterialLibraryLoader(dataStore);
            var materialLibraryLoaderFacade = new MaterialLibraryLoaderFacade(materialLibraryLoader, openMaterialStreamFunc);
            var materialLibraryParser = new MaterialLibraryParser(materialLibraryLoaderFacade);
            var useMaterialParser = new UseMaterialParser(dataStore);

            return new ObjLoader(dataStore, faceParser, groupParser, normalParser, textureParser, vertexParser, materialLibraryParser, useMaterialParser);
        }
    }
}