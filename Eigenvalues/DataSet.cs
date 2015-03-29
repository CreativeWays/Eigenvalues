using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Eigenvalues
{
    public partial class MainWindow
    {
        // Make some data sets.
        public void SetData()
        {
            Random rand = new Random();
            for (int data_set = 0; data_set < 3; data_set++)
            {
                double last_y = rand.Next(3, 7);

                DataPoints[data_set] = new PointCollection();
                for (double x = 0; x <= 100; x += 10)
                {
                    last_y += rand.Next(-10, 10) / 10.0;
                    if (last_y < 0) last_y = 0;
                    if (last_y > 10) last_y = 10;
                    Point p = new Point(x, last_y);
                    DataPoints[data_set].Add(WtoD(p));
                }

                Polyline polyline = new Polyline();
                polyline.StrokeThickness = 1;
                polyline.Stroke = DataBrushes[data_set];
                polyline.Points = DataPoints[data_set];

                canGraph.Children.Add(polyline);
            }
        }
    }
}
