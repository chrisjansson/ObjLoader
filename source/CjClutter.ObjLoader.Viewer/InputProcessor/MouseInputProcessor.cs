using System;
using System.Windows.Forms;
using OpenTK;

namespace CjClutter.ObjLoader.Viewer.InputProcessor
{
    public class MouseInputProcessor
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

        public event Action<MouseInputEvent> MouseMove;

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
            FireAction(MouseMove, eventArgument);
        }

        private void FireAction<T>(Action<T> action, T argument)
        {
            if(action != null)
            {
                action(argument);
            }
        }
    }
}