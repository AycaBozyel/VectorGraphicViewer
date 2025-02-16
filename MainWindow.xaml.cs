using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using VectorGraphicViewer.Interface;
using VectorGraphicViewer.Logging;

namespace VectorGraphicViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IShapeLoaderService shapeLoader;
        private readonly IDrawingService drawingService;
        private readonly ILogService logService;

        private readonly string fileName = "shapes.json";

        private double zoomFactor = 1.0;
        private readonly TranslateTransform translateTransform = new TranslateTransform();
        private readonly ScaleTransform scaleTransform = new ScaleTransform();
        private readonly TransformGroup transformGroup = new TransformGroup();

        private bool isPanning = false;
        private Point lastMousePosition;
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(IShapeLoaderService shapeLoader, IDrawingService drawingService, ILogService logService) : this()
        {
            InitializeComponent();

            this.shapeLoader = shapeLoader;
            this.drawingService = drawingService;
            this.logService = logService;

            SetupTransforms();
            LoadAndDrawShapes();
        }

        public void SetupTransforms()
        {
            transformGroup.Children.Add(translateTransform);
            transformGroup.Children.Add(scaleTransform);

            DrawingCanvas.RenderTransform = transformGroup;
        }

        private void LoadAndDrawShapes()
        {
            if (shapeLoader != null)
            {
                try
                {
                    var shapes = shapeLoader.LoadShapes(fileName);
                    if (shapes == null || !shapes.Any()) return;

                    drawingService?.DrawShapes(DrawingCanvas, shapes);
                }
                catch (Exception ex)
                {
                    logService.LogError("Shapes could not be loaded and drawn", ex);
                }
            }
        }
        
        private void ZoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (e.NewValue != zoomFactor)
            {
                zoomFactor = e.NewValue;
                ApplyZoom();
            }
        }

        private void ApplyZoom()
        {
            scaleTransform.ScaleX = zoomFactor;
            scaleTransform.ScaleY = zoomFactor;

            RedrawShapes();
        }

        private void RedrawShapes()
        {
            DrawingCanvas.Children.Clear();
            LoadAndDrawShapes();
        }

        private void DrawingCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isPanning = true;
            lastMousePosition = e.GetPosition(DrawingCanvas);
            DrawingCanvas.CaptureMouse();
        }

        private void DrawingCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPanning)
            {
                Point currentMousePosition = e.GetPosition(DrawingCanvas);
                translateTransform.X += currentMousePosition.X - lastMousePosition.X;
                translateTransform.Y += currentMousePosition.Y - lastMousePosition.Y;
                lastMousePosition = currentMousePosition;
            }
        }

        private void DrawingCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isPanning = false;
            DrawingCanvas.ReleaseMouseCapture();
        }
    }
}