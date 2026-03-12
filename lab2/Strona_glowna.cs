using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2
{
    public partial class Strona_glowna : Form
    {
        public Strona_glowna()
        {
            InitializeComponent();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Lista oknoWyboru = new Lista();

            if (oknoWyboru.ShowDialog() == DialogResult.OK)
            {
                foreach (string produkt in oknoWyboru.WybraneProdukty)
                {
                    listView1.Items.Add(produkt);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 oknoWyboru = new Form1();
            oknoWyboru.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 oknoWyboru = new Form2();
            oknoWyboru.ShowDialog();

            // Po zamknięciu okna odczytujemy wybraną wartość i pokazujemy ją na stronie głównej
            string wybor = oknoWyboru.SelectedPayment;
            if (!string.IsNullOrEmpty(wybor))
            {
                UstawFormeZaplaty(wybor);
            }
        }

        public void UstawFormeZaplaty(string tekst)
        {
            // Dodajemy jako element listy zamiast ustawiać .Text (ListView.Text nie pokazuje elementu)
            if (!string.IsNullOrEmpty(tekst))
            {
                listView3.Items.Add(tekst);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
