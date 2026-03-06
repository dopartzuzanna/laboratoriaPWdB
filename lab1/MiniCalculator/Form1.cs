using Calculation;
using System;
using System.Windows.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;
using Calculation;

namespace MiniCalculator
{
    public partial class Form1 : Form
    {
        calculate cal = new calculate();

        public Form1()
        {
            InitializeComponent();
            // Usuń ręczne tworzenie txtFirstNo/txtSecNo/txtResult — korzystamy z richTextBox1/2/3 z designera.
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(richTextBox1.Text.Trim(), out int a) ||
                !int.TryParse(richTextBox2.Text.Trim(), out int b))
            {
                MessageBox.Show("Proszę podać poprawne liczby całkowite.", "Błąd danych", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int i = cal.Add(a, b);
            richTextBox3.Text = i.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(richTextBox1.Text.Trim(), out int a) ||
                !int.TryParse(richTextBox2.Text.Trim(), out int b))
            {
                MessageBox.Show("Proszę podać poprawne liczby całkowite.", "Błąd danych", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int i = cal.Sub(a, b);
            richTextBox3.Text = i.ToString();
        }

        private void label3_Click(object sender, EventArgs e) { }
        private void richTextBox1_TextChanged(object sender, EventArgs e) { }
    }
}