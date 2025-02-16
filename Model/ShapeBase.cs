using VectorGraphicViewer.Interface;

namespace VectorGraphicViewer.Model
{
    public abstract class ShapeBase : IShape
    {
        public string Color { get; set; } = "255;0;0;0"; // Default: Black (ARGB)
        public bool Filled { get; set; } = false;
        public string Type { get; set; }

        public abstract double GetMaxX();
        public abstract double GetMaxY();
    }
}