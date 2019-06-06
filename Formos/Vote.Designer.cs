namespace Formos
{
    partial class Vote
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
            this.XD = new System.Windows.Forms.Label();
            this.kanditatas1 = new System.Windows.Forms.RadioButton();
            this.kanditatas2 = new System.Windows.Forms.RadioButton();
            this.kanditatas3 = new System.Windows.Forms.RadioButton();
            this.StartButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // XD
            // 
            this.XD.AutoSize = true;
            this.XD.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.XD.Location = new System.Drawing.Point(12, 9);
            this.XD.Name = "XD";
            this.XD.Size = new System.Drawing.Size(142, 25);
            this.XD.TabIndex = 0;
            this.XD.Text = "BALSAVIMAS";
            // 
            // kanditatas1
            // 
            this.kanditatas1.AutoSize = true;
            this.kanditatas1.Location = new System.Drawing.Point(27, 59);
            this.kanditatas1.Name = "kanditatas1";
            this.kanditatas1.Size = new System.Drawing.Size(103, 17);
            this.kanditatas1.TabIndex = 12;
            this.kanditatas1.TabStop = true;
            this.kanditatas1.Text = "Ingrida Šimonytė";
            this.kanditatas1.UseVisualStyleBackColor = true;
            this.kanditatas1.CheckedChanged += new System.EventHandler(this.Kandidatas1RadioButton_CheckedChanged);
            // 
            // kanditatas2
            // 
            this.kanditatas2.AutoSize = true;
            this.kanditatas2.Location = new System.Drawing.Point(27, 82);
            this.kanditatas2.Name = "kanditatas2";
            this.kanditatas2.Size = new System.Drawing.Size(107, 17);
            this.kanditatas2.TabIndex = 13;
            this.kanditatas2.TabStop = true;
            this.kanditatas2.Text = "Gitanas Nausėda";
            this.kanditatas2.UseVisualStyleBackColor = true;
            this.kanditatas2.CheckedChanged += new System.EventHandler(this.Kandidatas2RadioButton_CheckedChanged);
            // 
            // kanditatas3
            // 
            this.kanditatas3.AutoSize = true;
            this.kanditatas3.Location = new System.Drawing.Point(27, 105);
            this.kanditatas3.Name = "kanditatas3";
            this.kanditatas3.Size = new System.Drawing.Size(111, 17);
            this.kanditatas3.TabIndex = 14;
            this.kanditatas3.TabStop = true;
            this.kanditatas3.Text = "Saulius Skvernelis";
            this.kanditatas3.UseVisualStyleBackColor = true;
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(198, 59);
            this.StartButton.Margin = new System.Windows.Forms.Padding(2);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(147, 38);
            this.StartButton.TabIndex = 18;
            this.StartButton.Text = "Balsuoti";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // Vote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 144);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.kanditatas3);
            this.Controls.Add(this.kanditatas2);
            this.Controls.Add(this.kanditatas1);
            this.Controls.Add(this.XD);
            this.Name = "Vote";
            this.Text = "Vote";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label XD;
        private System.Windows.Forms.RadioButton kanditatas1;
        private System.Windows.Forms.RadioButton kanditatas2;
        private System.Windows.Forms.RadioButton kanditatas3;
        private System.Windows.Forms.Button StartButton;
    }
}