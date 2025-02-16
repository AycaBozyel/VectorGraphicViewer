namespace VectorGraphicViewer.Model
{
    public class LineObject : ShapeBase
    {
        public string A { get; set; }
        public string B { get; set; }

        public override double GetMaxX()
        {
            var a = A.Split(';').Select(double.Parse).ToArray();
            var b = B.Split(';').Select(double.Parse).ToArray();
            return Math.Max(a[0], b[0]);
        }

        public override double GetMaxY()
        {
            var a = A.Split(';').Select(double.Parse).ToArray();
            var b = B.Split(';').Select(double.Parse).ToArray();
            return Math.Max(a[1], b[1]);
        }
    }
}
