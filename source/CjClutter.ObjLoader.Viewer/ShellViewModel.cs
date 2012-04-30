using System.ComponentModel.Composition;
using System.IO;
using Microsoft.Win32;
using ObjLoader.Loader.Loaders;
using System.Linq;

namespace CjClutter.ObjLoader.Viewer
{
    [Export(typeof(IShell))]
    public class ShellViewModel : Screen<ShellView>, IShell
    {
        private readonly IObjLoaderFactory _objLoaderFactory;
        private readonly IObjToMehsConverter _converter;

        private LoadResult _loadResult;

        [ImportingConstructor]
        public ShellViewModel(IObjLoaderFactory objLoaderFactory, IObjToMehsConverter converter)
        {
            _converter = converter;
            _objLoaderFactory = objLoaderFactory;
        }

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
            var objLoader = _objLoaderFactory.Create(x => null);

            _loadResult = objLoader.Load(modelStream);

            View.Meshes = _converter.Convert(_loadResult).ToList();
        }
    }
}
