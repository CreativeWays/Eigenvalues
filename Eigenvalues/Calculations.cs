using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eigenvalues
{
    static class Calculations
    {
        // -----------------------
        public static IEnumerable<string> CalculateAndPrintAsymptoticSolutions(InputData inputData)
        {
            int dimension = inputData.Dimension;
            double alpha = inputData.Alpha;
            double delta = inputData.Delta;

            // Checking states with asymbtotic formula
            // There is 2 options: 1. when (a, b) in U1 2. when (a, b) in U2
            // In out case (when a = 1.185, b = 0.05): (a, b) in U2 ('couse 0 < 0.05 < 0.15). Thats why we gonna use this system: http://i.imgur.com/uz92E8S.png
            yield return "asymptotic calculations:";
            double[,] asymptoteArray = new double[dimension - 1, dimension - 1];
            for (int k0 = 0; k0 < dimension - 1; k0++)
            {
                for (int j = 1; j < dimension - 1; j++)
                {
                    if (j <= k0)
                    {
                        asymptoteArray[k0, j - 1] = -(alpha - 1) * Math.Log(1 / delta) - (alpha - 1) * Math.Log(k0 + 1 - j) +
                                                    alpha * Math.Log(alpha - 1);
                    }
                    else
                    {
                        asymptoteArray[k0, j - 1] = (alpha - 1) * Math.Log(1 / delta) + (alpha - 1) * Math.Log(j - k0) - alpha * Math.Log(alpha - 1);
                    }
                }
                yield return string.Format("({0}, {1})", asymptoteArray[k0, inputData.XOnGraph], asymptoteArray[k0, inputData.YOnGraph]);
            }
        }

        public static IEnumerable<string> PrintAllPoints(InputData inputData, List<Vector> fullVectorsSet = null)
        {
            if (fullVectorsSet == null)
            {
                yield return "PrintAllPoints is faild 'cause there is no elements in fullVectorsSet";
                yield break;
            }
            foreach (Vector v in fullVectorsSet)
                yield return String.Format("x: {0}, y: {1}", v[inputData.XOnGraph], v[inputData.YOnGraph]);
        }

        public static IEnumerable<string> FindFixedPoints(InputData inputData, List<Vector> fullVectorsSet = null)
        {
            if (fullVectorsSet == null)
            {
                yield return "FindFixedPoints is faild 'cause there is no elements in fullVectorsSet";
                yield break;
            }

            foreach (Vector vector in VectorHelpers.FindUnicVectors(inputData, fullVectorsSet))
            {
                yield return String.Format("x: {0}, y: {1}",
                    vector[inputData.XOnGraph], vector[inputData.YOnGraph]);
            }
        }

        public static IEnumerable<string> AnalyseEquilibriumStates(InputData inputData,
            List<Vector> fullVectorsSet = null)
        {
            if (fullVectorsSet == null)
            {
                yield return "AnalyseEquilibriumStates is faild 'cause there is no elements in fullVectorsSet";
                yield break;
            }

            List<Vector> equilibriumStatesn = new List<Vector>(),
                recurrentStates = new List<Vector>();
            List<int> equilibriumStatesAmount = new List<int>();
            int countDotsInRange = 0;
            
            foreach (Vector vector in equilibriumStatesn)
            {
                bool isThisDotInARange = vector.IsStayInCircle(2.0);

                // Possibly it's recursive
                if (vector[0] != -1)
                {
                    // Probably there is a point that was already defind in common vector array
                    // But first of all we add this point to recursive array that contains uniq recursive points
                    int answer = VectorHelpers.isAlreadyHave(recurrentStates, vector);
                    if (answer == -1)
                        recurrentStates.Add(vector);
                }
                // but let's hope that it's fixed point
                else
                {
                    int answer = VectorHelpers.isAlreadyHave(equilibriumStatesn, vector);
                    if (answer == -1)
                    {
                        equilibriumStatesn.Add(vector);
                        equilibriumStatesAmount.Add(0);
                    }
                    else
                        equilibriumStatesAmount[answer]++;
                }

                if (isThisDotInARange)
                    countDotsInRange++;
            }

            // Output
            yield return "Count of dots that does maintains in initial radius: " + countDotsInRange;
            yield return "Fixed points:";
            for (int i = 0; i < equilibriumStatesn.Count; i++)
            {
                yield return string.Format("({0}, {1}) - {2}%",
                    Math.Round(equilibriumStatesn[i][inputData.XOnGraph], 2),
                    Math.Round(equilibriumStatesn[i][inputData.YOnGraph], 2),
                    Convert.ToInt32(((double)equilibriumStatesAmount[i] / inputData.TaskCount) * 100));
            }

            yield return "Periodic points:";
            if (recurrentStates.Count == 0)
                yield return "Не найдены";
            else
                yield return "Найдены (WTF?)";
        }
    }
}
