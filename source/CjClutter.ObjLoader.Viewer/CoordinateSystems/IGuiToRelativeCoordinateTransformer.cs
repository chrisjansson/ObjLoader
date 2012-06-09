using System.Windows.Forms;
using OpenTK;

namespace CjClutter.ObjLoader.Viewer.CoordinateSystems
{
    public interface IGuiToRelativeCoordinateTransformer
    {
        Vector2d TransformToRelative(Vector2d absoluteCoordinate);
        Control Control { get; set; }
    }
}