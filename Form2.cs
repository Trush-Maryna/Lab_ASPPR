using System.Text;

namespace Lab1_ASPPR
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            if (checkBMax.Checked)
            {
                try
                {
                    int variablesCount = int.Parse(txtBoxCount.Text.Trim());
                    string zString = tBZ.Text.Trim();
                    string[] restrictions = tBLimit.Text.Trim().Split('\n');

                    string protocolText;
                    string solutionX;
                    string solutionZ;

                    MatrixOperations.MaxSolve(variablesCount, zString, restrictions,
                        out protocolText, out solutionX, out solutionZ
                    );

                    txtBSolveX.Text = solutionX;
                    txtBSolveZ.Text = solutionZ;

                    ProtocolForm protocolForm = new ProtocolForm(protocolText);
                    protocolForm.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка: " + ex.Message);
                }
            }

            if (checkBMin.Checked)
            {
                try
                {
                    int variablesCount = int.Parse(txtBoxCount.Text.Trim());
                    string zString = tBZ.Text.Trim();
                    string[] restrictions = tBLimit.Text.Trim().Split('\n');

                    string protocolText;
                    string solutionX;
                    string solutionZ;

                    MatrixOperations.MinSolve(variablesCount, zString, restrictions,
                        out protocolText, out solutionX, out solutionZ
                    );

                    txtBSolveX.Text = solutionX;
                    txtBSolveZ.Text = solutionZ;

                    ProtocolForm protocolForm = new ProtocolForm(protocolText);
                    protocolForm.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка: " + ex.Message);
                }
            }
        }


        private void btn_example_Click(object sender, EventArgs e)
        {
            StringBuilder limitations = new();
            limitations.AppendLine("x1+x2-x3-2x4<=6");
            limitations.AppendLine("x1+x2+x3-x4>=5");
            limitations.AppendLine("2x1-x2+3x3+4x4<=10");
            tBLimit.Text = limitations.ToString();
            txtBoxCount.Text = "4";
            checkBMin.Checked = true;
            tBZ.Text = "x1+2x2-x3-x4";
        }
    }
}
