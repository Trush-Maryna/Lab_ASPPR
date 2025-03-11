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

        public static (double[] Solutions, string Protocol) SolveModified(double[,] matrixA, double[] matrixB)
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
                double solveElem = augmentedMatrix[step, step];

                if (solveElem == 0)
                    throw new InvalidOperationException("Система не має унікального розв'язку (неможливо обрати розв'язувальний елемент).");

                augmentedMatrix[step, step] = 1;

                for (int j = 0; j <= n; j++)
                {
                    if (j != step)
                        augmentedMatrix[step, j] /= solveElem;
                }

                for (int i = 0; i < n; i++)
                {
                    if (i != step)
                    {
                        double factor = augmentedMatrix[i, step];
                        for (int j = 0; j <= n; j++)
                        {
                            if (j != step)
                            {
                                augmentedMatrix[i, j] -= factor * augmentedMatrix[step, j];
                            }
                        }
                        augmentedMatrix[i, step] = -factor;
                    }
                }
                protocol.AppendLine("Матриця після виконання кроку:");
                protocol.AppendLine(MatrixGenerator.MatrixToString(augmentedMatrix));
            }

            double[] solutions = new double[n];
            protocol.AppendLine("Витяг розв'язків:");
            for (int i = 0; i < n; i++)
            {
                solutions[i] = -augmentedMatrix[i, n];
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

        //Lab1.2

        public static string SolveSimplex(double[,] tableau, List<int> basis)
        {
            int rows = tableau.GetLength(0);
            int cols = tableau.GetLength(1);
            string protocol = "Початкова симплекс-таблиця:\n" + PrintTable(tableau);

            while (true)
            {
                int pivotCol = FindPivotColumn(tableau);
                if (pivotCol == -1) break;

                int pivotRow = FindPivotRow(tableau, pivotCol);
                if (pivotRow == -1) return "Задача не має розв’язку";

                PerformPivoting(tableau, pivotRow, pivotCol);
                basis[pivotRow] = pivotCol;

                protocol += $"\nРозв’язувальний стовпець: {pivotCol + 1}, Розв’язувальний рядок: {pivotRow + 1}\n";
                protocol += PrintTable(tableau);
            }

            protocol += "\nЗнайдено оптимальний розв’язок:\n";
            for (int i = 0; i < basis.Count; i++)
            {
                protocol += $"X{basis[i] + 1} = {tableau[i, cols - 1]:F2}; ";
            }
            protocol += $"\nMax (Z) = {tableau[rows - 1, cols - 1]:F2}";
            return protocol;
        }

        public static int FindPivotColumn(double[,] tableau)
        {
            int lastRow = tableau.GetLength(0) - 1;
            int cols = tableau.GetLength(1);
            int pivotCol = -1;
            double min = 0;
            for (int j = 0; j < cols - 1; j++)
            {
                if (tableau[lastRow, j] < min)
                {
                    min = tableau[lastRow, j];
                    pivotCol = j;
                }
            }
            return pivotCol;
        }

        public static int FindPivotRow(double[,] tableau, int pivotCol)
        {
            int rows = tableau.GetLength(0);
            int pivotRow = -1;
            double minRatio = double.MaxValue;
            for (int i = 0; i < rows - 1; i++)
            {
                if (tableau[i, pivotCol] > 0)
                {
                    double ratio = tableau[i, tableau.GetLength(1) - 1] / tableau[i, pivotCol];
                    if (ratio < minRatio)
                    {
                        minRatio = ratio;
                        pivotRow = i;
                    }
                }
            }
            return pivotRow;
        }

        public static void PerformPivoting(double[,] tableau, int pivotRow, int pivotCol)
        {
            int cols = tableau.GetLength(1);
            double pivotValue = tableau[pivotRow, pivotCol];
            for (int j = 0; j < cols; j++)
                tableau[pivotRow, j] /= pivotValue;

            int rows = tableau.GetLength(0);
            for (int i = 0; i < rows; i++)
            {
                if (i != pivotRow)
                {
                    double factor = tableau[i, pivotCol];
                    for (int j = 0; j < cols; j++)
                        tableau[i, j] -= factor * tableau[pivotRow, j];
                }
            }
        }

        public static string PrintTable(double[,] tableau)
        {
            string table = "";
            int rows = tableau.GetLength(0);
            int cols = tableau.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    table += $"{tableau[i, j],8:F2} ";
                }
                table += "\n";
            }
            return table;
        }

        public static void MinSolve(int variablesCount, string zString, string[] restrictions,
        out string protocolText, out string solutionX, out string solutionZ)
        {
            int[] variables = VariablesRead(variablesCount, zString);
            double[,] matrix = MatrixFill(variables, restrictions);

            // Перетворення до Z'
            for (int j = 0; j < matrix.GetLength(1) - 1; j++)
            {
                matrix[matrix.GetLength(0) - 1, j] *= -1;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Знаходження опорного рішення:");

            int[] rowsHeading = null;
            int[] colsHeading = null;
            solutionX = "";
            solutionZ = "";

            try
            {
                double[] result = SupportSolution(ref matrix, sb, out rowsHeading, out colsHeading);
                sb.AppendLine("Опорний розв'язок знайдено:");
                sb.AppendLine($"X:({string.Join(", ", result)})\n");
            }
            catch (Exception ex)
            {
                sb.AppendLine("Помилка при знаходженні опорного розв'язку: " + ex.Message);
            }

            sb.AppendLine("Знаходження оптимального рішення:");
            try
            {
                double[] result = OptimalSolution(ref matrix, sb, rowsHeading, colsHeading);
                sb.AppendLine("Оптимальний розв'язок знайдено:");
                solutionX = string.Join(", ", result);
                solutionZ = (matrix[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1] * -1).ToString();
                sb.AppendLine($"X:({solutionX})");
                sb.AppendLine($"Z: {solutionZ}");
            }
            catch (Exception ex)
            {
                sb.AppendLine("Помилка при знаходженні оптимального розв'язку: " + ex.Message);
            }

            protocolText = sb.ToString();
        }

        public static void MaxSolve(int variablesCount, string zString, string[] restrictions,
        out string protocolText, out string solutionX, out string solutionZ)
        {
            int[] variables = VariablesRead(variablesCount, zString);
            double[,] matrix = MatrixFill(variables, restrictions);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Знаходження опорного рішення:");

            int[] rowsHeading = null;
            int[] colsHeading = null;
            solutionX = "";
            solutionZ = "";

            try
            {
                double[] result = SupportSolution(ref matrix, sb, out rowsHeading, out colsHeading);
                sb.AppendLine("Опорний розв'язок знайдено:");
                sb.AppendLine($"X:({string.Join(", ", result)})\n");
            }
            catch (Exception ex)
            {
                sb.AppendLine("Помилка при знаходженні опорного розв'язку: " + ex.Message);
            }

            sb.AppendLine("Знаходження оптимального рішення:");
            try
            {
                double[] result = OptimalSolution(ref matrix, sb, rowsHeading, colsHeading);
                sb.AppendLine("Оптимальний розв'язок знайдено:");
                solutionX = string.Join(", ", result);
                solutionZ = matrix[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1].ToString();
                sb.AppendLine($"X:({solutionX})");
                sb.AppendLine($"Z: {solutionZ}");
            }
            catch (Exception ex)
            {
                sb.AppendLine("Помилка при знаходженні оптимального розв'язку: " + ex.Message);
            }

            protocolText = sb.ToString();
        }

        public static double[,] MatrixFill(int[] variables, string[] rows)
        {
            double[,] matrix = new double[rows.Length + 1, variables.Length + 1];// rows.Length + 1 => zRow, variables.Length + 1 => right part

            for (int i = 0; i < rows.Length; i++)
            {
                int indexMoreOrEqual = rows[i].IndexOf(">=");
                int indexLessOrEqual = rows[i].IndexOf("<=");

                if (indexMoreOrEqual != -1)
                {
                    string[] rowParts = rows[i].Split(">=");
                    int[] vars = VariablesRead(variables.Length, rowParts[0]);

                    for (int j = 0; j < variables.Length; j++)
                    {
                        matrix[i, j] = vars[j] * -1;// *-1
                    }

                    matrix[i, variables.Length] = int.Parse(rowParts[1]) * -1;
                }

                if (indexLessOrEqual != -1)
                {
                    string[] rowParts = rows[i].Split("<=");
                    int[] vars = VariablesRead(variables.Length, rowParts[0]);

                    for (int j = 0; j < variables.Length; j++)
                    {
                        matrix[i, j] = vars[j]; //*-1
                    }

                    matrix[i, variables.Length] = int.Parse(rowParts[1]);
                }

            }

            for (int j = 0; j < variables.Length; j++)
            {
                matrix[rows.Length, j] = variables[j] * -1;
            }

            return matrix;
        }

        public static int[] VariablesRead(int varAmount, string zString)
        {
            int[] variables = new int[varAmount];
            int tempVariable = 1;

            while (zString.Length > 0)
            {
                tempVariable = 1;

                //x value
                if (zString[0] == '-')
                {
                    tempVariable *= -1;
                    zString = zString.Substring(1);
                }

                if (zString[0] == '+')
                {
                    zString = zString.Substring(1);
                }

                string coeff = String.Empty;
                while (zString[0] != 'x')
                {
                    coeff += zString.Substring(0, 1);
                    zString = zString.Substring(1);
                }

                if (coeff != String.Empty)
                {
                    tempVariable *= int.Parse(coeff);
                }

                //remowe 'x'
                zString = zString.Substring(1);

                //x index
                string xIndex = string.Empty;
                while (zString.Length > 0 && zString[0] != '-' && zString[0] != '+')
                {
                    xIndex += zString.Substring(0, 1);
                    zString = zString.Substring(1);
                }

                try
                {
                    variables[int.Parse(xIndex) - 1] = tempVariable;
                }
                catch (Exception ex)
                {
                    //stringBuilder.AppendLine(ex.Message);
                    //stringBuilder.AppendLine($"х{xIndex} не існує");
                }
            }

            return variables;
        }

        private static void FancyPrintMatrixOnRichTextBox(double[,] incertMatrix, RichTextBox richTextBox)
        {
            //richTextBox.Clear();

            for (int i = 0; i < incertMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < incertMatrix.GetLength(1); j++)
                {
                    richTextBox.Text += Math.Round(incertMatrix[i, j], 3);
                    richTextBox.Text += "\t";
                }
                richTextBox.Text += "\n";
            }
        }

        public static double[] SupportSolution(ref double[,] matrix, StringBuilder protocolText, out int[] rowsHeading, out int[] colsHeading)
        {
            double[] res = new double[matrix.GetLength(1) - 1];
            rowsHeading = new int[matrix.GetLength(0) - 1];
            colsHeading = new int[matrix.GetLength(1) - 1];

            for (int i = 0; i < rowsHeading.Length; i++)
                rowsHeading[i] = -(i + 1); // базис: -1, -2, -3...

            for (int i = 0; i < colsHeading.Length; i++)
                colsHeading[i] = i + 1;   // вільні: 1, 2, 3...

            double firstNegativeNumber = 0;
            int pickedRow = -1;
            for (int i = 0; i < matrix.GetLength(0) - 1; i++)
            {
                if (matrix[i, matrix.GetLength(1) - 1] < 0)
                {
                    firstNegativeNumber = matrix[i, matrix.GetLength(1) - 1];
                    pickedRow = i;
                    break;
                }
            }

            if (firstNegativeNumber == 0)
            {
                for (int i = 0; i < matrix.GetLength(0) - 1; i++)
                {
                    if (rowsHeading[i] > 0)
                        res[rowsHeading[i] - 1] = matrix[i, matrix.GetLength(1) - 1];
                }
                return res;
            }

            int iteration = 0;
            do
            {
                int pickedCol = -1;
                for (int j = 0; j < matrix.GetLength(1) - 1; j++)
                {
                    if (matrix[pickedRow, j] < 0)
                    {
                        pickedCol = j;
                        MatrixElementsSwap(ref rowsHeading, pickedRow, ref colsHeading, j);
                        break;
                    }
                }

                if (pickedCol == -1)
                    throw new ArgumentException("Система обмежень є суперечливою");

                double minRatio = double.MaxValue;
                for (int i = 0; i < matrix.GetLength(0) - 1; i++)
                {
                    double ratio = matrix[i, pickedCol] != 0 ? matrix[i, matrix.GetLength(1) - 1] / matrix[i, pickedCol] : double.MaxValue;
                    if (ratio >= 0 && ratio < minRatio)
                    {
                        minRatio = ratio;
                        pickedRow = i;
                    }
                }

                matrix = ModifiedZhordansExeptions(matrix, pickedRow, pickedCol);

                // Формуємо назви змінних для PrintProtocol2
                string[] baseVariables = rowsHeading.Select(v => v < 0 ? $"y{Math.Abs(v)}" : $"x{v}").ToArray();
                string[] freeVariables = colsHeading.Select(v => v < 0 ? $"y{Math.Abs(v)}" : $"x{v}").ToArray();

                ProtocolForm.PrintProtocol2(protocolText, matrix, baseVariables, freeVariables, iteration, pickedRow, pickedCol);

                firstNegativeNumber = 0;
                for (int i = 0; i < matrix.GetLength(0) - 1; i++)
                {
                    if (matrix[i, matrix.GetLength(1) - 1] < 0)
                    {
                        firstNegativeNumber = matrix[i, matrix.GetLength(1) - 1];
                        pickedRow = i;
                        break;
                    }
                }

                iteration++;
            }
            while (firstNegativeNumber != 0);

            for (int i = 0; i < matrix.GetLength(0) - 1; i++)
            {
                if (rowsHeading[i] > 0)
                    res[rowsHeading[i] - 1] = matrix[i, matrix.GetLength(1) - 1];
            }

            return res;
        }

        public static void MatrixElementsSwap(ref int[] array1, int index1, ref int[] array2, int index2)
        {
            int temp = array1[index1];
            array1[index1] = array2[index2];
            array2[index2] = temp;
        }

        public static double[] OptimalSolution(ref double[,] matrix, StringBuilder protocolText, int[] rowsHeading, int[] colsHeading)
        {
            double[] res = new double[matrix.GetLength(1) - 1];

            int iteration = 0;
            int pickedCol = -1;

            for (int j = 0; j < matrix.GetLength(1) - 1; j++)
            {
                if (matrix[matrix.GetLength(0) - 1, j] < 0)
                {
                    pickedCol = j;
                    break;
                }
            }

            while (pickedCol != -1 && iteration < 20)
            {
                int pickedRow = -1;
                double minRatio = double.MaxValue;

                for (int i = 0; i < matrix.GetLength(0) - 1; i++)
                {
                    if (matrix[i, pickedCol] > 0)
                    {
                        double ratio = matrix[i, matrix.GetLength(1) - 1] / matrix[i, pickedCol];
                        if (ratio >= 0 && ratio < minRatio)
                        {
                            minRatio = ratio;
                            pickedRow = i;
                        }
                    }
                }

                if (pickedRow == -1)
                    throw new ArgumentException("Функція мети не обмежена зверху");

                matrix = ModifiedZhordansExeptions(matrix, pickedRow, pickedCol);
                MatrixElementsSwap(ref rowsHeading, pickedRow, ref colsHeading, pickedCol);

                // Формуємо назви змінних
                string[] baseVariables = rowsHeading.Select(v => v < 0 ? $"y{Math.Abs(v)}" : $"x{v}").ToArray();
                string[] freeVariables = colsHeading.Select(v => v < 0 ? $"y{Math.Abs(v)}" : $"x{v}").ToArray();

                ProtocolForm.PrintProtocol2(protocolText, matrix, baseVariables, freeVariables, iteration, pickedRow, pickedCol);

                pickedCol = -1;
                for (int j = 0; j < matrix.GetLength(1) - 1; j++)
                {
                    if (matrix[matrix.GetLength(0) - 1, j] < 0)
                    {
                        pickedCol = j;
                        break;
                    }
                }

                iteration++;
            }

            for (int i = 0; i < matrix.GetLength(0) - 1; i++)
            {
                if (rowsHeading[i] > 0)
                    res[rowsHeading[i] - 1] = matrix[i, matrix.GetLength(1) - 1];
            }

            return res;
        }

        public static double[,] ModifiedZhordansExeptions(double[,] insertMatrix, int row, int col)
        {
            double[,] tempMatrix = CopyMatrix(insertMatrix);

            if (tempMatrix[row, col] == 0)
            {
                int swapRow = -1;
                for (int i = row + 1; i < insertMatrix.GetLength(0); i++)
                {
                    if (tempMatrix[i, row] != 0)
                    {
                        swapRow = i;
                        break;
                    }
                }

                if (swapRow == -1)
                {
                    throw new InvalidOperationException("У столбці тільки нулі");
                }

                for (int j = 0; j < insertMatrix.GetLength(1); j++)
                {
                    double temp = insertMatrix[row, j];
                    insertMatrix[row, j] = insertMatrix[swapRow, j];
                    insertMatrix[swapRow, j] = temp;
                }

                tempMatrix = CopyMatrix(insertMatrix);
            }

            double ars = tempMatrix[row, col];

            //main col
            for (int i = 0; i < insertMatrix.GetLength(0); i++)
            {
                insertMatrix[i, col] = -tempMatrix[i, col];
            }
            insertMatrix[row, col] = 1;

            //other cols
            for (int i = 0; i < insertMatrix.GetLength(0); i++)
            {
                if (i == row) continue;

                for (int j = 0; j < insertMatrix.GetLength(1); j++)
                {
                    if (j == col) continue;
                    insertMatrix[i, j] = tempMatrix[i, j] * tempMatrix[row, col] - tempMatrix[row, j] * tempMatrix[i, col];
                }
            }

            //division
            for (int i = 0; i < insertMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < insertMatrix.GetLength(1); j++)
                {
                    insertMatrix[i, j] /= ars;
                }
            }

            return insertMatrix;
        }

        public static double[,] CopyMatrix(double[,] source)
        {
            int rows = source.GetLength(0);
            int cols = source.GetLength(1);
            double[,] destination = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    destination[i, j] = source[i, j];
                }
            }

            return destination;
        }
    }
}
