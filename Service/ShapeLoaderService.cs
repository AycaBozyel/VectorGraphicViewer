using VectorGraphicViewer.Interface;
using VectorGraphicViewer.Util;

namespace VectorGraphicViewer.Service
{
    public class ShapeLoaderService : IShapeLoaderService
    {
        public List<IShape> LoadShapes(string fileName)
        {
            return ShapeReader.LoadShapes(fileName);
        }
    }
}
