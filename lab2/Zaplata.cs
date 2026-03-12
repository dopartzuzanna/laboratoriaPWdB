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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        // Nowa właściwość udostępniająca wybraną formę płatności
        public string SelectedPayment
        {
            get
            {
                if (radioButton1.Checked) return radioButton1.Text;
                if (radioButton2.Checked) return radioButton2.Text;
                if (radioButton3.Checked) return radioButton3.Text;
                if (radioButton4.Checked) return radioButton4.Text;
                return null;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
