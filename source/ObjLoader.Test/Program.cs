using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using ObjLoader.Loader.Loaders;
using ObjLoader = ObjLoader.Loader.Loaders.ObjLoader;

namespace ObjLoader.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var objLoaderFactory = new ObjLoaderFactory();

            var objLoader = objLoaderFactory.Create(fileName => File.Open(fileName, FileMode.Open, FileAccess.Read));

            var fileStream = new FileStream("buddha2.obj", FileMode.Open, FileAccess.Read);

            var buffer = new byte[fileStream.Length];
            int read = fileStream.Read(buffer, 0, (int)fileStream.Length);
            var memoryStream = new MemoryStream(buffer, 0, read);


            var result = objLoader.Load(memoryStream);

            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
            PrintResult(result);
        }

        private static void PrintResult(ObjLoaderLoaderResult result)
        {
            var sb = new StringBuilder();

            sb.AppendLine("Load result:");
            sb.Append("Vertices: ");
            sb.AppendLine(result.Vertices.Count.ToString(CultureInfo.InvariantCulture));
            sb.Append("Textures: ");
            sb.AppendLine(result.Textures.Count.ToString(CultureInfo.InvariantCulture));
            sb.Append("Normals: ");
            sb.AppendLine(result.Normals.Count.ToString(CultureInfo.InvariantCulture));
            sb.AppendLine();
            sb.AppendLine("Groups: ");

            foreach (var loaderGroup in result.Groups)
            {
                sb.AppendLine(loaderGroup.Name);
                sb.Append("Faces: ");
                sb.AppendLine(loaderGroup.Faces.Count.ToString(CultureInfo.InvariantCulture));
            }

            Console.WriteLine(sb);
        }
    }
}
