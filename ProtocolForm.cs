using System.Text;

namespace Lab1_ASPPR
{
    public partial class ProtocolForm : Form
    {
        public ProtocolForm(string protocol)
        {
            InitializeComponent();
            textBoxProtocol.Text = protocol;
            textBoxProtocol.ScrollBars = ScrollBars.Vertical;
        }

        public static void PrintProtocol(double[,] insertMatrix, StringBuilder protocolText, int step)
        {
            double solvingElement = insertMatrix[step, step];
            protocolText.AppendLine($"Крок №{step + 1}");
            protocolText.AppendLine($"Розв'язувальний елемент: A[{step},{step}] = {Math.Round(solvingElement, 3)}");

            for (int i = 0; i < insertMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < insertMatrix.GetLength(1); j++)
                {
                    if (insertMatrix[i, j] >= 0)
                    {
                        protocolText.Append(" ");
                    }

                    protocolText.Append(Math.Round(insertMatrix[i, j], 3) + "\t");
                }
                protocolText.AppendLine();
            }

            protocolText.AppendLine("\n");
        }

        public static void PrintProtocol(double[,] insertMatrix, StringBuilder protocolText, int step, int itaya, int jitaya)
        {
            double solvingElement = insertMatrix[itaya, jitaya];
            protocolText.AppendLine($"Крок №{step + 1}");
            protocolText.AppendLine($"Розв'язувальний елемент: A[{itaya},{jitaya}] = {Math.Round(solvingElement, 3)}");

            for (int i = 0; i < insertMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < insertMatrix.GetLength(1); j++)
                {
                    if (insertMatrix[i, j] >= 0)
                    {
                        protocolText.Append(" ");
                    }

                    protocolText.Append(Math.Round(insertMatrix[i, j], 3) + "\t");
                }
                protocolText.AppendLine();
            }

            protocolText.AppendLine("\n");
        }

        public static void PrintProtocol2(StringBuilder protocolText, double[,] matrix, string[] baseVariables, string[] freeVariables, int step, int pivotRow, int pivotCol)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            protocolText.AppendLine($"Крок №{step + 1}");
            protocolText.AppendLine($"Розв’язувальний рядок: {baseVariables[pivotRow]}");
            protocolText.AppendLine($"Розв’язувальний стовпець: {freeVariables[pivotCol]}");

            protocolText.Append("    ");
            for (int j = 0; j < cols - 1; j++)
            {
                protocolText.Append($"{freeVariables[j],6}");
            }
            protocolText.Append("   Вільн.\n");

            protocolText.AppendLine(new string('-', 7 * cols));

            for (int i = 0; i < rows - 1; i++)
            {
                protocolText.Append($"{baseVariables[i],-4}= ");
                for (int j = 0; j < cols; j++)
                {
                    protocolText.Append($"{matrix[i, j],6:0.00}");
                }
                protocolText.AppendLine();
            }

            protocolText.Append("Z    = ");
            for (int j = 0; j < cols; j++)
            {
                protocolText.Append($"{matrix[rows - 1, j],6:0.00}");
            }
            protocolText.AppendLine("\n");
        }
    }
}
