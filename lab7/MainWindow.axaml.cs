using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Collections.Generic;
using System.Linq;

namespace lab7
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
            public void ButtonClicked(object source, RoutedEventArgs args)
        {
            string dna = InputDNA.Text?.ToUpper();

            if (string.IsNullOrEmpty(dna) || dna.Length < 4)
            {
                ResultText.Text = "Sekwencja jest za krótka!";
                return;
            }

            var counts = new Dictionary<string, int>();

            for (int i = 0; i <= dna.Length - 4; i++)
            {
                string kmer = dna.Substring(i, 4);
                if (counts.ContainsKey(kmer)) counts[kmer]++;
                else counts[kmer] = 1;
            }

            ResultText.Text = string.Join("\n", counts.Select(x => $"{x.Key}: {x.Value}"));
        }
    
    }
}