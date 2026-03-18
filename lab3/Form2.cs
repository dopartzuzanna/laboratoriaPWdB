using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lab3
{
    public partial class Form2 : Form
    {
        public int EmployeeId { get; private set; }
        public string FirstName => textBox1.Text;
        public string LastName => textBox2.Text;
        public int Age => int.TryParse(textBox3.Text, out int age) ? age : 0;
        public string Position => comboBox1.Text;

        public Form2(int nextId)
        {
            InitializeComponent();

            EmployeeId = nextId;

            if (comboBox1.Items.Count == 0)
            {
                comboBox1.Items.AddRange(new object[] { "Kierownik", "Programista", "Analityk" });
                comboBox1.SelectedIndex = 0;
            }

            zatwierdz.Click += ButtonOK_Click;
            anuluj.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                MessageBox.Show("Podaj imię.", "Brak danych", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(LastName))
            {
                MessageBox.Show("Podaj nazwisko.", "Brak danych", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(Position))
            {
                MessageBox.Show("Wybierz stanowisko.", "Brak danych", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Zatwierdź dialog
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
