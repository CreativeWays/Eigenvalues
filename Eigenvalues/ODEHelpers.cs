using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eigenvalues
{
    static class ODEHelpers
    {
        public static void FillLeapArrays(double alpha, double dx, out int[] leapArr, out int[] savePointArr)
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
        public static void RandomInit(out Vector[] vectors, InputData inputData)
        {
            Random rnd = new Random();
            int n = inputData.TaskCount;
            int dim = inputData.Dimension;
            double radius = inputData.Radius;
            
            vectors = VectorHelpers.CreateVectorArray(n, dim);
            int lineDimention = Convert.ToInt32(Math.Sqrt(n));
            double step = (radius*2) / ((dim - 2 > 1) ? lineDimention : n);
            double along = -radius;
            double across = -radius;
            for (int i = 0; i < lineDimention; ++i)
            {
                if (dim - 2 > 1)
                    along = -radius;
                for (int j = 0; j < lineDimention; ++j)
                {
                    for (int k = 1; k < dim - 1; ++k)
                    {
                        if (k == inputData.XOnGraph && inputData.XOnGraph != 0)
                            vectors[i * lineDimention + j][k] = across;
                        else
                            if (k == inputData.YOnGraph && inputData.YOnGraph != 0)
                                vectors[i * lineDimention + j][k] = along;
                            else
                                vectors[i * lineDimention + j][k] = rnd.NextDouble();
                        
                    }
                    vectors[i * lineDimention + j][0] = 0;
                    vectors[i * lineDimention + j][dim - 1] = 0;

                    along += step;
                }
                across += step;
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
