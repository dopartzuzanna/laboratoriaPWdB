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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Właściwość zwracająca zaznaczone opcje transportu
        public List<string> SelectedTransports
        {
            get
            {
                var list = new List<string>();
                if (checkBox1.Checked) list.Add(checkBox1.Text);
                if (checkBox2.Checked) list.Add(checkBox2.Text);
                if (checkBox3.Checked) list.Add(checkBox3.Text);
                if (checkBox4.Checked) list.Add(checkBox4.Text);
                return list;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // opcjonalnie: reaguj natychmiastowo albo zostaw puste
        }
    }
}
