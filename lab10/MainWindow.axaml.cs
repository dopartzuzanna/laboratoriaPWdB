using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace lab10
{
    // 1. Model danych
    public class FastaSequence
    {
        public string Name { get; set; } = "";
        public string Sequence { get; set; } = "";
        public int Length => Sequence.Length;

        public int CountA => Sequence.Count(c => c == 'A' || c == 'a');
        public int CountT => Sequence.Count(c => c == 'T' || c == 't');
        public int CountG => Sequence.Count(c => c == 'G' || c == 'g');
        public int CountC => Sequence.Count(c => c == 'C' || c == 'c');

        public double GcPercent => Length > 0
            ? (double)(CountG + CountC) / Length * 100
            : 0;

        public int CodonCount => Length / 3;
    }

    // 2. Konwerter do wykresu
    public class LengthToHeightConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int length)
            {
                // Skalowanie: 1mln bp / 20k = 50px wysokości. Max 100px.
                double scaledHeight = length / 20000.0;
                return Math.Min(scaledHeight, 100);
            }
            return 0;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => null;
    }

    // 3. Główne okno
    public partial class MainWindow : Window
    {
        public ObservableCollection<FastaSequence> Sequences { get; set; } = new();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private async void OnLoadFastaClick(object sender, RoutedEventArgs e)
        {
            var files = await this.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Wybierz plik FASTA",
                AllowMultiple = true
            });

            if (files.Any())
            {
                foreach (var file in files)
                {
                    using var stream = await file.OpenReadAsync();
                    using var reader = new StreamReader(stream);
                    ParseFasta(await reader.ReadToEndAsync());
                }
            }
        }

        private void ParseFasta(string content)
        {
            var lines = content.Split('\n');
            FastaSequence? currentSeq = null;
            StringBuilder seqBuilder = new StringBuilder();

            foreach (var line in lines.Select(l => l.Trim()))
            {
                if (line.StartsWith(">"))
                {
                    if (currentSeq != null)
                    {
                        currentSeq.Sequence = seqBuilder.ToString();
                        Sequences.Add(currentSeq);
                    }
                    currentSeq = new FastaSequence { Name = line.Substring(1) };
                    seqBuilder.Clear();
                }
                else if (!string.IsNullOrWhiteSpace(line))
                {
                    seqBuilder.Append(line);
                }
            }

            if (currentSeq != null)
            {
                currentSeq.Sequence = seqBuilder.ToString();
                Sequences.Add(currentSeq);
            }
        }

        private async void OnExportCsvClick(object sender, RoutedEventArgs e)
        {
            var file = await this.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
            {
                Title = "Zapisz jako CSV",
                DefaultExtension = ".csv",
                SuggestedFileName = "analiza_dna.csv"
            });

            if (file != null)
            {
                var csv = new StringBuilder();
                csv.AppendLine("Nazwa;Dlugosc;GC_Procent;Kodony");
                foreach (var seq in Sequences)
                    csv.AppendLine($"{seq.Name};{seq.Length};{seq.GcPercent:F2};{seq.CodonCount}");

                await File.WriteAllTextAsync(file.Path.LocalPath, csv.ToString());
            }
        }

        private async void OnExportJsonClick(object sender, RoutedEventArgs e)
        {
            var file = await this.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
            {
                Title = "Zapisz jako JSON",
                DefaultExtension = ".json"
            });

            if (file != null)
            {
                var json = JsonSerializer.Serialize(Sequences, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(file.Path.LocalPath, json);
            }
        }
    }
}