namespace Assimbio
{
    partial class Form1
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
            this.richTextBoxEx1 = new Ionic.WinForms.RichTextBoxEx();
            this.repeatButton1 = new Ionic.WinForms.RepeatButton();
            this.SuspendLayout();
            // 
            // richTextBoxEx1
            // 
            this.richTextBoxEx1.Location = new System.Drawing.Point(96, 80);
            this.richTextBoxEx1.Name = "richTextBoxEx1";
            this.richTextBoxEx1.NumberAlignment = System.Drawing.StringAlignment.Center;
            this.richTextBoxEx1.NumberBackground1 = System.Drawing.SystemColors.ControlLight;
            this.richTextBoxEx1.NumberBackground2 = System.Drawing.SystemColors.Window;
            this.richTextBoxEx1.NumberBorder = System.Drawing.SystemColors.ControlDark;
            this.richTextBoxEx1.NumberBorderThickness = 1F;
            this.richTextBoxEx1.NumberColor = System.Drawing.Color.DarkGray;
            this.richTextBoxEx1.NumberFont = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxEx1.NumberLeadingZeroes = false;
            this.richTextBoxEx1.NumberLineCounting = Ionic.WinForms.RichTextBoxEx.LineCounting.CRLF;
            this.richTextBoxEx1.NumberPadding = 2;
            this.richTextBoxEx1.ShowLineNumbers = false;
            this.richTextBoxEx1.Size = new System.Drawing.Size(100, 96);
            this.richTextBoxEx1.TabIndex = 0;
            this.richTextBoxEx1.Text = "";
            // 
            // repeatButton1
            // 
            this.repeatButton1.DelayTicks = 0;
            this.repeatButton1.Interval = 100;
            this.repeatButton1.Location = new System.Drawing.Point(197, 197);
            this.repeatButton1.Name = "repeatButton1";
            this.repeatButton1.Size = new System.Drawing.Size(75, 23);
            this.repeatButton1.TabIndex = 1;
            this.repeatButton1.Text = "repeatButton1";
            this.repeatButton1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.repeatButton1);
            this.Controls.Add(this.richTextBoxEx1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Ionic.WinForms.RichTextBoxEx richTextBoxEx1;
        private Ionic.WinForms.RepeatButton repeatButton1;
    }
}