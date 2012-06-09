using FluentAssertions;
using FluentAssertions.Assertions;

namespace CjClutter.ObjLoader.Viewer.Extensions
{
    public static class NumericAssertionsExtensions
    {
        public static AndConstraint<NumericAssertions<double>> BeApproximately(this NumericAssertions<double> assertions, double expectedValue)
        {
            return assertions.BeApproximately(expectedValue, 0.0001);
        }
    }
}