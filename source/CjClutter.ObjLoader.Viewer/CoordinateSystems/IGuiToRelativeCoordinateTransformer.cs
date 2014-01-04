using CjClutter.ObjLoader.Viewer.Adapters;
using OpenTK;

namespace CjClutter.ObjLoader.Viewer.CoordinateSystems
{
    public interface IGuiToRelativeCoordinateTransformer
    {
        Vector2d TransformToRelative(Vector2d absoluteCoordinate);
        ISizeAdapter Source { get; set; }
    }
}