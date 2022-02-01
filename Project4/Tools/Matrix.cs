using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Project4.Tools
{
    public class Matrix
    {
        public double[,] matrix;
        int row, column;

        public int Row { get { return row; } }
        public int Column { get { return column; } }

        public Matrix(int row, int colunm)
        {
            this.row = row;
            this.column = colunm;
            matrix = new double[row, column];
        }

        public Matrix Transpose()
        {
            Matrix m = new Matrix(column, row);

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    m.matrix[j, i] = matrix[i, j];
                }
            }

            return m;
        }

        public void TransposeMyself()
        {
            matrix = Transpose().matrix;
        }

        public Matrix Inverse()
        {
            double det = Determinant();
            if (det == 0)
            {
                throw new Exception("Impossible operation");
            }

            Matrix m = new Matrix(row, column);

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    m.matrix[i, j] = Cofactor(matrix, i, j) / det;
                }
            }

            return m.Transpose();
        }

        public double Determinant()
        {
            if (column != row)
            {
                throw new Exception("Impossible operation");
            }
            return Determinant(matrix);
        }

        private double Determinant(double[,] matrix)
        {
            int n = (int)Math.Sqrt(matrix.Length);

            if (n == 1)
            {
                return matrix[0, 0];
            }

            double det = 0;

            for (int k = 0; k < n; k++)
            {
                det += matrix[0, k] * Cofactor(matrix, 0, k);
            }

            return det;
        }

        private double Cofactor(double[,] matrix, int row, int column)
        {
            return Math.Pow(-1, column + row) * Determinant(Minor(matrix, row, column));
        }

        private double[,] Minor(double[,] matrix, int row, int column)
        {
            int n = (int)Math.Sqrt(matrix.Length);
            double[,] minor = new double[n - 1, n - 1];

            int _i = 0;
            for (int i = 0; i < n; i++)
            {
                if (i == row)
                {
                    continue;
                }
                int _j = 0;
                for (int j = 0; j < n; j++)
                {
                    if (j == column)
                    {
                        continue;
                    }
                    minor[_i, _j] = matrix[i, j];
                    _j++;
                }
                _i++;
            }
            return minor;
        }

        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            if (m1.row != m2.row || m1.column != m2.column)
            {
                throw new Exception("Impossible operation");
            }

            Matrix m = new Matrix(m1.row, m1.column);

            for (int i = 0; i < m1.row; i++)
            {
                for (int j = 0; j < m1.column; j++)
                {
                    m.matrix[i, j] = m1.matrix[i, j] + m2.matrix[i, j];
                }
            }

            return m;
        }

        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            if (m1.row != m2.row || m1.column != m2.column)
            {
                throw new Exception("Impossible operation");
            }

            Matrix m = new Matrix(m1.row, m1.column);

            for (int i = 0; i < m1.row; i++)
            {
                for (int j = 0; j < m1.column; j++)
                {
                    m.matrix[i, j] = m1.matrix[i, j] - m2.matrix[i, j];
                }
            }

            return m;
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.column != m2.row)
            {
                throw new Exception("Impossible operation");
            }

            Matrix m = new Matrix(m1.row, m2.column);

            for (int i = 0; i < m1.row; i++)
            {
                for (int j = 0; j < m2.column; j++)
                {
                    double sum = 0;

                    for (int k = 0; k < m1.column; k++)
                    {
                        sum += m1.matrix[i, k] * m2.matrix[k, j];
                    }

                    m.matrix[i, j] = sum;
                }
            }

            return m;
        }

        public static Vector4 operator *(Matrix m1, Vector4 v)
        {
            if (m1.column != 4)
            {
                throw new Exception("Impossible operation");
            }

            Matrix m2 = new Matrix(m1.row, 1);
            m2.matrix = new double[,] { { v.X }, { v.Y }, { v.Z }, { v.W } };
            Matrix m = new Matrix(m1.row, 1);

            for (int i = 0; i < m1.row; i++)
            {
                for (int j = 0; j < m2.column; j++)
                {
                    double sum = 0;

                    for (int k = 0; k < m1.column; k++)
                    {
                        sum += m1.matrix[i, k] * m2.matrix[k, j];
                    }

                    m.matrix[i, j] = sum;
                }
            }
            Vector4 res = new Vector4((float)m.matrix[0, 0], (float)m.matrix[1, 0], (float)m.matrix[2, 0], (float)m.matrix[3, 0]);
            return res;
        }

        public override string ToString()
        {
            string str = "";

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    str += matrix[i, j] + "\t";
                }
                str += "\n";
            }

            return str;
        }
    }
}
