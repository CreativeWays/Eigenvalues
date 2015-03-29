﻿using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Eigenvalues
{
    public partial class MainWindow
    {
        public void SetAxes(double wxmin, double wxmax, double wymin, double wymax, double xstep, double ystep)
        {
            // Get the tic mark lengths.
            Point p0 = DtoW(new Point(0, 0));
            Point p1 = DtoW(new Point(2, 2));
            double xtic = p1.X - p0.X;
            double ytic = p1.Y - p0.Y;

            // Make the X axis.
            GeometryGroup xaxis_geom = new GeometryGroup();
            p0 = new Point(wxmin, 0);
            p1 = new Point(wxmax, 0);
            xaxis_geom.Children.Add(new LineGeometry(WtoD(p0), WtoD(p1)));

            for (double x = xstep; x <= wxmax - xstep; x += xstep)
            {
                // Add the tic mark.
                Point tic0 = WtoD(new Point(x, -ytic));
                Point tic1 = WtoD(new Point(x, ytic));
                xaxis_geom.Children.Add(new LineGeometry(tic0, tic1));

                // Label the tic mark's X coordinate.
                DrawText(canGraph, x.ToString(),
                    new Point(tic0.X, tic0.Y + 5), 0, 12,
                    HorizontalAlignment.Center,
                    VerticalAlignment.Top);
            }

            Path xaxis_path = new Path();
            xaxis_path.StrokeThickness = 1;
            xaxis_path.Stroke = Brushes.Black;
            xaxis_path.Data = xaxis_geom;

            canGraph.Children.Add(xaxis_path);

            // Make the Y axis.
            GeometryGroup yaxis_geom = new GeometryGroup();
            p0 = new Point(0, wymin);
            p1 = new Point(0, wymax);
            xaxis_geom.Children.Add(new LineGeometry(WtoD(p0), WtoD(p1)));

            for (double y = ystep; y <= wymax - ystep; y += ystep)
            {
                // Add the tic mark.
                Point tic0 = WtoD(new Point(-xtic, y));
                Point tic1 = WtoD(new Point(xtic, y));
                xaxis_geom.Children.Add(new LineGeometry(tic0, tic1));

                // Label the tic mark's Y coordinate.
                DrawText(canGraph, y.ToString(),
                    new Point(tic0.X - 10, tic0.Y), -90, 12,
                    HorizontalAlignment.Center,
                    VerticalAlignment.Center);
            }

            Path yaxis_path = new Path();
            yaxis_path.StrokeThickness = 1;
            yaxis_path.Stroke = Brushes.Black;
            yaxis_path.Data = yaxis_geom;

            canGraph.Children.Add(yaxis_path);
        }
    }
}