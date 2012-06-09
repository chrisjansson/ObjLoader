using NUnit.Framework;
using FluentAssertions;

namespace CjClutter.ObjLoader.Viewer.Extensions
{
    [TestFixture]
    public class DoubleExtensionsTests
    {
        [Test]
        public void Clamp_clamps_to_max()
        {
            const double valueToClamp = 1.1;
            var actual = valueToClamp.Clamp(0.0, 1.0);

            actual.Should().BeApproximately(1.0);
        }

        [Test]
        public void Clamp_clamps_to_min()
        {
            const double valueToClamp = -1.1;
            var actual = valueToClamp.Clamp(-1.0, 1.0);

            actual.Should().BeApproximately(-1.0);
        }

        [Test]
        public void Clamp_does_not_clamp_value_within_limits()
        {
            const double valueToClamp = 0.0;
            var actual = valueToClamp.Clamp(-1.0, 1.0);

            actual.Should().BeApproximately(0.0);
        }
    }
}