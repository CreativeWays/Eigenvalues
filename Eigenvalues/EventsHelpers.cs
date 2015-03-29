using System.Windows;

namespace Eigenvalues
{
    public partial class MainWindow
    {
        // Find the data point at this device coordinate location.
        // Return data_set = -1 if there is no point at this location.
        private void FindDataPoint(Point location, out int data_set, out int point_number)
        {
            // Check each data set.
            for (data_set = 0; data_set < DataPoints.Length; data_set++)
            {
                // Check this data set.
                for (point_number = 0;
                    point_number < DataPoints[data_set].Count;
                    point_number++)
                {
                    // See how far the location is from the data point.
                    Point data_point = DataPoints[data_set][point_number];
                    Vector vector = location - data_point;
                    double dist = vector.Length;
                    if (dist < 3) return;
                }
            }

            // We didn't find a point at this location.
            data_set = -1;
            point_number = -1;
        }
    }
}
