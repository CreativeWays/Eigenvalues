using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eigenvalues
{
    static class VectorHelpers
    {
        public static List<Vector> FindUnicVectors(InputData inputData, List<Vector> fullVectorsSet = null)
        {
            if (fullVectorsSet == null)
                return null;

            List<Vector> uniqVectorLists = new List<Vector>();

            foreach (Vector vector in fullVectorsSet)
            {
                if (isAlreadyHave(uniqVectorLists, vector) == -1)
                    uniqVectorLists.Add(vector.Clone());
            }

            return uniqVectorLists;
        }

        public static int isAlreadyHave(List<Vector> equilibriums, Vector arrForCheck)
        {
            int counter = 0;
            foreach (var vector in equilibriums)
            {
                if (arrForCheck.IsEqualsTo(vector, 0.05))
                    return counter;
                counter++;
            }
            return -1;
        }
        public static int isAlreadyHave(List<Vector> equilibriums, Vector arrForCheck, int index)
        {
            bool isEqual = true;
            int counter = 0;
            foreach (var state in equilibriums)
            {
                if (counter != index)
                {
                    isEqual = true;
                    for (int i = 0; i < arrForCheck.Size; i++)
                    {
                        if (Math.Abs(arrForCheck[i] - state[i]) > 0.05)
                            isEqual = false;
                    }
                    if (isEqual)
                        return counter;
                }
                counter++;
            }
            return -1;
        }

        public static Vector[] CreateVectorArray(int membersCount, int dimmention)
        {
            Vector[] vectors = new Vector[membersCount];
            for (int i = 0; i < membersCount; i++)
            {
                vectors[i] = new Vector(dimmention);
            }
            return vectors;
        }
        public static Vector[] CreateCopyOfVectorArray(Vector[] vectorToCopy)
        {
            int arrSize = vectorToCopy.Length;
            Vector[] vectors = new Vector[arrSize];
            for (int i = 0; i < arrSize; i++)
            {
                vectors[i] = vectorToCopy[i].Clone();
            }
            return vectors;
        }
    }
}
