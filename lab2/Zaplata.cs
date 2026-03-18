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

        private Strona_glowna _rodzic;

        public Form2(Strona_glowna rodzic)
        {
            InitializeComponent();
            _rodzic = rodzic;
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null && rb.Checked)
            {
                _rodzic.UstawFormeZaplaty(rb.Text);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
