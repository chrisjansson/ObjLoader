using System;
using System.IO;
using ObjLoader.Loader.Data;
using ObjLoader.Loader.TypeParsers;

namespace ObjLoader.Loader.Loaders
{
    public class ObjLoaderFactory
    {
        public ObjLoader Create(Func<string, Stream> openMaterialStreamFunc)
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

            return new ObjLoader(faceParser, groupParser, normalParser, textureParser, vertexParser, materialLibraryParser);
        }
    }
}