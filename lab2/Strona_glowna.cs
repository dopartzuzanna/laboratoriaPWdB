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
            // Otwieramy okno transportu modalnie
            Form1 oknoWyboru = new Form1();
            oknoWyboru.ShowDialog();

            // Po zamknięciu okna odczytujemy zaznaczone opcje i pokazujemy je w listView2
            var wybrane = oknoWyboru.SelectedTransports;
            if (wybrane != null && wybrane.Count > 0)
            {
                // czyścimy poprzednie wpisy (opcjonalne)
                listView2.Items.Clear();
                foreach (string transport in wybrane)
                {
                    listView2.Items.Add(transport);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 oknoZaplaty = new Form2(this);
            oknoZaplaty.Show();
        }

        public void UstawFormeZaplaty(string tekst)
        {
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

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
