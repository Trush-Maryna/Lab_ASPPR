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

            for (int step = 0; step < Math.Min(rows, columns); step++)
            {
                // Пошук розв'язувального елемента
                int pivotRow = -1, pivotCol = -1;
                double maxValue = 0.0;
                for (int i = step; i < rows; i++)
                {
                    for (int j = step; j < columns; j++)
                    {
                        if (Math.Abs(tempMatrix[i, j]) > maxValue)
                        {
                            maxValue = Math.Abs(tempMatrix[i, j]);
                            pivotRow = i;
                            pivotCol = j;
                        }
                    }
                }
                // Якщо немає ненульового елемента, пропускаю
                if (pivotRow == -1 || pivotCol == -1 || maxValue == 0.0)
                {
                    // Перевірка на повністю нульовий рядок
                    bool allZero = true;
                    for (int j = 0; j < columns; j++)
                    {
                        if (tempMatrix[step, j] != 0)
                        {
                            allZero = false;
                            break;
                        }
                    }
                    if (allZero) continue;
                }
                rank++;
                // Переставляю рядки і стовпці
                SwapRows(tempMatrix, step, pivotRow);
                SwapColumns(tempMatrix, step, pivotCol);

                double pivotElement = tempMatrix[step, step];

                if (pivotElement == 0.0) continue;
                // Оновлення матриці за формулою та нормалізація розв'язувального елемента
                for (int i = 0; i < rows; i++)
                {
                    if (i == step) continue;
                    for (int j = 0; j < columns; j++)
                    {
                        if (j == step) continue;
                        tempMatrix[i, j] = (tempMatrix[i, j] * pivotElement - tempMatrix[i, step] * tempMatrix[step, j]) / pivotElement;
                    }
                }
                for (int i = 0; i < rows; i++)
                {
                    if (i != step) tempMatrix[i, step] = 0;
                }
                for (int j = 0; j < columns; j++)
                {
                    if (j != step) tempMatrix[step, j] = 0;
                }
                tempMatrix[step, step] = 1;
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

        private static void SwapColumns(double[,] matrix, int col1, int col2)
        {
            int rows = matrix.GetLength(0);
            for (int i = 0; i < rows; i++)
            {
                double temp = matrix[i, col1];
                matrix[i, col1] = matrix[i, col2];
                matrix[i, col2] = temp;
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
                }
                augmentedMatrix[i, i + n] = 1;
            }
            for (int pivotRow = 0; pivotRow < n; pivotRow++)
            {
                double pivotElement = augmentedMatrix[pivotRow, pivotRow];
                if (pivotElement == 0)
                {
                    bool swapped = false;
                    for (int k = pivotRow + 1; k < n; k++)
                    {
                        if (augmentedMatrix[k, pivotRow] != 0)
                        {
                            SwapRows(augmentedMatrix, pivotRow, k);
                            swapped = true;
                            break;
                        }
                    }
                    if (!swapped)
                        throw new InvalidOperationException("Матриця вироджена (не має оберненої).");

                    pivotElement = augmentedMatrix[pivotRow, pivotRow];
                }
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < 2 * n; j++)
                    {
                        if (i != pivotRow && j != pivotRow)
                        {
                            augmentedMatrix[i, j] = (augmentedMatrix[i, j] * pivotElement
                                - augmentedMatrix[i, pivotRow] * augmentedMatrix[pivotRow, j]) / pivotElement;
                        }
                    }
                }
                for (int i = 0; i < n; i++)
                {
                    if (i != pivotRow)
                    {
                        augmentedMatrix[i, pivotRow] = 0;
                    }
                }
                for (int j = 0; j < 2 * n; j++)
                {
                    if (j != pivotRow)
                    {
                        augmentedMatrix[pivotRow, j] /= pivotElement; 
                    }
                }
                augmentedMatrix[pivotRow, pivotRow] = 1; 
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

            protocol.AppendLine("Формування розширеної матриці [A | -B]:");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    augmentedMatrix[i, j] = matrixA[i, j];
                }
                augmentedMatrix[i, n] = -matrixB[i];
            }
            protocol.AppendLine(MatrixGenerator.MatrixToString(augmentedMatrix));

            for (int step = 0; step < n; step++)
            {
                protocol.AppendLine($"Крок #{step + 1}");
                double pivot = augmentedMatrix[step, step];
                if (pivot == 0)
                {
                    throw new InvalidOperationException("Система не має унікального розв'язку (неможливо обрати розв'язувальний елемент).");
                }

                protocol.AppendLine($"Розв'язувальний елемент: A[{step + 1},{step + 1}] = {pivot:F2}");
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j <= n; j++)
                    {
                        if (i != step && j != step)
                        {
                            augmentedMatrix[i, j] = Math.Round(
                                (augmentedMatrix[i, j] * pivot - augmentedMatrix[i, step] * augmentedMatrix[step, j]) / pivot,
                                2
                            );
                        }
                    }
                }
                for (int j = 0; j <= n; j++)
                {
                    if (j != step)
                    {
                        augmentedMatrix[step, j] = Math.Round(augmentedMatrix[step, j] / pivot, 2);
                    }
                }
                for (int i = 0; i < n; i++)
                {
                    if (i != step)
                    {
                        augmentedMatrix[i, step] = Math.Round(-augmentedMatrix[i, step] / pivot, 2);
                    }
                }
                augmentedMatrix[step, step] = Math.Round(1 / pivot, 2);
                protocol.AppendLine("Матриця після виконання Жорданових виключень:");
                protocol.AppendLine(MatrixGenerator.MatrixToString(augmentedMatrix));
            }

            double[] solutions = new double[n];
            protocol.AppendLine("Витяг розв'язків:");
            for (int i = 0; i < n; i++)
            {
                solutions[i] = Math.Round(-augmentedMatrix[i, n], 1);
                protocol.AppendLine($"X[{i + 1}] = {solutions[i]:F1}");
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
