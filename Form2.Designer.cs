namespace Lab1_ASPPR
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            } 
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtBSolveX = new TextBox();
            label5 = new Label();
            txtBSolveZ = new TextBox();
            label4 = new Label();
            btnSolve = new Button();
            txtBoxCount = new TextBox();
            label3 = new Label();
            checkBMax = new CheckBox();
            checkBMin = new CheckBox();
            label2 = new Label();
            tBZ = new TextBox();
            label1 = new Label();
            tBLimit = new TextBox();
            btn_example = new Button();
            SuspendLayout();
            // 
            // txtBSolveX
            // 
            txtBSolveX.Location = new Point(282, 293);
            txtBSolveX.Name = "txtBSolveX";
            txtBSolveX.Size = new Size(262, 27);
            txtBSolveX.TabIndex = 52;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(248, 294);
            label5.Name = "label5";
            label5.Size = new Size(28, 20);
            label5.TabIndex = 51;
            label5.Text = "Х=";
            // 
            // txtBSolveZ
            // 
            txtBSolveZ.Location = new Point(282, 338);
            txtBSolveZ.Name = "txtBSolveZ";
            txtBSolveZ.Size = new Size(262, 27);
            txtBSolveZ.TabIndex = 50;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(248, 339);
            label4.Name = "label4";
            label4.Size = new Size(28, 20);
            label4.TabIndex = 49;
            label4.Text = "Z=";
            // 
            // btnSolve
            // 
            btnSolve.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSolve.Location = new Point(383, 147);
            btnSolve.Name = "btnSolve";
            btnSolve.Size = new Size(227, 73);
            btnSolve.TabIndex = 48;
            btnSolve.Text = "Знайти оптимальний розв'язок";
            btnSolve.UseVisualStyleBackColor = true;
            btnSolve.Click += btnSolve_Click;
            // 
            // txtBoxCount
            // 
            txtBoxCount.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtBoxCount.Location = new Point(383, 50);
            txtBoxCount.Multiline = true;
            txtBoxCount.Name = "txtBoxCount";
            txtBoxCount.ScrollBars = ScrollBars.Both;
            txtBoxCount.Size = new Size(97, 44);
            txtBoxCount.TabIndex = 47;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.Location = new Point(383, 16);
            label3.Name = "label3";
            label3.Size = new Size(140, 20);
            label3.TabIndex = 46;
            label3.Text = "Кількість змінних:";
            // 
            // checkBMax
            // 
            checkBMax.AutoSize = true;
            checkBMax.Location = new Point(616, 42);
            checkBMax.Name = "checkBMax";
            checkBMax.Size = new Size(59, 24);
            checkBMax.TabIndex = 45;
            checkBMax.Text = "Max";
            checkBMax.UseVisualStyleBackColor = true;
            // 
            // checkBMin
            // 
            checkBMin.AutoSize = true;
            checkBMin.Location = new Point(616, 12);
            checkBMin.Name = "checkBMin";
            checkBMin.Size = new Size(56, 24);
            checkBMin.TabIndex = 44;
            checkBMin.Text = "Min";
            checkBMin.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.Location = new Point(20, 89);
            label2.Name = "label2";
            label2.Size = new Size(101, 20);
            label2.TabIndex = 43;
            label2.Text = "Обмеження:";
            // 
            // tBZ
            // 
            tBZ.Location = new Point(54, 39);
            tBZ.Name = "tBZ";
            tBZ.Size = new Size(262, 27);
            tBZ.TabIndex = 42;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.Location = new Point(20, 40);
            label1.Name = "label1";
            label1.Size = new Size(29, 20);
            label1.TabIndex = 41;
            label1.Text = "Z=";
            // 
            // tBLimit
            // 
            tBLimit.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tBLimit.Location = new Point(54, 123);
            tBLimit.Multiline = true;
            tBLimit.Name = "tBLimit";
            tBLimit.ScrollBars = ScrollBars.Both;
            tBLimit.Size = new Size(300, 145);
            tBLimit.TabIndex = 40;
            // 
            // btn_example
            // 
            btn_example.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btn_example.Location = new Point(678, 12);
            btn_example.Name = "btn_example";
            btn_example.Size = new Size(94, 54);
            btn_example.TabIndex = 53;
            btn_example.Text = "Приклад";
            btn_example.UseVisualStyleBackColor = true;
            btn_example.Click += btn_example_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btn_example);
            Controls.Add(txtBSolveX);
            Controls.Add(label5);
            Controls.Add(txtBSolveZ);
            Controls.Add(label4);
            Controls.Add(btnSolve);
            Controls.Add(txtBoxCount);
            Controls.Add(label3);
            Controls.Add(checkBMax);
            Controls.Add(checkBMin);
            Controls.Add(label2);
            Controls.Add(tBZ);
            Controls.Add(label1);
            Controls.Add(tBLimit);
            Name = "Form2";
            Text = "Розв'язання ЗЛП (МЖВ)";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtBSolveX;
        private Label label5;
        private TextBox txtBSolveZ;
        private Label label4;
        private Button btnSolve;
        private TextBox txtBoxCount;
        private Label label3;
        private CheckBox checkBMax;
        private CheckBox checkBMin;
        private Label label2;
        private TextBox tBZ;
        private Label label1;
        private TextBox tBLimit;
        private Button btn_example;
    }
}