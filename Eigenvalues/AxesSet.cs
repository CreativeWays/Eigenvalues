using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Eigenvalues
{
    public partial class MainWindow
    {
        public void SetAxes(double wxmin, double wxmax, double wymin, double wymax, double xstep, double ystep)
        {
            // Get the tic mark lengths.
            Point p0 = AxesConverter.DtoW(new Point(0, 0));
            Point p1 = AxesConverter.DtoW(new Point(2, 2));
            double xtic = p1.X - p0.X;
            double ytic = p1.Y - p0.Y;

            // Make the X axis.
            GeometryGroup xaxisGeom = new GeometryGroup();
            p0 = new Point(wxmin, 0);
            p1 = new Point(wxmax, 0);
            // Draw the line that starts in wxmin and ends in wxmax on Ox
            xaxisGeom.Children.Add(
                new LineGeometry(
                    AxesConverter.WtoD(p0), AxesConverter.WtoD(p1)
                    )
                );
            // Draw the tic marks and they labels
            for (double x = xstep; x <= wxmax - xstep; x += xstep)
            {
                // Add the tic mark.
                Point tic0 = AxesConverter.WtoD(new Point(x, -ytic));
                Point tic1 = AxesConverter.WtoD(new Point(x, ytic));
                xaxisGeom.Children.Add(new LineGeometry(tic0, tic1));

                // Label the tic mark's X coordinate.
                DrawText(canGraph, (x/10).ToString(),
                    new Point(tic0.X, tic0.Y + 5), 0, 12,
                    HorizontalAlignment.Center,
                    VerticalAlignment.Top);
            }

            // Combine all lines (tic marks and Ox-axe) to 1 Path object that has color, thickness and other properties
            Path xaxisPath = new Path();
            xaxisPath.StrokeThickness = 1;
            xaxisPath.Stroke = Brushes.Black;
            xaxisPath.Data = xaxisGeom;

            canGraph.Children.Add(xaxisPath);

            // Make the Y axis.
            GeometryGroup yaxisGeom = new GeometryGroup();
            p0 = new Point(0, wymin);
            p1 = new Point(0, wymax);
            xaxisGeom.Children.Add(
                new LineGeometry(
                    AxesConverter.WtoD(p0), AxesConverter.WtoD(p1)
                    )
                );

            for (double y = ystep; y <= wymax - ystep; y += ystep)
            {
                // Add the tic mark.
                Point tic0 = AxesConverter.WtoD(new Point(-xtic, y));
                Point tic1 = AxesConverter.WtoD(new Point(xtic, y));
                xaxisGeom.Children.Add(new LineGeometry(tic0, tic1));

                // Label the tic mark's Y coordinate.
                DrawText(canGraph, (y/10).ToString(),
                    new Point(tic0.X - 10, tic0.Y), -90, 12,
                    HorizontalAlignment.Center,
                    VerticalAlignment.Center);
            }

            Path yaxisPath = new Path();
            yaxisPath.StrokeThickness = 1;
            yaxisPath.Stroke = Brushes.Black;
            yaxisPath.Data = yaxisGeom;

            canGraph.Children.Add(yaxisPath);

            /*GeometryGroup directrix = new GeometryGroup();
            p0 = new Point(0, 0);
            p1 = new Point(wxmax, wymax);
            directrix.Children.Add(
                new LineGeometry(
                    AxesConverter.WtoD(p0), AxesConverter.WtoD(p1)
                    )
                );
            Path directrixPath = new Path();
            directrixPath.StrokeThickness = 1;
            directrixPath.Stroke = Brushes.Blue;
            directrixPath.Data = directrix;

            canGraph.Children.Add(directrixPath);

            GeometryGroup directrY = new GeometryGroup();
            p0 = new Point(0, 0);
            p1 = new Point(-wxmax, -wymax);
            directrY.Children.Add(
                new LineGeometry(
                    AxesConverter.WtoD(p0), AxesConverter.WtoD(p1)
                    )
                );

            Path directrYPath = new Path();
            directrYPath.StrokeThickness = 1;
            directrYPath.Stroke = Brushes.Blue;
            directrYPath.Data = directrY;
            
            canGraph.Children.Add(directrYPath);*/
        }
    }
}
