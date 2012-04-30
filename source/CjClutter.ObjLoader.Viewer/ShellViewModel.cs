using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using Caliburn.Micro;
using Microsoft.Win32;
using ObjLoader.Loader.Loaders;
using OpenTK;

namespace CjClutter.ObjLoader.Viewer
{
    [Export(typeof(IShell))]
    public class ShellViewModel : ViewModelBase<ShellView>, IShell
    {
        private LoadResult _loadResult;

        public void Browse()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".obj";
            openFileDialog.Filter = "Wavefront Obj File (.obj) |*.obj";

            var result = openFileDialog.ShowDialog();

            if (!result.Value)
            {
                return;
            }

            var modelStream = openFileDialog.OpenFile();
            LoadModel(modelStream);
        }

        private void LoadModel(Stream modelStream)
        {
            var objLoaderFactory = new ObjLoaderFactory();
            var objLoader = objLoaderFactory.Create(x => null);

            _loadResult = objLoader.Load(modelStream);

            var objToMehsConverter = new ObjToMehsConverter();
            View.Meshes = objToMehsConverter.Convert(_loadResult);
        }
    }

    public class Mesh
    {
        public Mesh()
        {
            Triangles = new List<Vector3>();
            Normals = new List<Vector3>();
        }

        public List<Vector3> Triangles { get; set; }
        public List<Vector3> Normals { get; set; }
    }
}
