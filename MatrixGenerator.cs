using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_ASPPR
{
    public static class MatrixGenerator
    {
        public static double[,] GenerateMatrix(int rows, int columns)
        {
            if (rows > 5 || columns > 5)
                throw new ArgumentException("Максимальний розмір матриці 5x5.");

            Random random = new Random();
            double[,] matrix = new double[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix[i, j] = random.Next(-10, 11);
                }
            }
            return matrix;
        }

        public static string MatrixToString(double[,] matrix)
        {
            string result = "";
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    result += matrix[i, j] + "\t";
                }
                result += Environment.NewLine;
            }
            return result;
        }
    }
}
