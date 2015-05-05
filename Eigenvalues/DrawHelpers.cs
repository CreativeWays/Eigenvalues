using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace Eigenvalues
{
    public partial class MainWindow
    {
        // Position a label at the indicated point.
        private void DrawText(Canvas can, string text,
            Point location, double angle, double font_size,
            HorizontalAlignment halign, VerticalAlignment valign)
        {
            // Make the label.
            Label label = new Label();
            label.Content = text;
            label.FontSize = font_size;
            can.Children.Add(label);

            // Rotate if desired.
            if (angle != 0) label.LayoutTransform = new RotateTransform(angle);

            // Position the label.
            label.Measure(new Size(double.MaxValue, double.MaxValue));

            double x = location.X;
            if (halign == HorizontalAlignment.Center)
                x -= label.DesiredSize.Width / 2;
            else if (halign == HorizontalAlignment.Right)
                x -= label.DesiredSize.Width;
            Canvas.SetLeft(label, x);

            double y = location.Y;
            if (valign == VerticalAlignment.Center)
                y -= label.DesiredSize.Height / 2;
            else if (valign == VerticalAlignment.Bottom)
                y -= label.DesiredSize.Height;
            Canvas.SetTop(label, y);
        }
        private void DrawPoints(OutputTypes outputType, InputData inputData, out List<Vector> fullVectorsSet)
        {
            ODESolver result = new ODESolver();
            List<string> output;
            Path pointsPath = new Path();
            pointsPath.Fill = Brushes.Black;
            pointsPath.Stroke = Brushes.Black;
            pointsPath.Data = result.Run(outputType, inputData, out fullVectorsSet, out output);
            // Draw Point
            canGraph.Children.Add(pointsPath);
            // Print Output
            Print(output);
        }

        public static EllipseGeometry CreateEllipseGeometry(double x, double y)
        { 
            return new EllipseGeometry(AxesConverter.WtoD(
                new Point(x*10, y*10)), // point position
                3, 3 // radius
                );
        }
    }
}
