using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace lab2
{
    public partial class Lista : Form
    {
        public List<string> WybraneProdukty { get; set; } = new List<string>();

        public Lista()
        {
            InitializeComponent();

            listBox1.SelectionMode = SelectionMode.MultiExtended;
            PopulateList();

            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
        }

        private void PopulateList()
        {
            listBox1.BeginUpdate();
            try
            {
                listBox1.Items.Clear();
                for (int x = 1; x <= 50; x++)
                {
                    listBox1.Items.Add("Item " + x.ToString());
                }
            }
            finally
            {
                listBox1.EndUpdate();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WybraneProdukty.Clear();

            foreach (var item in listBox1.SelectedItems)
            {
                WybraneProdukty.Add(item.ToString());
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
