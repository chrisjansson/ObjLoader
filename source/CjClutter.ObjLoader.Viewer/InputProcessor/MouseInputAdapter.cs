using System.Windows.Forms;
using OpenTK;

namespace CjClutter.ObjLoader.Viewer.InputProcessor
{
    public class MouseInputAdapter
    {
        private Control _source;
        public Control Source
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

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            var eventArgument = new Vector2d(e.X, e.Y);
            Target.OnMouseMove(eventArgument);
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                var position = new Vector2d(e.X, e.Y);
                Target.OnLeftMouseButtonDown(position);    
            }
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                var position = new Vector2d(e.X, e.Y);
                Target.OnLeftMouseButtonUp(position);    
            }
        }
    }
}