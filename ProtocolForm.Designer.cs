namespace Lab1_ASPPR
{
    partial class ProtocolForm
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
            textBoxProtocol = new TextBox();
            SuspendLayout();
            // 
            // textBoxProtocol
            // 
            textBoxProtocol.Dock = DockStyle.Fill;
            textBoxProtocol.Location = new Point(0, 0);
            textBoxProtocol.Multiline = true;
            textBoxProtocol.Name = "textBoxProtocol";
            textBoxProtocol.Size = new Size(800, 450);
            textBoxProtocol.TabIndex = 0;
            // 
            // ProtocolForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(textBoxProtocol);
            Name = "ProtocolForm";
            Text = "ProtocolForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxProtocol;
    }
}