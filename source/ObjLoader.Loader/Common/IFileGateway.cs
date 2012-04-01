using System.IO;

namespace ObjLoader.Loader.Common
{
    public interface IFileGateway
    {
        Stream OpenFile(string path, FileMode fileMode = FileMode.Open, FileAccess fileAccess = FileAccess.Read);
        bool FileExists(string path);
    }
}