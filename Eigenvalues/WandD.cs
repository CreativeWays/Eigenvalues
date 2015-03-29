using System.Windows;
using System.Windows.Media;

namespace Eigenvalues
{
    public partial class MainWindow
    {
        // Prepare values for perform transformations.
        private Matrix WtoDMatrix, DtoWMatrix;
        private void PrepareTransformations(
            double wxmin, double wxmax, double wymin, double wymax,
            double dxmin, double dxmax, double dymin, double dymax)
        {
            // Make WtoD.
            WtoDMatrix = Matrix.Identity;
            WtoDMatrix.Translate(-wxmin, -wymin);

            double xscale = (dxmax - dxmin) / (wxmax - wxmin);
            double yscale = (dymax - dymin) / (wymax - wymin);
            WtoDMatrix.Scale(xscale, yscale);

            WtoDMatrix.Translate(dxmin, dymin);

            // Make DtoW.
            DtoWMatrix = WtoDMatrix;
            DtoWMatrix.Invert();
        }

        // Transform a point from world to device coordinates.
        private Point WtoD(Point point)
        {
            return WtoDMatrix.Transform(point);
        }

        // Transform a point from device to world coordinates.
        private Point DtoW(Point point)
        {
            return DtoWMatrix.Transform(point);
        }
    }
}
