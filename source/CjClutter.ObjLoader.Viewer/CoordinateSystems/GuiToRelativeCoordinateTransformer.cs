using System.Windows.Forms;
using OpenTK;

namespace CjClutter.ObjLoader.Viewer.CoordinateSystems
{
    public class GuiToRelativeCoordinateTransformer : IGuiToRelativeCoordinateTransformer
    {
        public Control Control { get; set; }

        public Vector2d TransformToRelative(Vector2d absoluteCoordinate)
        {
            var x = absoluteCoordinate.X / Control.Width * 2 - 1;
            var y = (Control.Height - absoluteCoordinate.Y) / Control.Height * 2 - 1;

            return new Vector2d(x, y);
        }
    }
}