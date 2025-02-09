namespace Lab1_ASPPR
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            tBRows = new TextBox();
            tBColumns = new TextBox();
            label4 = new Label();
            label5 = new Label();
            tBMatrix1 = new TextBox();
            tBMatrix2 = new TextBox();
            btnGenerate = new Button();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            btnRang = new Button();
            tBRang = new TextBox();
            btnInverse = new Button();
            tBInverse = new TextBox();
            tBSLAU = new TextBox();
            btnSLAU = new Button();
            checkBox3 = new CheckBox();
            checkBox4 = new CheckBox();
            checkBox5 = new CheckBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.Location = new Point(22, 9);
            label1.Name = "label1";
            label1.Size = new Size(144, 20);
            label1.TabIndex = 0;
            label1.Text = "Генерація матриці";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 38);
            label2.Name = "label2";
            label2.Size = new Size(52, 20);
            label2.TabIndex = 1;
            label2.Text = "Рядки:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(88, 38);
            label3.Name = "label3";
            label3.Size = new Size(66, 20);
            label3.TabIndex = 2;
            label3.Text = "Стовпці:";
            // 
            // tBRows
            // 
            tBRows.Location = new Point(22, 61);
            tBRows.Name = "tBRows";
            tBRows.Size = new Size(52, 27);
            tBRows.TabIndex = 3;
            // 
            // tBColumns
            // 
            tBColumns.Location = new Point(100, 61);
            tBColumns.Name = "tBColumns";
            tBColumns.Size = new Size(54, 27);
            tBColumns.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic);
            label4.Location = new Point(234, 9);
            label4.Name = "label4";
            label4.Size = new Size(96, 20);
            label4.TabIndex = 5;
            label4.Text = "Матриця А";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic);
            label5.Location = new Point(411, 9);
            label5.Name = "label5";
            label5.Size = new Size(95, 20);
            label5.TabIndex = 6;
            label5.Text = "Матриця В";
            // 
            // tBMatrix1
            // 
            tBMatrix1.Location = new Point(210, 38);
            tBMatrix1.Multiline = true;
            tBMatrix1.Name = "tBMatrix1";
            tBMatrix1.Size = new Size(160, 108);
            tBMatrix1.TabIndex = 7;
            // 
            // tBMatrix2
            // 
            tBMatrix2.Location = new Point(401, 38);
            tBMatrix2.Multiline = true;
            tBMatrix2.Name = "tBMatrix2";
            tBMatrix2.Size = new Size(103, 108);
            tBMatrix2.TabIndex = 8;
            // 
            // btnGenerate
            // 
            btnGenerate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnGenerate.Location = new Point(22, 94);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(132, 29);
            btnGenerate.TabIndex = 9;
            btnGenerate.Text = "Згенерувати";
            btnGenerate.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(22, 129);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(105, 24);
            checkBox1.TabIndex = 10;
            checkBox1.Text = "матриця А";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(22, 159);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(104, 24);
            checkBox2.TabIndex = 11;
            checkBox2.Text = "матриця В";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // btnRang
            // 
            btnRang.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRang.Location = new Point(539, 9);
            btnRang.Name = "btnRang";
            btnRang.Size = new Size(249, 29);
            btnRang.TabIndex = 12;
            btnRang.Text = "Знайти ранг матриці";
            btnRang.UseVisualStyleBackColor = true;
            // 
            // tBRang
            // 
            tBRang.Location = new Point(539, 35);
            tBRang.Name = "tBRang";
            tBRang.Size = new Size(249, 27);
            tBRang.TabIndex = 13;
            // 
            // btnInverse
            // 
            btnInverse.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnInverse.Location = new Point(539, 79);
            btnInverse.Name = "btnInverse";
            btnInverse.Size = new Size(249, 29);
            btnInverse.TabIndex = 14;
            btnInverse.Text = "Знайти обернену матрицю";
            btnInverse.UseVisualStyleBackColor = true;
            // 
            // tBInverse
            // 
            tBInverse.Location = new Point(587, 104);
            tBInverse.Multiline = true;
            tBInverse.Name = "tBInverse";
            tBInverse.Size = new Size(160, 108);
            tBInverse.TabIndex = 15;
            // 
            // tBSLAU
            // 
            tBSLAU.Location = new Point(587, 255);
            tBSLAU.Multiline = true;
            tBSLAU.Name = "tBSLAU";
            tBSLAU.Size = new Size(160, 111);
            tBSLAU.TabIndex = 16;
            // 
            // btnSLAU
            // 
            btnSLAU.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSLAU.Location = new Point(539, 229);
            btnSLAU.Name = "btnSLAU";
            btnSLAU.Size = new Size(249, 29);
            btnSLAU.TabIndex = 17;
            btnSLAU.Text = "Обчислити СЛАУ";
            btnSLAU.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            checkBox3.Location = new Point(22, 229);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(269, 24);
            checkBox3.TabIndex = 18;
            checkBox3.Text = "Формувати протокол обчислень";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Location = new Point(22, 259);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(95, 24);
            checkBox4.TabIndex = 19;
            checkBox4.Text = "На екран";
            checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            checkBox5.AutoSize = true;
            checkBox5.Location = new Point(192, 259);
            checkBox5.Name = "checkBox5";
            checkBox5.Size = new Size(79, 24);
            checkBox5.TabIndex = 20;
            checkBox5.Text = "В файл";
            checkBox5.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 381);
            Controls.Add(checkBox5);
            Controls.Add(checkBox4);
            Controls.Add(checkBox3);
            Controls.Add(btnSLAU);
            Controls.Add(tBSLAU);
            Controls.Add(btnInverse);
            Controls.Add(tBInverse);
            Controls.Add(btnRang);
            Controls.Add(tBRang);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Controls.Add(btnGenerate);
            Controls.Add(tBMatrix2);
            Controls.Add(tBMatrix1);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(tBColumns);
            Controls.Add(tBRows);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Застосування звичайних Жорданових виключень";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox tBRows;
        private TextBox tBColumns;
        private Label label4;
        private Label label5;
        private TextBox tBMatrix1;
        private TextBox tBMatrix2;
        private Button btnGenerate;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private Button btnRang;
        private TextBox tBRang;
        private Button btnInverse;
        private TextBox tBInverse;
        private TextBox tBSLAU;
        private Button btnSLAU;
        private CheckBox checkBox3;
        private CheckBox checkBox4;
        private CheckBox checkBox5;
    }
}
