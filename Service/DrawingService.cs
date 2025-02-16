using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using VectorGraphicViewer.Interface;
using VectorGraphicViewer.Logging;
using VectorGraphicViewer.Model;

namespace VectorGraphicViewer.Service
{
    public class DrawingService : IDrawingService
    {
        private readonly ILogService _logService;

        private const double windowWidth = 500.0;
        private const double windowHeight = 500.0;

        private double scaleX, scaleY;
        private double maxX, maxY;

        public DrawingService(ILogService logService)
        {
            _logService = logService;
        }

        public void DrawShapes(Canvas canvas, List<IShape> shapes)
        {
            CalculateScaleFactors(shapes);

            canvas.Children.Clear();
            foreach (var shape in shapes)
            {
                switch (shape)
                {
                    case LineObject line:
                        DrawLine(canvas, line, scaleX, scaleY);
                        break;
                    case CircleObject circle:
                        DrawCircle(canvas, circle, scaleX);
                        break;
                    case TriangleObject triangle:
                        DrawTriangle(canvas, triangle, scaleX, scaleY);
                        break;
                    case RectangleObject rectangle:
                        DrawRectangle(canvas, rectangle, scaleX, scaleY);
                        break;
                }
            }
        }

        private void CalculateScaleFactors(List<IShape> shapes)
        {
            maxX = shapes.Max(s => s.GetMaxX());
            maxY = shapes.Max(s => s.GetMaxY());

            scaleX = windowWidth / maxX;
            scaleY = windowHeight / maxY;
        }

        private void DrawLine(Canvas canvas, LineObject line, double scaleX, double scaleY)
        {
            try
            {
                var start = ScalePoint(ParsePoint(line.A), scaleX, scaleY);
                var end = ScalePoint(ParsePoint(line.B), scaleX, scaleY);

                var wpfLine = new Line
                {
                    X1 = start.X,
                    Y1 = start.Y,
                    X2 = end.X,
                    Y2 = end.Y,
                    Stroke = new SolidColorBrush(ParseColor(line.Color)),
                    StrokeThickness = 1
                };

                canvas.Children.Add(wpfLine);
            }
            catch (Exception ex)
            {
                _logService.LogError("ERROR: The Line could not drawn" , ex);
            }
        }

        private void DrawCircle(Canvas canvas, CircleObject circle, double scaleX)
        {
            try
            {
                var center = ScalePoint(ParsePoint(circle.Center), scaleX, scaleX);
                double radius = circle.Radius * scaleX;

                var ellipse = new Ellipse
                {
                    Width = radius * 2,
                    Height = radius * 2,
                    Stroke = new SolidColorBrush(ParseColor(circle.Color)),
                    StrokeThickness = 1,
                    Fill = circle.Filled ? new SolidColorBrush(ParseColor(circle.Color)) : null
                };

                Canvas.SetLeft(ellipse, center.X - radius);
                Canvas.SetTop(ellipse, center.Y - radius);
                canvas.Children.Add(ellipse);
            }
            catch(Exception ex)
            {
                _logService.LogError("ERROR: The Circle could not drawn", ex);
            }
        }

        private void DrawTriangle(Canvas canvas, TriangleObject triangle, double scaleX, double scaleY)
        {
            try
            {
                var points = new PointCollection
            {
                ScalePoint(ParsePoint(triangle.A), scaleX, scaleY),
                ScalePoint(ParsePoint(triangle.B), scaleX, scaleY),
                ScalePoint(ParsePoint(triangle.C), scaleX, scaleY)
            };

                var polygon = new Polygon
                {
                    Points = points,
                    Stroke = new SolidColorBrush(ParseColor(triangle.Color)),
                    StrokeThickness = 1,
                    Fill = triangle.Filled ? new SolidColorBrush(ParseColor(triangle.Color)) : null
                };

                canvas.Children.Add(polygon);
            }
            catch (Exception ex) 
            {
                _logService.LogError("ERROR: The Triangle could not drawn", ex);
            }

        }

        private void DrawRectangle(Canvas canvas, RectangleObject rectangleObj, double scaleX, double scaleY)
        {
            try
            {
                var a = ScalePoint(ParsePoint(rectangleObj.A), scaleX, scaleY);
                var b = ScalePoint(ParsePoint(rectangleObj.B), scaleX, scaleY);

                var width = Math.Abs(b.X - a.X);
                var height = Math.Abs(b.Y - a.Y);

                var rectangle = new Rectangle
                {
                    Width = width,
                    Height = height,
                    Stroke = new SolidColorBrush(ParseColor(rectangleObj.Color)),
                    StrokeThickness = 1,
                    Fill = rectangleObj.Filled ? new SolidColorBrush(ParseColor(rectangleObj.Color)) : null
                };

                Canvas.SetLeft(rectangle, Math.Min(a.X, b.X));
                Canvas.SetTop(rectangle, Math.Min(a.Y, b.Y));

                canvas.Children.Add(rectangle);
            }catch(Exception ex)
            {
                _logService.LogError("ERROR: The Rectangle could not drawn", ex);
            }

        }

        public Point ParsePoint(string str)
        {
            var split = str.Split(';');
            return new Point(double.Parse(split[0], CultureInfo.InvariantCulture), double.Parse(split[1], CultureInfo.InvariantCulture));
        }

        private Point ScalePoint(Point point, double scaleX, double scaleY)
        {
            return new Point(point.X * scaleX, ConvertToYAxis(point.Y * scaleY));
        }

        public Color ParseColor(string color)
        {
            var rgba = color.Split(';');
            return Color.FromArgb(byte.Parse(rgba[0]), byte.Parse(rgba[1]), byte.Parse(rgba[2]), byte.Parse(rgba[3]));
        }

        private static double ConvertToYAxis(double Y)
        {
            return windowHeight - Y; 
        }
    }

}
