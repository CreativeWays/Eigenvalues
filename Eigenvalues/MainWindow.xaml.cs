using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Eigenvalues
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // The data.
        private PointCollection[] DataPoints = new PointCollection[3];
        private Brush[] DataBrushes = { Brushes.Red, Brushes.Green, Brushes.Blue };

        // To mark a clicked point.
        private Ellipse DataEllipse = null;
        private Label DataLabel = null;

        // Draw a simple graph.
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            double wxmin = -10;
            double wxmax = 110;
            double wymin = -1;
            double wymax = 11;
            const double xstep = 10;
            const double ystep = 1;

            const double dmargin = 10;
            double dxmin = dmargin;
            double dxmax = canGraph.Width - dmargin;
            double dymin = dmargin;
            double dymax = canGraph.Height - dmargin;

            // Prepare the transformation matrices.
            PrepareTransformations(
                wxmin, wxmax, wymin, wymax,
                dxmin, dxmax, dymax, dymin);

            SetAxes(wxmin, wxmax, wymin, wymax, xstep, ystep);

            SetData();

            // Make a title
            Point title_location = WtoD(new Point(50, 10));
            DrawText(canGraph, "Amazing Data",
                title_location, 0, 20,
                HorizontalAlignment.Center,
                VerticalAlignment.Top);
        }
    }
}
