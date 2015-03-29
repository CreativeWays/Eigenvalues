using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Input;

namespace Eigenvalues
{
    public partial class MainWindow
    {
        // See if the mouse is over a data point.
        private void canGraph_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // Find the data point at the mouse's location.
            Point mouse_location = e.GetPosition(canGraph);
            int data_set, point_number;
            FindDataPoint(mouse_location, out data_set, out point_number);
            if (data_set < 0) return;
            Point data_point = DataPoints[data_set][point_number];

            // Make the data ellipse if we haven't already.
            if (DataEllipse == null)
            {
                DataEllipse = new Ellipse();
                DataEllipse.Fill = null;
                DataEllipse.StrokeThickness = 1;
                DataEllipse.Width = 7;
                DataEllipse.Height = 7;
                canGraph.Children.Add(DataEllipse);
            }

            // Color and position the ellipse.
            DataEllipse.Stroke = DataBrushes[data_set];
            Canvas.SetLeft(DataEllipse, data_point.X - 3);
            Canvas.SetTop(DataEllipse, data_point.Y - 3);

            // Make the data label if we haven't already.
            if (DataLabel == null)
            {
                DataLabel = new Label();
                DataLabel.FontSize = 12;
                canGraph.Children.Add(DataLabel);
            }

            // Convert the data values back into world coordinates.
            Point world_point = DtoW(data_point);

            // Set the data label's text and position it.
            DataLabel.Content = "(" +
                world_point.X.ToString("0.0") + ", " +
                world_point.Y.ToString("0.0") + ")";
            DataLabel.Measure(new Size(double.MaxValue, double.MaxValue));
            Canvas.SetLeft(DataLabel, data_point.X + 4);
            Canvas.SetTop(DataLabel, data_point.Y - DataLabel.DesiredSize.Height);
        }

        // Change the mouse cursor appropriately.
        private void canGraph_MouseMove(object sender, MouseEventArgs e)
        {
            // Find the data point at the mouse's location.
            Point mouse_location = e.GetPosition(canGraph);
            int data_set, point_number;
            FindDataPoint(mouse_location, out data_set, out point_number);

            // Display the appropriate cursor.
            if (data_set < 0)
                canGraph.Cursor = null;
            else
                canGraph.Cursor = Cursors.UpArrow;
        }
    }
}
