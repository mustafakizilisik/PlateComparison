namespace PlateComparison
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFirstPlate = new System.Windows.Forms.TextBox();
            this.txtLastPlate = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(100, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "1. Plaka";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(234, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "2. Plaka";
            // 
            // txtFirstPlate
            // 
            this.txtFirstPlate.Location = new System.Drawing.Point(103, 99);
            this.txtFirstPlate.Name = "txtFirstPlate";
            this.txtFirstPlate.Size = new System.Drawing.Size(100, 23);
            this.txtFirstPlate.TabIndex = 2;
            // 
            // txtLastPlate
            // 
            this.txtLastPlate.Location = new System.Drawing.Point(234, 99);
            this.txtLastPlate.Name = "txtLastPlate";
            this.txtLastPlate.Size = new System.Drawing.Size(100, 23);
            this.txtLastPlate.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(180, 152);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Karşılaştır";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(150, 201);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(91, 15);
            this.lblResult.TabIndex = 5;
            this.lblResult.Text = "Doğruluk Oranı:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 288);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtLastPlate);
            this.Controls.Add(this.txtFirstPlate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtFirstPlate;
        private TextBox txtLastPlate;
        private Button button1;
        private Label lblResult;
    }
}