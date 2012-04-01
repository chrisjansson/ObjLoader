using ObjLoader.Loader.Data;
using ObjLoader.Loader.TypeParsers;

namespace ObjLoader.Loader.Loaders
{
    public class ObjLoaderFactory
    {
        public ObjLoader Create()
        {
            var dataStore = new DataStore();
            
            var faceParser = new FaceParser(dataStore);
            var groupParser = new GroupParser(dataStore);
            var normalParser = new NormalParser(dataStore);
            var textureParser = new TextureParser(dataStore);
            var vertexParser = new VertexParser(dataStore);

            return new ObjLoader(faceParser, groupParser, normalParser, textureParser, vertexParser);
        }
    }
}