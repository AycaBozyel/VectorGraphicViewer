namespace VectorGraphicViewer.Model
{
    public class RectangleObject : ShapeBase
    {
        public string A { get; set; }
        public string B { get; set; }
       
        public override double GetMaxX()
        {
            var points = new[] { A, B }
               .Select(p => p.Split(';').Select(double.Parse).ToArray()[0]);
            return points.Max();
        }

        public override double GetMaxY()
        {
            var points = new[] { A, B }
               .Select(p => p.Split(';').Select(double.Parse).ToArray()[1]);
            return points.Max();
        }
    }
}
