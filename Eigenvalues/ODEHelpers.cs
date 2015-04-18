using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eigenvalues
{
    static class ODEHelpers
    {
        public static void fillLeapArrays(double alpha, double dx, out int[] leapArr, out int[] savePointArr)
        {
            leapArr = new int[4];
            savePointArr = new int[6];
            // ----------------
            leapArr[0] = 1;
            leapArr[1] = Convert.ToInt32(1 / dx) + 1;
            leapArr[2] = Convert.ToInt32(alpha / dx) + 1;
            leapArr[3] = Convert.ToInt32((1 + alpha) / dx) + 1;

            // ----------------
            savePointArr[0] = 0;
            savePointArr[1] = Convert.ToInt32(1 / dx);
            savePointArr[2] = 1;
            savePointArr[3] = Convert.ToInt32(alpha / dx);
            savePointArr[4] = Convert.ToInt32((1 + alpha) / dx);
            savePointArr[5] = Convert.ToInt32(alpha / dx) + 1;
        }
        public static void RandomInit(out Vector[] vectors, int n, int dim, double radius)
        {
            vectors = VectorHelpers.CreateVectorArray(n, dim);
            const double counter = 1.5;
            int lineDimention = Convert.ToInt32(Math.Sqrt(n));
            double step = counter / lineDimention;
            double along = 0.0;
            double across = 0.0;
            for (int i = 0; i < lineDimention; ++i)
            {
                across = 0;
                for (int j = 0; j < lineDimention; ++j)
                {
                    vectors[i * lineDimention + j][0] = 0;
                    vectors[i * lineDimention + j][dim - 1] = 0;
                    vectors[i * lineDimention + j][1] = along;
                    vectors[i * lineDimention + j][2] = across;
                    across += step;
                }
                along += step;
            }
        }

        public static int Contains(int[] arr, int number)
        {
            for (int i = 0; i < arr.Length; i++)
                if (arr[i] == number) return i;
            return -1;
        }
        
    }
}
