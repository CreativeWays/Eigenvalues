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
            int data_set;
            FindDataPoint(mouse_location, out data_set);
            if (data_set < 0) return;
            Point data_point = _fullVectorsSet[data_set].ToPoint(_inputData.XOnGraph, _inputData.YOnGraph);

            // Make the data ellipse if we haven't already.
            if (_dataEllipse == null)
            {
                _dataEllipse = new Ellipse();
                _dataEllipse.Fill = null;
                _dataEllipse.StrokeThickness = 1;
                _dataEllipse.Width = 7;
                _dataEllipse.Height = 7;
                canGraph.Children.Add(_dataEllipse);
            }

            // Color and position the ellipse.
            _dataEllipse.Stroke = _dataBrushes[data_set];
            Canvas.SetLeft(_dataEllipse, data_point.X - 3);
            Canvas.SetTop(_dataEllipse, data_point.Y - 3);

            // Make the data label if we haven't already.
            if (_dataLabel == null)
            {
                _dataLabel = new Label();
                _dataLabel.FontSize = 12;
                canGraph.Children.Add(_dataLabel);
            }

            // Convert the data values back into world coordinates.
            Point world_point = AxesConverter.DtoW(data_point);

            // Set the data label's text and position it.
            _dataLabel.Content = "(" +
                world_point.X.ToString("0.0") + ", " +
                world_point.Y.ToString("0.0") + ")";
            _dataLabel.Measure(new Size(double.MaxValue, double.MaxValue));
            Canvas.SetLeft(_dataLabel, data_point.X + 4);
            Canvas.SetTop(_dataLabel, data_point.Y - _dataLabel.DesiredSize.Height);
        }

        // Change the mouse cursor appropriately.
        private void canGraph_MouseMove(object sender, MouseEventArgs e)
        {
            // Find the data point at the mouse's location.
            Point mouse_location = e.GetPosition(canGraph);
            int data_set;
            FindDataPoint(mouse_location, out data_set);

            // Display the appropriate cursor.
            if (data_set < 0)
                canGraph.Cursor = null;
            else
                canGraph.Cursor = Cursors.UpArrow;
        }
    }
}
