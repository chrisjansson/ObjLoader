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
        }

        private void SubscribeToNewSource()
        {
            if(_source == null)
            {
                return;
            }

            _source.MouseMove += OnMouseMove;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            var eventArgument = new MouseInputEvent(new Vector2d(e.X, e.Y));
            Target.OnMouseMove(eventArgument);
        }
    }
}