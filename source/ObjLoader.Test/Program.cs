using System;
using System.Diagnostics;
using System.IO;
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

            Loader.Loaders.ObjLoader objLoader = objLoaderFactory.Create(fileName => File.Open(fileName, FileMode.Open, FileAccess.Read));

            var fileStream = new FileStream("buddha2.obj", FileMode.Open, FileAccess.Read);

            var buffer = new byte[fileStream.Length];
            int read = fileStream.Read(buffer, 0, (int)fileStream.Length);
            var memoryStream = new MemoryStream(buffer, 0, read);


            objLoader.Load(memoryStream);

            //objLoader.Load(fileStream);

            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
        }
    }
}
