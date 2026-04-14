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
    public partial class FormGame : Form
    {
        public FormGame()
        {
            InitializeComponent();
            GenerujPlansze();
            lblCzas.Text = UstawieniaGry.Czas.ToString() + " s";
        }

        private void GenerujPlansze()
        {
            // Czyścimy siatkę TableLayoutPanel
            gridPlansza.Controls.Clear();
            gridPlansza.ColumnCount = UstawieniaGry.X;
        gridPlansza.RowCount = UstawieniaGry.Y;

    // Dopasowujemy rozmiar komórek, żeby były równe
    for (int i = 0; i < UstawieniaGry.X; i++)
                gridPlansza.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / UstawieniaGry.X));
            for (int i = 0; i < UstawieniaGry.Y; i++)
                gridPlansza.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / UstawieniaGry.Y));

            // Tworzymy przyciski w pętli
            for (int y = 0; y < UstawieniaGry.Y; y++)
            {
                for (int x = 0; x < UstawieniaGry.X; x++)
                {
                    Button btn = new Button();
                    btn.Dock = DockStyle.Fill;
                    btn.BackColor = Color.Gray; // Szary kolor zgodnie z instrukcją 
                    btn.Name = $"btn_{x}_{y}";

                    // Każdemu przyciskowi przypisujemy to samo zdarzenie kliknięcia
                    btn.Click += Przycisk_Click;

                    gridPlansza.Controls.Add(btn, x, y);
                }
            }
        }

        private void Przycisk_Click(object sender, EventArgs e)
        {
            Button klikniety = (Button)sender;

            // Tu będziemy sprawdzać co jest pod spodem (Dydelf, Szop, Krokodyl) 
            klikniety.Enabled = false; // Przycisk może być naciśnięty tylko raz 
            klikniety.BackColor = Color.White; // Przykładowe odkrycie pola
        }
        private void FormGame_Load(object sender, EventArgs e)
        {

        }

        private void lblCzas_Click(object sender, EventArgs e)
        {

        }
    }
}
