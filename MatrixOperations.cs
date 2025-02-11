using System.Text;

namespace Lab1_ASPPR
{
    public static class MatrixOperations
    {
        public static int CalculateRank(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);
            double[,] tempMatrix = (double[,])matrix.Clone();
            int rank = 0;

            for (int i = 0; i < Math.Min(rows, columns); i++)
            {
                if (tempMatrix[i, i] == 0)
                {
                    bool swapped = false;
                    for (int j = i + 1; j < rows; j++)
                    {
                        if (tempMatrix[j, i] != 0)
                        {
                            SwapRows(tempMatrix, i, j);
                            swapped = true;
                            break;
                        }
                    }
                    if (!swapped) continue;
                }

                rank++;
                for (int j = 0; j < rows; j++)
                {
                    if (j == i) continue;
                    double factor = tempMatrix[j, i] / tempMatrix[i, i];
                    for (int k = 0; k < columns; k++)
                    {
                        tempMatrix[j, k] -= factor * tempMatrix[i, k];
                    }
                }
            }
            return rank;
        }

        private static void SwapRows(double[,] matrix, int row1, int row2)
        {
            int columns = matrix.GetLength(1);
            for (int i = 0; i < columns; i++)
            {
                double temp = matrix[row1, i];
                matrix[row1, i] = matrix[row2, i];
                matrix[row2, i] = temp;
            }
        }

        public static double[,] InvertMatrix(double[,] matrix)
        {
            int n = matrix.GetLength(0);
            if (n != matrix.GetLength(1))
                throw new ArgumentException("Матриця повинна бути квадратною для обчислення оберненої.");

            double[,] augmentedMatrix = new double[n, 2 * n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    augmentedMatrix[i, j] = matrix[i, j];
                    augmentedMatrix[i, j + n] = (i == j) ? 1 : 0; 
                }
            }

            for (int i = 0; i < n; i++)
            {
                if (augmentedMatrix[i, i] == 0)
                {
                    bool swapped = false;
                    for (int j = i + 1; j < n; j++)
                    {
                        if (augmentedMatrix[j, i] != 0)
                        {
                            SwapRows(augmentedMatrix, i, j);
                            swapped = true;
                            break;
                        }
                    }
                    if (!swapped)
                        throw new InvalidOperationException("Матриця вироджена (не має оберненої).");
                }

                double diagonalValue = augmentedMatrix[i, i];
                for (int j = 0; j < 2 * n; j++)
                {
                    augmentedMatrix[i, j] /= diagonalValue;
                }

                for (int j = 0; j < n; j++)
                {
                    if (j != i)
                    {
                        double factor = augmentedMatrix[j, i];
                        for (int k = 0; k < 2 * n; k++)
                        {
                            augmentedMatrix[j, k] -= factor * augmentedMatrix[i, k];
                        }
                    }
                }
            }

            double[,] inverseMatrix = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    inverseMatrix[i, j] = Math.Round(augmentedMatrix[i, j + n], 2);
                }
            }

            return inverseMatrix;
        }

        public static (double[] Solutions, string Protocol) Solve(double[,] matrixA, double[] matrixB)
        {
            int n = matrixA.GetLength(0);

            if (matrixA.GetLength(1) != n || matrixB.Length != n)
                throw new ArgumentException("Розмірність матриці коефіцієнтів та вектора вільних членів не збігається.");

            double[,] augmentedMatrix = new double[n, n + 1];
            StringBuilder protocol = new StringBuilder();

            protocol.AppendLine("Формування розширеної матриці [A | b]:");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    augmentedMatrix[i, j] = matrixA[i, j];
                }
                augmentedMatrix[i, n] = -matrixB[i];
            }
            protocol.AppendLine(MatrixGenerator.MatrixToString(augmentedMatrix));

            for (int i = 0; i < n; i++)
            {
                protocol.AppendLine($"Крок {i + 1}:");
                double pivot = augmentedMatrix[i, i];
                protocol.AppendLine($"Розв'язувальний елемент: A[{i + 1},{i + 1}] = {pivot:F2}");

                if (pivot == 0)
                {
                    bool swapped = false;
                    for (int j = i + 1; j < n; j++)
                    {
                        if (augmentedMatrix[j, i] != 0)
                        {
                            SwapRows(augmentedMatrix, i, j);
                            swapped = true;
                            protocol.AppendLine($"Рядки {i + 1} та {j + 1} були поміняні місцями.");
                            protocol.AppendLine(MatrixGenerator.MatrixToString(augmentedMatrix));
                            pivot = augmentedMatrix[i, i];
                            protocol.AppendLine($"Новий розв'язувальний елемент: A[{i + 1},{i + 1}] = {pivot:F2}");
                            break;
                        }
                    }
                    if (!swapped)
                        throw new InvalidOperationException("Система не має розв’язків або має нескінченну кількість розв’язків.");
                }

                protocol.AppendLine($"Нормалізація рядка {i + 1} (ділення на {pivot:F2}):");
                for (int j = 0; j < n + 1; j++)
                {
                    augmentedMatrix[i, j] /= pivot;
                }
                protocol.AppendLine(MatrixGenerator.MatrixToString(augmentedMatrix));

                for (int j = 0; j < n; j++)
                {
                    if (j != i)
                    {
                        double factor = augmentedMatrix[j, i];
                        protocol.AppendLine($"Віднімання {factor:F2} * рядка {i + 1} від рядка {j + 1}:");
                        for (int k = 0; k < n + 1; k++)
                        {
                            augmentedMatrix[j, k] -= factor * augmentedMatrix[i, k];
                        }
                        protocol.AppendLine(MatrixGenerator.MatrixToString(augmentedMatrix));
                    }
                }
            }

            double[] solutions = new double[n];
            protocol.AppendLine("Витяг розв’язків:");
            for (int i = 0; i < n; i++)
            {
                solutions[i] = Math.Round(augmentedMatrix[i, n], 2);
                protocol.AppendLine($"X[{i + 1}] = {solutions[i]:F2}");
            }

            return (solutions, protocol.ToString());
        }


        public static double[,] StringToMatrix(string input)
        {
            string[] rows = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            string[] firstRowElements = rows[0].Split(new[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);
            int columnCount = firstRowElements.Length;
            int rowCount = rows.Length;

            double[,] matrix = new double[rowCount, columnCount];

            for (int i = 0; i < rowCount; i++)
            {
                string[] elements = rows[i].Split(new[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (elements.Length != columnCount)
                {
                    throw new FormatException("Усі рядки повинні містити однакову кількість елементів.");
                }
                for (int j = 0; j < columnCount; j++)
                {
                    matrix[i, j] = double.Parse(elements[j]);
                }
            }

            return matrix;
        }

        public static double[] StringToVector(string input)
        {
            string[] lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            double[] vector = Array.ConvertAll(lines, double.Parse);
            return vector;
        }
    }
}
