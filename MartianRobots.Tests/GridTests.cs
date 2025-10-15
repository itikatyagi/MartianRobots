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
            Assert.False(grid.IsOutOfBounds(2, 1));
        }

        [Fact]
        public void Scent_CanBeAdded_AndDetected()
        {
            var grid = new Grid(5, 3);
            grid.AddScent(1, 1, 'N');
            Assert.True(grid.HasScent(1, 1, 'N'));
            Assert.False(grid.HasScent(1, 1, 'E'));
        }

        [Fact]
        public void MultipleScents_DoNotInterfere()
        {
            var grid = new Grid(5, 3);
            grid.AddScent(0, 0, 'N');
            grid.AddScent(5, 3, 'S');

            Assert.True(grid.HasScent(0, 0, 'N'));
            Assert.True(grid.HasScent(5, 3, 'S'));
            Assert.False(grid.HasScent(0, 0, 'S'));
            Assert.False(grid.HasScent(5, 3, 'N'));
        }

        [Fact]
        public void AddingSameScentMultipleTimes_IsSafe()
        {
            var grid = new Grid(5, 3);
            grid.AddScent(2, 2, 'E');
            grid.AddScent(2, 2, 'E'); // duplicate
            Assert.True(grid.HasScent(2, 2, 'E'));
        }
    }
}