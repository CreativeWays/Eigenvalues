using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Eigenvalues
{
    public static class RungeKutta
    {
        //public delegate Vector SmallRkDelegate(double x, Vector y);

        private static double _sixth = 1.0 / 6.0;

        public static void MakeStep(ref Vector y, double d, double dx, double dim, InputData.FuncDel f)
        {
            double halfdx = 0.5 * dx;

            Vector k1 = dx * f(0, y);
            Vector k2 = dx * f(0 + halfdx, y + k1 * halfdx);
            Vector k3 = dx * f(0 + halfdx, y + k2 * halfdx);

            Vector k4 = dx * f(0 + dx, y + k3 * dx);

            y = y + (_sixth * (k1 + 2 * k2 + 2 * k3 + k4));
        }

        // Leaps
        public static Vector Y1Func(Vector[] savedPoints, double alpha, double beta)
        {
            return ((alpha - 1)/(alpha - beta - 1)) * savedPoints[0];
        }
        public static Vector Y2Func(Vector[] savedPoints, double alpha, double beta)
        {
            return savedPoints[1] - (alpha / (alpha - 1)) * savedPoints[2];
        }
        public static Vector Y3Func(Vector[] savedPoints, double alpha, double beta)
        {
            return (1 + beta) * savedPoints[3];
        }
        public static Vector Y4Func(Vector[] savedPoints, double alpha, double beta)
        {
            return savedPoints[4] - (alpha / (1 + beta)) * savedPoints[5];
        }
    }
}
