using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Eigenvalues
{
    enum OutputTypes
    {
        // Comment
        All,
        // Comment
        AfterNt,
        // Comment
        AfterT
    };

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private InputData _inputData;
        List<Vector> _fullVectorsSet;
        public MainWindow()
        {
            
            InitializeComponent();            
        }

        // The data.        
        private Brush[] _dataBrushes = { Brushes.Red, Brushes.Green, Brushes.Blue };

        // To mark a clicked point.
        private Ellipse _dataEllipse = null;
        private Label _dataLabel = null;

        // Draw a simple graph.
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Initial Data Set Initializing
            _inputData = new InputData();

            // Some Calculations For Drawing Axes
            const double dmargin = 10;
            double dxmin = dmargin;
            double dxmax = canGraph.Width - dmargin;
            double dymin = dmargin;
            double dymax = canGraph.Height - dmargin;

            // Prepare the transformation matrices.
            AxesConverter.PrepareTransformations(
                _inputData.Wxmin, _inputData.Wxmax, _inputData.Wymin, _inputData.Wymax,
                dxmin, dxmax, dymax, dymin);

            // Drawing a Axes Base On Initial Data and Calculations Above
            SetAxes(_inputData.Wxmin, _inputData.Wxmax, _inputData.Wymin, _inputData.Wymax, _inputData.Xstep, _inputData.Ystep);

            // Draw a Title
            /*Point titleLocation = AxesConverter.WtoD(new Point(50, 10));
            DrawText(canGraph, "Uh",
                titleLocation, 0, 20,
                HorizontalAlignment.Center,
                VerticalAlignment.Top);*/
        }

        private void showAfterT_btn_Click(object sender, RoutedEventArgs e)
        {
            DrawPoints(OutputTypes.AfterT, _inputData, out _fullVectorsSet);
        }

        private void showAfterNT_btn_Click(object sender, RoutedEventArgs e)
        {
            //_inputData.TestInitializing(); // temp
            DrawPoints(OutputTypes.AfterNt, _inputData, out _fullVectorsSet);
        }

        private void showAllPoints_btn_Click(object sender, RoutedEventArgs e)
        {
            DrawPoints(OutputTypes.All, _inputData, out _fullVectorsSet);
        }

        private void AsymptoticCalculation_btn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in Calculations.CalculateAndPrintAsymptoticSolutions(_inputData))
                stateEquilibrium.Items.Add(item);
        }

        private void PrintAllPoints_btn_Click(object sender, RoutedEventArgs e)
        {
            foreach(var item in Calculations.PrintAllPoints(_inputData, _fullVectorsSet))
                stateEquilibrium.Items.Add(item);
        }

        private void FixedPointsInfo_btn_Click(object sender, RoutedEventArgs e)
        {
            // Print Equilibrium Points If We In "AfterT" or "AfterNT" Mode Only
            foreach (var item in Calculations.FindFixedPoints(_inputData, _fullVectorsSet))
                stateEquilibrium.Items.Add(item);
        }

        private void EquilibriumStatesAnalysis_btn_Click(object sender, RoutedEventArgs e)
        {
            // Post analisys
            // Print Equilibrium Points If We In "AfterT" or "AfterNT" Mode Only
            //if (outputType != OutputTypes.All)
            foreach (string line in Calculations.AnalyseEquilibriumStates(_inputData, _fullVectorsSet))
                stateEquilibrium.Items.Add(line);
        }
        
        private void Test_btn_Click(object sender, RoutedEventArgs e)
        {
            _inputData.TestInitializing();
            //DrawPoints(OutputTypes.All, _inputData, _fullVectorsSet);
            DrawPoints(OutputTypes.AfterT, _inputData, out _fullVectorsSet);
        }

        public void Print(List<string> lines)
        {
            foreach (string line in lines)
            {
                stateEquilibrium.Items.Add(line);
            }
        }
    }
}
