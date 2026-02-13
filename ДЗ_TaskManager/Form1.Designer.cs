namespace ДЗ_TaskManager
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cmbUpdateSpeed = new System.Windows.Forms.ComboBox();
            this.btnEndTask = new System.Windows.Forms.Button();
            this.btnRunTask = new System.Windows.Forms.Button();
            this.btnDetails = new System.Windows.Forms.Button();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(454, 333);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // cmbUpdateSpeed
            // 
            this.cmbUpdateSpeed.FormattingEnabled = true;
            this.cmbUpdateSpeed.Location = new System.Drawing.Point(529, 303);
            this.cmbUpdateSpeed.Name = "cmbUpdateSpeed";
            this.cmbUpdateSpeed.Size = new System.Drawing.Size(121, 24);
            this.cmbUpdateSpeed.TabIndex = 1;
            this.cmbUpdateSpeed.SelectedIndexChanged += new System.EventHandler(this.cmbUpdateSpeed_SelectedIndexChanged);
            // 
            // btnEndTask
            // 
            this.btnEndTask.Location = new System.Drawing.Point(28, 384);
            this.btnEndTask.Name = "btnEndTask";
            this.btnEndTask.Size = new System.Drawing.Size(139, 23);
            this.btnEndTask.TabIndex = 2;
            this.btnEndTask.Text = "Завершити процес";
            this.btnEndTask.UseVisualStyleBackColor = true;
            this.btnEndTask.Click += new System.EventHandler(this.btnEndTask_Click);
            // 
            // btnRunTask
            // 
            this.btnRunTask.Location = new System.Drawing.Point(190, 384);
            this.btnRunTask.Name = "btnRunTask";
            this.btnRunTask.Size = new System.Drawing.Size(137, 23);
            this.btnRunTask.TabIndex = 3;
            this.btnRunTask.Text = "Запустити новий";
            this.btnRunTask.UseVisualStyleBackColor = true;
            this.btnRunTask.Click += new System.EventHandler(this.btnRunTask_Click);
            // 
            // btnDetails
            // 
            this.btnDetails.Location = new System.Drawing.Point(356, 384);
            this.btnDetails.Name = "btnDetails";
            this.btnDetails.Size = new System.Drawing.Size(74, 23);
            this.btnDetails.TabIndex = 4;
            this.btnDetails.Text = "Деталі";
            this.btnDetails.UseVisualStyleBackColor = true;
            this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
            // 
            // updateTimer
            // 
            this.updateTimer.Enabled = true;
            this.updateTimer.Interval = 1000;
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnDetails);
            this.Controls.Add(this.btnRunTask);
            this.Controls.Add(this.btnEndTask);
            this.Controls.Add(this.cmbUpdateSpeed);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cmbUpdateSpeed;
        private System.Windows.Forms.Button btnEndTask;
        private System.Windows.Forms.Button btnRunTask;
        private System.Windows.Forms.Button btnDetails;
        private System.Windows.Forms.Timer updateTimer;
    }
}

