using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Eigenvalues
{
    class ODESolver
    {
        public ODESolver()
        { }

        // Here we getting all point and deciding which point to display
        // Input 
        public GeometryGroup Run(OutputTypes outputType, InputData inputData, List<Vector> fullVectorsSet, out List<string> output)
        {
            // ----------------
            GeometryGroup pointsGeometryGroup = new GeometryGroup();
            // Temp
            List<double> tempList = new List<double>();

            // ----------------
            // define T-count
            bool isThrSenceToGo = false;
            int tIterator = 0;
            int initialPointToInt = -Convert.ToInt32(inputData.InitialPoint / inputData.Dx);
            // Compute T0 and leap dots
            double T0 = inputData.Alpha + 1 + (inputData.Beta + 1) / (inputData.Alpha - inputData.Beta - 1);            
            // ----------------
            int iterationsCount = Convert.ToInt32(T0 / inputData.Dx);
            iterationsCount = Convert.ToInt32(iterationsCount / inputData.ContinueNum);

            fullVectorsSet = new List<Vector>();
            Vector[] savedPoints;

            // ----------------
            int[] leapArr;
            int[] savePointArr;
            ODEHelpers.FillLeapArrays(inputData.Alpha, inputData.Dx, out leapArr, out savePointArr);

            // ----------------
            Vector[] startingPoints, vectorsAfterFirstIterationList = null, previousVectorsList = null;
            ODEHelpers.RandomInit(out startingPoints, inputData);

            // ----------------
            // Output
            output = new List<string>();
            output.Add("Mode: " + outputType);
            output.Add("Computing has started...");

            // ----------------
            int maxIterationsAmount = 100;
            if (outputType == OutputTypes.AfterT) maxIterationsAmount = 1;
            // PointCollection pointsToDraw = new PointCollection();

            // for 1 - it's T-count
            for (tIterator = 0; tIterator < maxIterationsAmount; ++tIterator)
            {
                isThrSenceToGo = false;
                //if (outputType != OutputTypes.AfterT)
                    previousVectorsList = VectorHelpers.CreateCopyOfVectorArray(startingPoints);

                // for 2 - it's initial points count
                for (var startingPointIterator = 0; startingPointIterator < inputData.TaskCount; ++startingPointIterator)
                {
                    savedPoints = new Vector[6];
                    int globalStepIterator = 0;
                    // for 3 - it's rg step 
                    // if first element of statring vector is equals to -1 then do nothing
                    if (startingPoints[startingPointIterator][0] == -1) continue;

                    // if this vector still in play then we keep calculating
                    for (var stepIterator = 0; stepIterator < iterationsCount; ++stepIterator)
                    {
                        // for 4 - it's skeep part
                        for (var skipPointsIterator = 0; skipPointsIterator < inputData.ContinueNum; ++skipPointsIterator)
                        {
                            if (ODEHelpers.Contains(savePointArr, globalStepIterator) != -1)
					        {
						        int determinedPoint = ODEHelpers.Contains(savePointArr, globalStepIterator);
					            savedPoints[determinedPoint] = startingPoints[startingPointIterator].Clone();
					        }
					        globalStepIterator++;
                            if (ODEHelpers.Contains(leapArr, globalStepIterator) != -1)
					        {
						        switch (ODEHelpers.Contains(leapArr, globalStepIterator))
						        {
							        case 0:
								        startingPoints[startingPointIterator] = RungeKutta.Y1Func(savedPoints, inputData.Alpha, inputData.Beta);
								        break;
							        case 1:
								        startingPoints[startingPointIterator] = RungeKutta.Y2Func(savedPoints, inputData.Alpha, inputData.Beta);
								        break;
							        case 2:
								        startingPoints[startingPointIterator] = RungeKutta.Y3Func(savedPoints, inputData.Alpha, inputData.Beta);
								        break;
							        case 3:
								        startingPoints[startingPointIterator] = RungeKutta.Y4Func(savedPoints, inputData.Alpha, inputData.Beta);
								        break;
							        default:
								        break;
						        }
					        }
					        else
					        {                            
						        RungeKutta.MakeStep(ref startingPoints[startingPointIterator], inputData.Delta, inputData.Dx, inputData.Dimension, inputData.F);
					        }

                        } // -- skipPointsIterator

                        // fill fullVectorsSet
                        if (outputType == OutputTypes.All)
                        {
                            pointsGeometryGroup.Children.Add(
                                MainWindow.CreateEllipseGeometry(startingPoints[startingPointIterator][inputData.XOnGraph],
                                            startingPoints[startingPointIterator][inputData.YOnGraph])
                            );
                            fullVectorsSet.Add(startingPoints[startingPointIterator].Clone());
                        }

                    } // -- stepIterator

                    // Abs It
                    // The reason why we can do this is writen in a base article that says:
                    // "в силу свойства нечетности Φ(z) устойчивой и неустойчивой неподвижным точкам из R+
                    //  отвечают аналогичные симметрично расположенные неподвижные точки из R−"
                    // Which mean that one way or another any point will attract to the fixed point that it's belong
                    // despite the fact that it's positive-definited or not
                    startingPoints[startingPointIterator].Abs();

                    if (outputType != OutputTypes.All)
                    {
                        pointsGeometryGroup.Children.Add(
                            MainWindow.CreateEllipseGeometry(
                                (inputData.XOnGraph == 0)
                                    ? previousVectorsList[startingPointIterator][inputData.YOnGraph]
                                    : startingPoints[startingPointIterator][inputData.XOnGraph],
                                startingPoints[startingPointIterator][inputData.YOnGraph])
                            );
                        fullVectorsSet.Add(startingPoints[startingPointIterator].Clone());
                    }
                    else if (outputType != OutputTypes.AfterT)
                    {
                        // Analize result after another T-period
                        if (previousVectorsList == null) continue;

                        // Is this point has been changed?
                        if (startingPoints[startingPointIterator].IsEqualsTo(
                            previousVectorsList[startingPointIterator], 0.002))
                        {
                            // If it's hasn't been changed then there is a sence to iterate further
                            startingPoints[startingPointIterator][0] = -1;
                        }
                        else
                        {
                            // If it's did has been changed
                            isThrSenceToGo = true;
                        }
                    }
                } // -- startingPointIterator
                if (outputType != OutputTypes.AfterT)
                {
                    // Save vector after the first step
                    if (tIterator == 0) vectorsAfterFirstIterationList = VectorHelpers.CreateCopyOfVectorArray(startingPoints);

                    if (!isThrSenceToGo)
                        break;
                }
            } // -- tIterator
            
            // Output
            output.Add("Count of steps (T0): " + tIterator);
            foreach (Vector vector in startingPoints)
            {
                output.Add(vector[1].ToString());
            }
            
            /*
            Point p = new Point(1, 1);
            DataPoints[0].Add(AxesConverter.WtoD(p));
            pointsGeometryGroup.Children.Add(
                new EllipseGeometry(AxesConverter.WtoD(p), 3, 3)
                );*/

            // ----------------
            return pointsGeometryGroup;
        }
    }
}
