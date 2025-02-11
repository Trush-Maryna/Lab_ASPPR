namespace Lab1_ASPPR
{
    public partial class Form1 : Form
    {
        private string protocol;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chBoxOnTheScreen.Enabled = false;
            chBoxAtTheFile.Enabled = false;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                int rows = int.Parse(tBRows.Text);
                int columns = int.Parse(tBColumns.Text);
                if (checkBox1.Checked)
                {
                    var matrixA = MatrixGenerator.GenerateMatrix(rows, columns);
                    tBMatrix1.Text = MatrixGenerator.MatrixToString(matrixA);
                }
                if (checkBox2.Checked)
                {
                    var matrixB = MatrixGenerator.GenerateMatrix(rows, columns);
                    tBMatrix2.Text = MatrixGenerator.MatrixToString(matrixB);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
        }

        private void btnRang_Click(object sender, EventArgs e)
        {
            try
            {
                double[,] matrix = MatrixOperations.StringToMatrix(tBMatrix1.Text);
                int rank = MatrixOperations.CalculateRank(matrix);
                tBRang.Text = rank.ToString();
                protocol = $"Ранг матриці: {rank}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
        }

        private void btnInverse_Click(object sender, EventArgs e)
        {
            try
            {
                double[,] matrix = MatrixOperations.StringToMatrix(tBMatrix1.Text);
                double[,] inverseMatrix = MatrixOperations.InvertMatrix(matrix);
                tBInverse.Text = MatrixGenerator.MatrixToString(inverseMatrix);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
        }

        private void btnSLAU_Click(object sender, EventArgs e)
        {
            try
            {
                double[,] matrixA = MatrixOperations.StringToMatrix(tBMatrix1.Text);
                double[] matrixB = MatrixOperations.StringToVector(tBMatrix2.Text);
                var (solutions, generatedProtocol) = MatrixOperations.Solve(matrixA, matrixB);
                tBSLAU.Text = string.Join(Environment.NewLine, solutions);
                protocol = generatedProtocol;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
        }

        private void chBoxOnTheScreen_CheckedChanged(object sender, EventArgs e)
        {
            if (chBoxOnTheScreen.Checked)
            {
                chBoxAtTheFile.Enabled = false;
                if (!string.IsNullOrEmpty(protocol))
                {
                    ProtocolForm protocolForm = new ProtocolForm(protocol);
                    protocolForm.Show();
                }
                else
                {
                    MessageBox.Show("Протокол порожній. Виконайте обчислення, щоб заповнити протокол.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                chBoxAtTheFile.Enabled = true;
            }
        }

        private void chBoxAtTheFile_CheckedChanged(object sender, EventArgs e)
        {
            if (chBoxAtTheFile.Checked)
            {
                chBoxOnTheScreen.Enabled = false;
                if (!string.IsNullOrEmpty(protocol))
                {
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                        saveFileDialog.Title = "Збереження протоколу";
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            string filePath = saveFileDialog.FileName;
                            System.IO.File.WriteAllText(filePath, protocol);
                            MessageBox.Show($"Протокол збережено у файл: {filePath}", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Збереження було скасовано.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Протокол порожній. Виконайте обчислення, щоб заповнити протокол.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                chBoxOnTheScreen.Enabled = true;
            }
        }

        private void chBoxProtocol_CheckedChanged(object sender, EventArgs e)
        {
            if (chBoxProtocol.Checked)
            {
                chBoxOnTheScreen.Enabled = true;
                chBoxAtTheFile.Enabled = true;
            }
            else
            {
                chBoxOnTheScreen.Enabled = false;
                chBoxAtTheFile.Enabled = false;

                chBoxOnTheScreen.Checked = false;
                chBoxAtTheFile.Checked = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Enabled = false;
            }
            else
            {
                checkBox2.Enabled = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Enabled = false;
            }
            else
            {
                checkBox1.Enabled = true;
            }
        }
    }
}
