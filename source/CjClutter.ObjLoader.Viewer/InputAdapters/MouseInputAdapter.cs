using System.Windows;
using System.Windows.Input;
using OpenTK;

namespace CjClutter.ObjLoader.Viewer.InputAdapters
{
    public class MouseInputAdapter : IMouseInputAdapter
    {
        private UIElement _source;
        private Vector2d _mouseDownPosition;
        private bool _isDragging;

        public UIElement Source
        {
            get { return _source; }
            set
            {
                UnsubscribeFromOldSource();
                _source = value;
                SubscribeToNewSource();
            }
        }

        public IMouseInputTarget Target { get; set; }

        private void UnsubscribeFromOldSource()
        {
            if(_source == null)
            {
                return;
            }

            _source.MouseMove -= OnMouseMove;
            _source.MouseDown -= OnMouseDown;
            _source.MouseUp -= OnMouseUp;
        }

        private void SubscribeToNewSource()
        {
            if(_source == null)
            {
                return;
            }

            _source.MouseMove += OnMouseMove;
            _source.MouseDown += OnMouseDown;
            _source.MouseUp += OnMouseUp;
        }

        private void OnMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            var location = GetPosition(mouseEventArgs);
            var position = CreateVector2dFrom(location);
            Target.OnMouseMove(position);

            FireMouseDrag(_mouseDownPosition, CreateVector2dFrom(location));
        }

        private Point GetPosition(MouseEventArgs mouseEventArgs)
        {
            return mouseEventArgs.GetPosition(Source);
        }

        private void FireMouseDrag(Vector2d startPoint, Vector2d endPoint)
        {
            if(_isDragging)
            {
                var mouseDragEventArgs = new MouseDragEventArgs(startPoint, endPoint);
                Target.OnMouseDrag(mouseDragEventArgs);
            }
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var location = GetPosition(mouseButtonEventArgs);

            if(mouseButtonEventArgs.ChangedButton == MouseButton.Left)
            {
                var position = CreateVector2dFrom(location);
                Target.OnLeftMouseButtonDown(position);

                BeginMouseDrag(position);
            }
        }

        private void BeginMouseDrag(Vector2d position)
        {
            _isDragging = true;
            _mouseDownPosition = position;
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var location = GetPosition(mouseButtonEventArgs);
            if (mouseButtonEventArgs.ChangedButton == MouseButton.Left)
            {
                var position = CreateVector2dFrom(location);
                Target.OnLeftMouseButtonUp(position);

                EndMouseDrag(location);
            }
        }

        private void EndMouseDrag(Point location)
        {
            if(_isDragging)
            {
                var endPoint = CreateVector2dFrom(location);
                FireMouseDrag(_mouseDownPosition, endPoint);
                FireEndMouseDrag(_mouseDownPosition, endPoint);
            }
            
            _isDragging = false;
        }

        private void FireEndMouseDrag(Vector2d mouseDownPosition, Vector2d endPoint)
        {
            var mouseDragEventArgs = new MouseDragEventArgs(mouseDownPosition, endPoint);
            Target.OnMouseDragEnd(mouseDragEventArgs);
        }

        private Vector2d CreateVector2dFrom(Point location)
        {
            return new Vector2d(location.X, location.Y);
        }
    }
}