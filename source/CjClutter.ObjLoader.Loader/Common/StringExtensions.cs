using System;
using System.Globalization;

namespace ObjLoader.Loader.Common
{
    public static class StringExtensions
    {
        public static float ParseInvariantFloat(this string floatString)
        {
            return float.Parse(floatString, CultureInfo.InvariantCulture.NumberFormat);
        }

        public static int ParseInvariantInt(this string intString)
        {
            return int.Parse(intString, CultureInfo.InvariantCulture.NumberFormat);
        }

        public static bool EqualsInvariantCultureIgnoreCase(this string str, string s)
        {
            return str.Equals(s, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
    }
}