using Xunit;
using MartianRobots.Models;

namespace MartianRobots.Tests
{
    public class GridTests
    {
        [Fact]
        public void IsOutOfBounds_ReturnsTrue_ForCoordinatesOutside()
        {
            var grid = new Grid(5, 3);
            Assert.True(grid.IsOutOfBounds(6, 2));
            Assert.True(grid.IsOutOfBounds(0, 4));
            Assert.True(grid.IsOutOfBounds(-1, 0));
        }

        [Fact]
        public void IsOutOfBounds_ReturnsFalse_ForBoundaryAndInside()
        {
            var grid = new Grid(5, 3);
            Assert.False(grid.IsOutOfBounds(0, 0));
            Assert.False(grid.IsOutOfBounds(5, 3));
        }
    }
}