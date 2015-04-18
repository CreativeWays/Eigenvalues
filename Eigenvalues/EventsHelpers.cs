using System;
using System.Windows;

namespace Eigenvalues
{
    public partial class MainWindow
    {
        private double FindDistanceBetweenVAndP (Vector vector, Point point)
        {
            Point tempPoint = new Point();
            tempPoint.X = vector[_inputData.XOnGraph] - point.X;
            tempPoint.Y = vector[_inputData.YOnGraph] - point.Y;

            double distance = Math.Pow(tempPoint.X, 2) + Math.Pow(tempPoint.Y, 2);
            return Math.Sqrt(distance);
        }
        // Find the data point at this device coordinate location.
        // Return data_set = -1 if there is no point at this location.
        private void FindDataPoint(Point location, out int data_set)
        {
            if (_fullVectorsSet == null)
            {
                data_set = -1;
                return;
            }                

            // Check each data set.
            for (data_set = 0; data_set < _fullVectorsSet.Capacity; data_set++)
            {
                // See how far the location is from the data point.
                Vector data_point = _fullVectorsSet[data_set];
                double distance = FindDistanceBetweenVAndP(data_point, location);
                if (distance < 4) return;
            }

            // We didn't find a point at this location.
            
        }
    }
}
