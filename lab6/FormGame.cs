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
        private int pozostalyCzas;
        public FormGame()
        {
            InitializeComponent();
            pozostalyCzas = UstawieniaGry.Czas;
            GenerujPlansze();
            RozmiesczZwierzeta();
            timerGry.Interval = 1000;
            timerGry.Start();
            timerKrokodyl.Interval = 2000;
            timerKrokodyl.Tick += timerKrokodyl_Tick;
            timerSzop.Tick += timerSzop_Tick;

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


        private void RozmiesczZwierzeta()
        {
            Random rnd = new Random();
            var wszystkiePrzyciski = gridPlansza.Controls.OfType<Button>().ToList();

            // Najpierw ustawiamy wszystkim "Puste"
            foreach (var b in wszystkiePrzyciski) b.Tag = "Puste";

            // Losujemy Dydelfy
            RozdzielRole(wszystkiePrzyciski, "Dydelf", UstawieniaGry.LiczbaDydelfow, rnd);
            // Losujemy Szopy 
            RozdzielRole(wszystkiePrzyciski, "Szop", UstawieniaGry.LiczbaSzopow, rnd);
            // Losujemy Krokodyle 
            RozdzielRole(wszystkiePrzyciski, "Krokodyl", UstawieniaGry.LiczbaKrokodyli, rnd);
        }

        private void RozdzielRole(List<Button> przyciski, string rola, int ilosc, Random rnd)
        {
            int dodane = 0;
            while (dodane < ilosc)
            {
                int index = rnd.Next(przyciski.Count);
                if (przyciski[index].Tag.ToString() == "Puste")
                {
                    przyciski[index].Tag = rola;
                    dodane++;
                }
            }
        }

        private int znalezioneDydelfy = 0;
        private bool krokodylAktywny = false;
        private Button ostatniSzop;

        private void timerKrokodyl_Tick(object sender, EventArgs e)
        {
            timerKrokodyl.Stop();
            if (krokodylAktywny)
            {
                timerGry.Stop();
                MessageBox.Show("Porażka! Krokodyl Cię dopadł (brak reakcji w 2s)."); // 
                this.Close();
            }
        }

     

        private void timerSzop_Tick(object sender, EventArgs e)
        {
            timerSzop.Stop();
            if (ostatniSzop == null) return;

            // Pobieramy współrzędne z nazwy przycisku (btn_x_y)
            string[] parts = ostatniSzop.Name.Split('_');
            int x = int.Parse(parts[1]);
            int y = int.Parse(parts[2]);

            // Pętla sprawdzająca sąsiadów (3x3 dookoła szopa)
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int nx = x + i;
                    int ny = y + j;

                    // Szukamy przycisku o takich współrzędnych w gridzie
                    Control[] found = gridPlansza.Controls.Find($"btn_{nx}_{ny}", true);
                    if (found.Length > 0)
                    {
                        Button b = (Button)found[0];
                        b.Enabled = true;
                        b.BackColor = Color.Gray;
                        b.Text = "";
                    }
                }
            }
        }
        private void Przycisk_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string coTo = btn.Tag.ToString();

       
            switch (coTo)
            {
                case "Dydelf":
                    btn.Text = "🦡"; 
                    btn.BackColor = Color.LightGreen;
                    btn.Enabled = false;
                    znalezioneDydelfy++;
                    if (znalezioneDydelfy == UstawieniaGry.LiczbaDydelfow)
                    {
                        timerGry.Stop();
                        MessageBox.Show("Sukces! Wszystkie Dydelfy odnalezione."); 
                    }
                    break;

                case "Szop":
                    btn.Text = "🦝";
                    btn.BackColor = Color.Orange;
                    btn.Enabled = false;
                    ostatniSzop = btn;
                    timerSzop.Interval = 2000;
                    timerSzop.Start();
                    break;

                case "Krokodyl":
                    if (!krokodylAktywny)
                    {
                        btn.Text = "🐊";
                        btn.BackColor = Color.Red;
                        krokodylAktywny = true;
                        timerKrokodyl.Start(); // Startujemy 2 sekundy na reakcję 
                    }
                    else
                    {
                        // Gracz zdążył kliknąć drugi raz!
                        timerKrokodyl.Stop();
                        krokodylAktywny = false;
                        btn.Text = "";
                        btn.BackColor = Color.Gray;
                        btn.Enabled = true; // Pozwalamy kliknąć ponownie później
                    }
                    break;

                default:
                    btn.BackColor = Color.White; // Puste pole 
                    break;
            }
        }
        private void FormGame_Load(object sender, EventArgs e)
        {

        }

        private void lblCzas_Click(object sender, EventArgs e)
        {

        }

        private void timerGry_Tick(object sender, EventArgs e)
        {
            if (pozostalyCzas > 0)
            {
                pozostalyCzas--;
                lblCzas.Text = pozostalyCzas + " s";
            }
            else
            {
                timerGry.Stop();
                MessageBox.Show("Gra kończy się porażką gdy upłynie ustalony czas."); 
        this.Close();
            }
        }
    }
}
