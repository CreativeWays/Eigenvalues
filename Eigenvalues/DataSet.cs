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
                GeometryGroup xaxis_geom1 = new GeometryGroup();

                for (double x = 0; x <= 100; x += 10)
                {
                    last_y += rand.Next(-10, 10) / 10.0;
                    if (last_y < 0) last_y = 0;
                    if (last_y > 10) last_y = 10;
                    Point p = new Point(x, last_y);
                    DataPoints[data_set].Add(WtoD(p));
                    xaxis_geom1.Children.Add(
                        new EllipseGeometry(WtoD(p), 3, 3)
                        );
                }                

                /*
                Polyline polyline = new Polyline();
                polyline.StrokeThickness = 1;
                polyline.Stroke = DataBrushes[data_set];
                polyline.Points = DataPoints[data_set];

                canGraph.Children.Add(polyline);
                */
                Path xaxis_path1 = new Path();
                xaxis_path1.Fill = Brushes.Black;
                xaxis_path1.Stroke = Brushes.Black;
                xaxis_path1.Data = xaxis_geom1;

                canGraph.Children.Add(xaxis_path1);                
            }
            
        }
    }
}
