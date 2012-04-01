using System.IO;

namespace ObjLoader.Loader.Common
{
    public class FileGateway : IFileGateway
    {
        public Stream OpenFile(string path, FileMode fileMode = FileMode.Open, FileAccess fileAccess = FileAccess.Read)
        {
            return File.Open(path, fileMode, fileAccess);
        }

        public bool FileExists(string path)
        {
            return File.Exists(path);
        }
    }
}