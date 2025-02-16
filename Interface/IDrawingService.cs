using System.Windows.Controls;

namespace VectorGraphicViewer.Interface
{
    public interface IDrawingService
    {
        public void DrawShapes(Canvas canvas, List<IShape> shapes);
    }
}
