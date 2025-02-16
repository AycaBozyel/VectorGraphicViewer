namespace VectorGraphicViewer.Interface
{
    public interface IShape
    {
        public string Type { get; set; }
        public string Color { get; set; }
        public double GetMaxX();
        public double GetMaxY();
    }
}
