using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;
using ObjLoader.Loader.Loaders;
using System.Linq;

namespace CjClutter.ObjLoader.Viewer
{
    public class ShellViewModel : Screen<ShellView>, IShell
    {
        private readonly IObjLoaderFactory _objLoaderFactory;
        private readonly IObjToMehsConverter _converter;

        private LoadResult _loadResult;

        public ShellViewModel(IObjLoaderFactory objLoaderFactory, IObjToMehsConverter converter)
        {
            _converter = converter;
            _objLoaderFactory = objLoaderFactory;
        }

        public void Browse()
        {
            var openFileDialog = new OpenFileDialog {DefaultExt = ".obj", Filter = "Wavefront Obj File (.obj) |*.obj"};
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
            
            ConvertToMeshes();
        }

        private void ConvertToMeshes()
        {
            var meshes = _converter.Convert(_loadResult).ToList();

            MeshViewModels = meshes
                .Select(CreateMeshGroupViewModel)
                .ToList();
        }

        private MeshViewModel CreateMeshGroupViewModel(Mesh mesh)
        {
            var viewModel = new MeshViewModel
                                {
                                    Name = mesh.Name, 
                                    Mesh = mesh
                                };

            return viewModel;
        }

        private List<MeshViewModel> _meshViewModels;
        public List<MeshViewModel> MeshViewModels
        {
            get { return _meshViewModels; }
            set { Set(x => _meshViewModels = x, () => MeshViewModels, value); }
        }

        public void OnSelectedMeshesChanged(IList selectedItems)
        {
            var selectedMeshes = MeshViewModels
                .Where(selectedItems.Contains)
                .Select(x => x.Mesh)
                .ToList();

            SelectedMeshes = selectedMeshes;
        }

        private List<Mesh> _selectedMeshes;
        public List<Mesh> SelectedMeshes
        {
            get { return _selectedMeshes; }
            set { Set(x => _selectedMeshes = x, () => SelectedMeshes, value); }
        }
    }
}
