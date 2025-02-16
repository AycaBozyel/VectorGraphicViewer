using VectorGraphicViewer.Logging;
using VectorGraphicViewer.Service;

namespace VectorGraphicViewer.Tests
{
    public class MainWindowTests
    {
        private readonly ILogService _logService;

        [Fact]
        public void ParsePoint_ShouldReturnCorrectPoint()
        {
            var mainWindow = new DrawingService(_logService);
            string input = "20;20";

            var result = mainWindow.ParsePoint(input);

            Assert.Equal(20, result.X);
            Assert.Equal(20, result.Y);
        }

        [Fact]
        public void ParseColor_Should_Return_Correct_Color()
        {
            var mainWindow = new DrawingService(_logService);
            string input = "255;128;64;32"; // ARGB

            var result = mainWindow.ParseColor(input);

            Assert.Equal(255, result.A);
            Assert.Equal(128, result.R);
            Assert.Equal(64, result.G);
            Assert.Equal(32, result.B);
        }
    }
}