using System;

namespace CjClutter.ObjLoader.Viewer.Extensions
{
    public static class DoubleExtensions
    {
         public static double Clamp(this double d, double min, double max)
         {
             var clampedToMax = Math.Min(d, max);
             var clampedToMin = Math.Max(clampedToMax, min);
             return clampedToMin;
         }
    }
}