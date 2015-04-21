using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Eigenvalues
{
    public class Vector
    {
        // Variables
        private const string WrongIndices = "Все векторы должны иметь одинаковые размерности";
        private int _size;
        private double[] vector;

        // Properties
        public int Size
        {
            get { return _size; }
            set { _size = value; }
        }

        // Functions
        // -------------------------------------------------------
        public Vector()
        {
            _size = 4;
            vector = new double[4];
        }
        public Vector(int index)
        {
            _size = index;
            vector = new double[index];
        }
        public Vector(Vector z)
        {
            _size = z.Size;
            vector = new double[z.Size];
            for (int i = 0; i < z.Size; i++)
            {
                this.vector[i] = z[i];
            }
        }

        // -------------------------------------------------------
        public static Vector operator +(Vector x, Vector y)
        {
            if (x.Size != y.Size)
            {
                throw new ArgumentException(WrongIndices);
            }
            else
            {
                Vector temp = new Vector(x.Size);
                for (int i = 1; i < temp.Size-1; i++)
                {
                    temp[i] = x[i] + y[i];
                }
                return temp;
            }
        }
        public static Vector operator +(Vector x, double value)
        {
            Vector temp = new Vector(x.Size);
            for (int i = 1; i < temp.Size-1; i++)
            {
                temp[i] = x[i] + value;
            }
            return temp;
        }
        public static Vector operator +(double value, Vector x)
        {
            return x + value;
        }

        // -------------------------------------------------------
        public static Vector operator -(Vector x, Vector y)
        {
            if (x.Size != y.Size)
            {
                //throw new Exceptoin("Error");
                throw new ArgumentException(WrongIndices);
            }
            else
            {
                Vector temp = new Vector(x.Size);
                for (int i = 1; i < temp.Size - 1; i++)
                {
                    temp[i] = x[i] - y[i];
                }
                return temp;
            }
        }
        public static Vector operator -(Vector x, double value)
        {
            return x + (-value);
        }
        public static Vector operator -(double value, Vector x)
        {
            return value + (-1)*x;
        }

        // -------------------------------------------------------
        public static Vector operator *(Vector x, double scal)
        {
            Vector temp = new Vector(x.Size);
            for (int i = 1; i < temp.Size - 1; i++)
            {
                temp[i] = x[i] * scal;
            }
            return temp;
        }
        public static Vector operator *(double scal, Vector x)
        {
            return x * scal;
        }

        // -------------------------------------------------------
        public static Vector operator /(Vector x, double scal)
        {
            return x * (1.0/scal);
        }
        public static Vector operator /(double scal, Vector x)
        {
            Vector temp = new Vector(x.Size);
            for (int i = 1; i < temp.Size - 1; i++)
            {
                temp[i] = 1 / x[i];
            }
            return temp;
        }



        // -------------------------------------------------------
        public double this[int numOfElement]
        {
            get
            {
                if (numOfElement < 0 && numOfElement >= _size)
                {
                    throw new IndexOutOfRangeException();
                }
                else return vector[numOfElement];
            }
            set
            {
                if (numOfElement < 0 && numOfElement >= _size)
                {
                    throw new IndexOutOfRangeException();
                }
                else vector[numOfElement] = value;
            }
        }

        public void Abs()
        {
            for (int i = 1; i < Size - 1; i++)
            {
                this[i] = Math.Abs(this[i]);
            }
        }

        public Vector Clone()
        {
            return new Vector(this);
        }

        public double Length()
        {
            double length = 0;
            for (int i = 0; i < this.Size; i++)
            {
                length += Math.Pow(this[i], 2);
            }
            return Math.Sqrt(length);
        }
        public Point ToPoint(int x, int y)
        {
            Point point = new Point();

            point.X = this[x];
            point.Y = this[y];

            return point;
        }

        public bool IsEqualsTo(Vector vectorToCmp, double eps)
        {
            for (int j = 1; j < Size - 1; j++)
            {
                if (Math.Abs(
                    Math.Abs(this[j]) - Math.Abs(vectorToCmp[j])
                    ) > eps)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsStayInCircle(double radius)
        {
            for (int j = 1; j < Size - 1; j++)
            {
                if (this[j] > radius)
                    return false;
            }
            return true;
        }
    }
}
