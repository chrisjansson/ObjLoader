using System.Collections;
using System.Collections.Generic;
using System.IO;
using CjClutter.ObjLoader.Viewer.Camera;
using CjClutter.ObjLoader.Viewer.CoordinateSystems;
using CjClutter.ObjLoader.Viewer.InputAdapters;
using Microsoft.Win32;
using ObjLoader.Loader.Loaders;
using System.Linq;
using OpenTK;

namespace CjClutter.ObjLoader.Viewer
{
    public class ShellViewModel : Screen<ShellView>, IShell, IMouseInputTarget
    {
        private readonly IObjLoaderFactory _objLoaderFactory;
        private readonly IObjToMehsConverter _converter;
        private readonly IMouseInputAdapter _mouseInputAdapter;
        private readonly IGuiToRelativeCoordinateTransformer _guiToRelativeCoordinateTransformer;

        private LoadResult _loadResult;

        public ShellViewModel(
            IObjLoaderFactory objLoaderFactory, 
            IObjToMehsConverter converter,
            IMouseInputAdapter mouseInputAdapter,
            ITrackballCamera camera,
            IGuiToRelativeCoordinateTransformer guiToRelativeCoordinateTransformer)
        {
            _converter = converter;
            _mouseInputAdapter = mouseInputAdapter;
            _camera = camera;
            _guiToRelativeCoordinateTransformer = guiToRelativeCoordinateTransformer;
            _objLoaderFactory = objLoaderFactory;
        }

        protected override void OnViewAttached(ShellView view)
        {
            _mouseInputAdapter.Target = this;
            _mouseInputAdapter.Source = view.openGlUserControl.glControl;

            _guiToRelativeCoordinateTransformer.Control = view.openGlUserControl.glControl;
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

        private ITrackballCamera _camera;
        public ITrackballCamera Camera
        {
            get { return _camera; }
            set { Set(x => _camera = x, () => Camera, value); }
        }

        public void OnMouseDrag(MouseDragEventArgs mouseDragEventArgs)
        {
            var startPoint = _guiToRelativeCoordinateTransformer.TransformToRelative(mouseDragEventArgs.StartPosition);
            var endPoint = _guiToRelativeCoordinateTransformer.TransformToRelative(mouseDragEventArgs.EndPosition);

            Camera.Rotate(startPoint, endPoint);
        }

        public void OnMouseDragEnd(MouseDragEventArgs mouseDragEventArgs)
        {
            var startPoint = _guiToRelativeCoordinateTransformer.TransformToRelative(mouseDragEventArgs.StartPosition);
            var endPoint = _guiToRelativeCoordinateTransformer.TransformToRelative(mouseDragEventArgs.EndPosition);

            Camera.CommitRotation(startPoint, endPoint);
        }

        public void OnMouseMove(Vector2d position)
        {
        }

        public void OnLeftMouseButtonDown(Vector2d position)
        {
        }

        public void OnLeftMouseButtonUp(Vector2d position)
        {
        }
    }
}
