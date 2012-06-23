using CjClutter.ObjLoader.Viewer.Adapters;
using OpenTK;

namespace CjClutter.ObjLoader.Viewer.CoordinateSystems
{
    public class GuiToRelativeCoordinateTransformer : IGuiToRelativeCoordinateTransformer
    {
        public ISizeAdapter Source { get; set; }

        public Vector2d TransformToRelative(Vector2d absoluteCoordinate)
        {
            var x = absoluteCoordinate.X / Source.Width * 2 - 1;
            var y = (Source.Height - absoluteCoordinate.Y) / Source.Height * 2 - 1;

            return new Vector2d(x, y);
        }
    }
}