namespace VectorGraphicViewer.Interface
{
    public interface IShapeLoaderService
    {
        List<IShape> LoadShapes(string filePath);
    }
}
