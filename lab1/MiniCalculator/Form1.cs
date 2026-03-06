using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Calculation;

namespace MiniCalculator
{
    public partial class Form1 : Form
    {
        private TextBox txtFirstNo;
        private TextBox txtSecNo;
        private TextBox txtResult;
        public Form1()
        {
            InitializeComponent();

            txtFirstNo = new TextBox();
            txtFirstNo.Location = new Point(10, 10);
            this.Controls.Add(txtFirstNo);

            txtSecNo = new TextBox();
            txtSecNo.Location = new Point(10, 40);
            this.Controls.Add(txtSecNo);

            txtResult = new TextBox();
            txtResult.Location = new Point(10, 70);
            this.Controls.Add(txtResult);
        }
        calculate cal=new calculate();
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //storing the result in int i
                int i = cal.Add(int.Parse(txtFirstNo.Text), int.Parse(txtSecNo.Text));
                txtResult.Text = i.ToString();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //storing the result in int i
                int i = cal.Sub(int.Parse(txtFirstNo.Text), int.Parse(txtSecNo.Text));
                txtResult.Text = i.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
