using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ДЗ_TaskManager
{
    public partial class Form1 : Form
    {
        private int _selectedPid = -1;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetupCustomUI();
        }

        private void SetupCustomUI()
        {
            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "ID";
            dataGridView1.Columns[1].Name = "Назва";
            dataGridView1.Columns[2].Name = "Пам'ять (MB)";
            dataGridView1.Columns[3].Name = "Пріоритет";
            dataGridView1.Columns[4].Name = "Час запуску";

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;

            cmbUpdateSpeed.Items.Clear();
            cmbUpdateSpeed.Items.Add("1 сек");
            cmbUpdateSpeed.Items.Add("2 сек");
            cmbUpdateSpeed.Items.Add("5 сек");
            cmbUpdateSpeed.Items.Add("Пауза");
            cmbUpdateSpeed.SelectedIndex = 0;

            updateTimer.Interval = 1000;
            updateTimer.Enabled = true;
            updateTimer.Start();

            RefreshProcessList();
        }


        private void RefreshProcessList()
        {
            int firstDisplayedScrollingRowIndex = dataGridView1.FirstDisplayedScrollingRowIndex;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (dataGridView1.SelectedRows[0].Cells[0].Value != null)
                {
                    int.TryParse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), out _selectedPid);
                }
            }

            Process[] processes = Process.GetProcesses();

            dataGridView1.Rows.Clear();

            foreach (Process p in processes)
            {
                try
                {
                    string id = p.Id.ToString();
                    string name = p.ProcessName;
                    string memory = (p.WorkingSet64 / 1024 / 1024).ToString("N1");
                    string priority = p.BasePriority.ToString();
                    string startTime;

                    try
                    {
                        startTime = p.StartTime.ToString("HH:mm:ss");
                    }
                    catch
                    {
                        startTime = "N/A";
                    }

                    dataGridView1.Rows.Add(id, name, memory, priority, startTime);
                }
                catch
                {
                }
            }

            if (firstDisplayedScrollingRowIndex >= 0 && firstDisplayedScrollingRowIndex < dataGridView1.Rows.Count)
            {
                try { dataGridView1.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex; } catch { }
            }

            if (_selectedPid != -1)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[0].Value != null &&
                        row.Cells[0].Value.ToString() == _selectedPid.ToString())
                    {
                        row.Selected = true;
                        break;
                    }
                }
            }
        }

        private void btnEndTask_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            try
            {
                int pid = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                string name = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();

                var result = MessageBox.Show($"Завершити процес {name}?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    Process p = Process.GetProcessById(pid);
                    p.Kill();
                    MessageBox.Show("Процес завершено");
                    RefreshProcessList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при завершенні: " + ex.Message);
            }
        }

        private void btnRunTask_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "exe files (*.exe)|*.exe|All files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Process.Start(ofd.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Не вдалося запустити: " + ex.Message);
                    }
                }
            }
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            try
            {
                int pid = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                Process p = Process.GetProcessById(pid);

                string info = $"ID: {p.Id}\n" +
                              $"Name: {p.ProcessName}\n" +
                              $"Threads: {p.Threads.Count}\n" +
                              $"Virtual Memory: {p.VirtualMemorySize64 / 1024 / 1024} MB";

                MessageBox.Show(info, "Деталі процесу");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неможливо отримати деталі: " + ex.Message);
            }
        }

        private void cmbUpdateSpeed_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = cmbUpdateSpeed.SelectedItem.ToString();

            switch (selected)
            {
                case "1 сек":
                    updateTimer.Interval = 1000;
                    updateTimer.Start();
                    break;
                case "2 сек":
                    updateTimer.Interval = 2000;
                    updateTimer.Start();
                    break;
                case "5 сек":
                    updateTimer.Interval = 5000;
                    updateTimer.Start();
                    break;
                case "Пауза":
                    updateTimer.Stop();
                    break;
            }
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            RefreshProcessList();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}