using System;
using System.Diagnostics;
using System.IO;
using ObjLoader.Loader.Loader;
using ObjLoader = ObjLoader.Loader.Loader.ObjLoader;

namespace ObjLoader.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var objLoaderFactory = new ObjLoaderFactory();

            Loader.Loader.ObjLoader objLoader = objLoaderFactory.Create();

            var fileStream = new FileStream("buddha.obj", FileMode.Open, FileAccess.Read);

            var buffer = new byte[fileStream.Length];
            int read = fileStream.Read(buffer, 0, (int) fileStream.Length);
            var memoryStream = new MemoryStream(buffer, 0, read);


            objLoader.Load(memoryStream);

            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
        }
    }
}
