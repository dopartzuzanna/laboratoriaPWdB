using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab6
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnZapisz_Click(object sender, EventArgs e)
        {
   
            UstawieniaGry.X = (int)numX.Value;
            UstawieniaGry.Y = (int)numY.Value;
            UstawieniaGry.Czas = (int)numCzas.Value;
            UstawieniaGry.LiczbaDydelfow = (int)numDydelfy.Value;
            UstawieniaGry.LiczbaSzopow = (int)numSzopy.Value;
            UstawieniaGry.LiczbaKrokodyli = (int)numKrokodyle.Value;

            this.Close(); 
        
    }
    }
}
