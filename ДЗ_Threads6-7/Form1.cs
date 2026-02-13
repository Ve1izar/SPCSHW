using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ДЗ_Threads6_7
{
    public partial class Form1 : Form
    {
        private object fileLock = new object();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnRunTask6_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("Завдання 6");

            try
            {
                int start = int.Parse(txtStart.Text);
                int end = int.Parse(txtEnd.Text);
                int threadCount = int.Parse(txtThreads.Text);

                for (int i = 0; i < threadCount; i++)
                {
                    int threadId = i + 1;

                    Thread t = new Thread(() => PrintNumbers(start, end, threadId));
                    t.IsBackground = true;
                    t.Start();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Будь ласка, введіть коректні цілі числа у поля Завдання 6.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
        }

        private void PrintNumbers(int start, int end, int id)
        {
            for (int i = start; i <= end; i++)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    listBox1.Items.Add($"Потік #{id}: {i}");
                    listBox1.TopIndex = listBox1.Items.Count - 1;
                });

                Thread.Sleep(50);
            }
        }

        private void btnRunTask7_Click(object sender, EventArgs e)
        {
            lblMax.Text = "...";
            lblMin.Text = "...";
            lblAvg.Text = "...";

            if (listBox1.Items.Count > 0) listBox1.Items.Add("Завдання 7");

            Thread mainWorker = new Thread(ProcessArrayData);
            mainWorker.IsBackground = true;
            mainWorker.Start();
        }

        private void ProcessArrayData()
        {
            List<int> numbers = new List<int>();
            Random r = new Random();
            for (int i = 0; i < 10000; i++) numbers.Add(r.Next(1, 100000));

            int maxRes = 0;
            int minRes = 0;
            double avgRes = 0;

            Thread tMax = new Thread(() =>
            {
                int localMax = int.MinValue;
                foreach (int n in numbers) if (n > localMax) localMax = n;
                maxRes = localMax;
            });

            Thread tMin = new Thread(() =>
            {
                int localMin = int.MaxValue;
                foreach (int n in numbers) if (n < localMin) localMin = n;
                minRes = localMin;
            });

            Thread tAvg = new Thread(() =>
            {
                long sum = 0;
                foreach (int n in numbers) sum += n;
                avgRes = (double)sum / numbers.Count;
            });

            tMax.Start();
            tMin.Start();
            tAvg.Start();

            tMax.Join();
            tMin.Join();
            tAvg.Join();

            this.Invoke((MethodInvoker)delegate
            {
                lblMax.Text = maxRes.ToString();
                lblMin.Text = minRes.ToString();
                lblAvg.Text = avgRes.ToString("F2");
                listBox1.Items.Add("Завдання 7: Обчислення завершено.");
            });

            Thread tFile = new Thread(() =>
            {
                try
                {
                    string path = "results_gui.txt";
                    lock (fileLock)
                    {
                        using (StreamWriter sw = new StreamWriter(path))
                        {
                            sw.WriteLine($"Дата: {DateTime.Now}");
                            sw.WriteLine($"Максимум: {maxRes}");
                            sw.WriteLine($"Мінімум: {minRes}");
                            sw.WriteLine($"Середнє: {avgRes}");
                            sw.WriteLine("--- Згенерований набір чисел ---");
                            foreach (var n in numbers) sw.Write(n + " ");
                        }
                    }

                    this.Invoke((MethodInvoker)delegate {
                        MessageBox.Show($"Результати успішно записано у файл:\n{Path.GetFullPath(path)}");
                    });
                }
                catch (Exception ex)
                {
                    this.Invoke((MethodInvoker)delegate {
                        MessageBox.Show("Помилка запису у файл: " + ex.Message);
                    });
                }
            });
            tFile.Start();
        }

        private void groupBox1_Enter(object sender, EventArgs e) { }
        private void txtStart_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void groupBox2_Enter(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void lblMin_Click(object sender, EventArgs e) { }
        private void lblAvg_Click(object sender, EventArgs e) { }
    }
}