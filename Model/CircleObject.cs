namespace VectorGraphicViewer.Model
{
    public class CircleObject : ShapeBase
    {
        public string Center { get; set; }
        public double Radius { get; set; }

        public override double GetMaxX()
        {
            var center = Center.Split(';').Select(double.Parse).ToArray();
            return center[0] + Radius;
        }

        public override double GetMaxY()
        {
            var center = Center.Split(';').Select(double.Parse).ToArray();
            return center[1] + Radius;
        }
    }
}
