using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Eigenvalues
{
    public partial class InputData
    {
        // Static Constatnts To Math Model
        private double _alpha;
        private double _beta;
        private double _delta;
        private double _initialPoint;        
        private bool _directionArrows;
        private double _radius;
        private int _taskCount;
        private int _dimension;
        private int _continueNum;
        private double _epsilon;
        private double _dx;
        private bool _isTest = false;

        public delegate Vector FuncDel(double t, Vector y);
        public FuncDel F;

        // Graph Preferences
        public double Wxmin = -1;
        public double Wxmax = 10;
        public double Wymin = -1;
        public double Wymax = 10;
        public double Xstep = 1;
        public double Ystep = 1;
        
        // Have to change It
        public int XOnGraph = 0;
        public int YOnGraph = 1;

        // --------------
        public InputData()
        {
            _alpha = 1.185;
            _beta = 0.05;
            _delta = 0.035;
            _initialPoint = 0; // = Math.Min((_beta + 1) / (_alpha - _beta - 1), 1);
            _directionArrows = true;
            _radius = 1.5;
            _taskCount = 1; // _taskCount = 100;
            _dimension = 3;
            _continueNum = 1;
            _epsilon = 0.1;
            _dx = 0.001;
            F = FMain;
        }

        public void TestInitializing()
        {
            _isTest = true;
            _alpha = 1.1;
            _beta = 0.05;
            _delta = 0.03;
            _taskCount = 16; // _taskCount = 100;
            _dimension = 3;
            _radius = 0.6;
            F = FTest;
        }

        // Properties
        #region Properties
        // --------------
        public double Alpha
        {
            get { return _alpha; }
            set
            {
                if (_alpha != value)
                {
                    _alpha = value;
                    RaisePropertyChanged("Alpha");
                }
            }
        }

        // --------------
        public double Beta
        {
            get { return _beta; }
            set
            {
                if (_beta != value)
                {
                    _beta = value;
                    RaisePropertyChanged("Beta");
                }
            }
        }

        // --------------
        public double Delta
        {
            get { return _delta; }
            set
            {
                if (_delta != value)
                {
                    _delta = value;
                    RaisePropertyChanged("Delta");
                }
            }
        }

        // --------------
        public double InitialPoint
        {
            get { return _initialPoint; }
            set
            {
                if (_initialPoint != value)
                {
                    _initialPoint = value;
                    RaisePropertyChanged("Initial Point");
                }
            }
        }

        // --------------
        public bool DirectionArrows
        {
            get { return _directionArrows; }
            set
            {
                if (_directionArrows != value)
                {
                    _directionArrows = value;
                    RaisePropertyChanged("DirectionArrows");
                }
            }
        }

        // --------------
        public double Radius
        {
            get { return _radius; }
            set
            {
                if (_radius != value)
                {
                    _radius = value;
                    RaisePropertyChanged("Radius");
                }
            }
        }

        // --------------
        public int TaskCount
        {
            get { return _taskCount; }
            set
            {
                if (_taskCount != Convert.ToInt32(value))
                {
                    _taskCount = Convert.ToInt32(value);
                    RaisePropertyChanged("TaskCount");
                }
            }
        }

        // --------------
        public int Dimension
        {
            get { return _dimension; }
            set
            {
                if (_dimension != Convert.ToInt32(value))
                {
                    _dimension = Convert.ToInt32(value);
                    RaisePropertyChanged("Dimension");
                }
            }
        }

        // --------------
        public int ContinueNum
        {
            get { return _continueNum; }
            set
            {
                if (_continueNum != Convert.ToInt32(value))
                {
                    _continueNum = Convert.ToInt32(value);
                    RaisePropertyChanged("Continue Number");
                }
            }
        }

        // --------------
        public double Epsilon
        {
            get { return _epsilon; }
            set
            {
                if (_epsilon != value)
                {
                    _epsilon = value;
                    RaisePropertyChanged("Epsilon");
                }
            }
        }

        // --------------
        public double Dx
        {
            get { return _dx; }
            set
            {
                if (_dx != value)
                {
                    _dx = value;
                    RaisePropertyChanged("H");
                }
            }
        }

        public bool IsTest
        {
            get { return _isTest; }
            set { _isTest = value; }
        }

        #endregion

        // TODO: Fill This With Initial Math Model
        public Vector FMain(double t, Vector y)
        {
            //Vector vector = new Vector(y.Size);
            // Do staff
            return y;
        }
        public Vector FTest(double t, Vector y)
        {
            Vector vector = new Vector(y.Size);
            vector[0] = 0;
            vector[y.Size - 1] = 0;
            vector[1] = -2 * _delta * Math.Sinh(y[1]);
            return vector;
        }
    }
}
