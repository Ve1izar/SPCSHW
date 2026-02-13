namespace ДЗ_Threads6_7
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblThreads = new System.Windows.Forms.Label();
            this.lblEnd = new System.Windows.Forms.Label();
            this.lblStart = new System.Windows.Forms.Label();
            this.txtThreads = new System.Windows.Forms.TextBox();
            this.txtStart = new System.Windows.Forms.TextBox();
            this.btnRunTask6 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnRunTask7 = new System.Windows.Forms.Button();
            this.lblAvg = new System.Windows.Forms.Label();
            this.lblMin = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMax = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEnd = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Controls.Add(this.txtEnd);
            this.groupBox1.Controls.Add(this.lblThreads);
            this.groupBox1.Controls.Add(this.lblEnd);
            this.groupBox1.Controls.Add(this.lblStart);
            this.groupBox1.Controls.Add(this.txtThreads);
            this.groupBox1.Controls.Add(this.txtStart);
            this.groupBox1.Controls.Add(this.btnRunTask6);
            this.groupBox1.Location = new System.Drawing.Point(12, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(700, 150);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "6";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // lblThreads
            // 
            this.lblThreads.AutoSize = true;
            this.lblThreads.Location = new System.Drawing.Point(252, 39);
            this.lblThreads.Name = "lblThreads";
            this.lblThreads.Size = new System.Drawing.Size(116, 16);
            this.lblThreads.TabIndex = 14;
            this.lblThreads.Text = "Кількість потоків";
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(136, 39);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(49, 16);
            this.lblEnd.TabIndex = 13;
            this.lblEnd.Text = "Кінець";
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(19, 39);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(63, 16);
            this.lblStart.TabIndex = 12;
            this.lblStart.Text = "Початок";
            // 
            // txtThreads
            // 
            this.txtThreads.Location = new System.Drawing.Point(255, 86);
            this.txtThreads.Name = "txtThreads";
            this.txtThreads.Size = new System.Drawing.Size(100, 22);
            this.txtThreads.TabIndex = 11;
            this.txtThreads.Text = "2";
            // 
            // txtStart
            // 
            this.txtStart.Location = new System.Drawing.Point(22, 86);
            this.txtStart.Name = "txtStart";
            this.txtStart.Size = new System.Drawing.Size(100, 22);
            this.txtStart.TabIndex = 9;
            this.txtStart.Text = "0";
            // 
            // btnRunTask6
            // 
            this.btnRunTask6.Location = new System.Drawing.Point(388, 85);
            this.btnRunTask6.Name = "btnRunTask6";
            this.btnRunTask6.Size = new System.Drawing.Size(75, 23);
            this.btnRunTask6.TabIndex = 3;
            this.btnRunTask6.Text = "Запустити";
            this.btnRunTask6.UseVisualStyleBackColor = true;
            this.btnRunTask6.Click += new System.EventHandler(this.btnRunTask6_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnRunTask7);
            this.groupBox2.Controls.Add(this.lblAvg);
            this.groupBox2.Controls.Add(this.lblMin);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lblMax);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 237);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(353, 159);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "7";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // btnRunTask7
            // 
            this.btnRunTask7.Location = new System.Drawing.Point(224, 93);
            this.btnRunTask7.Name = "btnRunTask7";
            this.btnRunTask7.Size = new System.Drawing.Size(75, 23);
            this.btnRunTask7.TabIndex = 7;
            this.btnRunTask7.Text = "Запустити";
            this.btnRunTask7.UseVisualStyleBackColor = true;
            this.btnRunTask7.Click += new System.EventHandler(this.btnRunTask7_Click);
            // 
            // lblAvg
            // 
            this.lblAvg.AutoSize = true;
            this.lblAvg.Location = new System.Drawing.Point(104, 101);
            this.lblAvg.Name = "lblAvg";
            this.lblAvg.Size = new System.Drawing.Size(11, 16);
            this.lblAvg.TabIndex = 6;
            this.lblAvg.Text = "-";
            this.lblAvg.Click += new System.EventHandler(this.lblAvg_Click);
            // 
            // lblMin
            // 
            this.lblMin.AutoSize = true;
            this.lblMin.Location = new System.Drawing.Point(104, 68);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(11, 16);
            this.lblMin.TabIndex = 5;
            this.lblMin.Text = "-";
            this.lblMin.Click += new System.EventHandler(this.lblMin_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Max:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblMax
            // 
            this.lblMax.AutoSize = true;
            this.lblMax.Location = new System.Drawing.Point(104, 36);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(11, 16);
            this.lblMax.TabIndex = 3;
            this.lblMax.Text = "-";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 16);
            this.label5.TabIndex = 2;
            this.label5.Text = "Avg:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "Min:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // txtEnd
            // 
            this.txtEnd.Location = new System.Drawing.Point(139, 87);
            this.txtEnd.Name = "txtEnd";
            this.txtEnd.Size = new System.Drawing.Size(100, 22);
            this.txtEnd.TabIndex = 15;
            this.txtEnd.Text = "50";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(517, 39);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 84);
            this.listBox1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRunTask6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblMax;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblMin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAvg;
        private System.Windows.Forms.Button btnRunTask7;
        private System.Windows.Forms.TextBox txtThreads;
        private System.Windows.Forms.TextBox txtStart;
        private System.Windows.Forms.Label lblThreads;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.TextBox txtEnd;
        private System.Windows.Forms.ListBox listBox1;
    }
}

