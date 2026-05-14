using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging; // Tu jest Bitmap dla UI
using Avalonia.Platform.Storage;
using QRCoder;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Drawing; // Tu jest Font i Brushes dla drukarki
using System.Drawing.Printing;
using System.Windows.Forms; // Dla PrintPreviewDialog

namespace lab11
{
    public partial class MainWindow : Window
    {
        private DatabaseManager _db;
        public ObservableCollection<BioSample> Samples { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            _db = new DatabaseManager();
            Samples = new ObservableCollection<BioSample>(_db.GetAllSamples());
            dgSamples.ItemsSource = Samples;
            dpDate.SelectedDate = DateTimeOffset.Now;
        }

        private void RefreshList()
        {
            Samples.Clear();
            foreach (var sample in _db.GetAllSamples())
            {
                Samples.Add(sample);
            }
        }

        

        private void OnGenerateQrClick(object sender, RoutedEventArgs e)
        {
            var selectedSample = dgSamples.SelectedItem as BioSample;
            if (selectedSample == null) return;

            string qrData = selectedSample.GetQrData();

            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrData, QRCodeGenerator.ECCLevel.Q))
            using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
            {
                byte[] qrCodeAsPngByteArr = qrCode.GetGraphic(20);

                using (var ms = new MemoryStream(qrCodeAsPngByteArr))
                {
                    // Używamy pełnej nazwy, aby uniknąć błędu CS0104
                    var bitmap = new Avalonia.Media.Imaging.Bitmap(ms);
                    imgQrCode.Source = bitmap;
                }
            }
            lblQrSampleName.Text = selectedSample.SampleId;
        }

        private async void OnSavePngClick(object sender, RoutedEventArgs e)
        {
            if (imgQrCode.Source == null) return;

            var file = await this.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
            {
                Title = "Zapisz etykietę QR",
                DefaultExtension = ".png",
                SuggestedFileName = $"etykieta_{lblQrSampleName.Text}.png",
                FileTypeChoices = new[] { new FilePickerFileType("Obrazy PNG") { Patterns = new[] { "*.png" } } }
            });

            if (file != null)
            {
                // Używamy pełnej nazwy, aby uniknąć błędu CS0104
                var bitmap = imgQrCode.Source as Avalonia.Media.Imaging.Bitmap;
                if (bitmap != null)
                {
                    using (var stream = await file.OpenWriteAsync())
                    {
                        bitmap.Save(stream);
                    }
                }
            }
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            string query = txtSearch.Text?.ToLower() ?? "";
            var allSamples = _db.GetAllSamples();
            var filtered = allSamples.Where(s =>
                s.Name.ToLower().Contains(query) ||
                s.SampleId.ToLower().Contains(query)
            ).ToList();

            Samples.Clear();
            foreach (var sample in filtered)
            {
                Samples.Add(sample);
            }
        }

        private void OnPrintClick(object sender, RoutedEventArgs e)
        {
            if (imgQrCode.Source == null) return;

            PrintDocument pd = new PrintDocument();

            pd.PrintPage += (s, ev) =>
            {
                var selectedSample = dgSamples.SelectedItem as BioSample;
                if (selectedSample == null) return;

                // Jawnie wskazujemy Bitmapę Avalonii
                var bitmap = imgQrCode.Source as Avalonia.Media.Imaging.Bitmap;
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    System.Drawing.Image qrImg = System.Drawing.Image.FromStream(ms);

                    ev.Graphics.DrawImage(qrImg, 10, 10, 100, 100);

                    // Używamy jawnie System.Drawing dla czcionek
                    using (var fontId = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold))
                    using (var fontName = new System.Drawing.Font("Arial", 10))
                    {
                        ev.Graphics.DrawString($"ID: {selectedSample.SampleId}", fontId, System.Drawing.Brushes.Black, 120, 20);
                        ev.Graphics.DrawString(selectedSample.Name, fontName, System.Drawing.Brushes.Black, 120, 45);
                        ev.Graphics.DrawString($"Data: {selectedSample.CollectionDate:yyyy-MM-dd}", fontName, System.Drawing.Brushes.Black, 120, 65);
                    }
                }
            };

            PrintPreviewDialog ppd = new PrintPreviewDialog();
            ppd.Document = pd;
            ppd.ShowDialog();
        }

        private int? _editingSampleId = null; // Przechowuje ID edytowanej próbki

        private void OnEditLoadClick(object sender, RoutedEventArgs e)
        {
            var selected = dgSamples.SelectedItem as BioSample;
            if (selected == null) return;

            // Ładujemy dane do formularza
            txtSampleId.Text = selected.SampleId;
            txtName.Text = selected.Name;
            txtDesc.Text = selected.Description;
            dpDate.SelectedDate = selected.CollectionDate;

            // Ustawiamy typ w ComboBox
            foreach (ComboBoxItem item in cbType.Items)
            {
                if (item.Content.ToString() == selected.Type)
                {
                    cbType.SelectedItem = item;
                    break;
                }
            }

            _editingSampleId = selected.Id;
            btnUpdate.IsEnabled = true; // Odblokowujemy przycisk aktualizacji
        }


        private void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            var selected = dgSamples.SelectedItem as BioSample;
            if (selected == null) return;

            _db.DeleteSample(selected.Id);
            RefreshList();
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSampleId.Text) || string.IsNullOrWhiteSpace(txtName.Text))
                return;

            var newSample = new BioSample
            {
                SampleId = txtSampleId.Text,
                Name = txtName.Text,
                Type = (cbType.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Inny",
                CollectionDate = dpDate.SelectedDate ?? DateTimeOffset.Now,
                Description = txtDesc.Text ?? ""
            };

            try
            {
                _db.AddSample(newSample);
                RefreshList();

                // Czyścimy pola tylko po udanym zapisie
                txtSampleId.Text = "";
                txtName.Text = "";
                txtDesc.Text = "";
            }
            catch (Exception ex)
            {
                // Wyświetlamy komunikat zamiast błędu systemowego
                System.Windows.Forms.MessageBox.Show(
                    "Błąd: Próbka o tym identyfikatorze (ID) już istnieje w bazie danych!",
                    "Błąd duplikatu",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void OnUpdateSaveClick(object sender, RoutedEventArgs e)
        {
            if (_editingSampleId == null) return;

            var updatedSample = new BioSample
            {
                Id = _editingSampleId.Value,
                SampleId = txtSampleId.Text,
                Name = txtName.Text,
                Type = (cbType.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Inny",
                CollectionDate = dpDate.SelectedDate ?? DateTimeOffset.Now,
                Description = txtDesc.Text ?? ""
            };

            try
            {
                _db.UpdateSample(updatedSample);
                RefreshList();

                _editingSampleId = null;
                btnUpdate.IsEnabled = false;
                txtSampleId.Text = "";
                txtName.Text = "";
                txtDesc.Text = "";
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    "Nie można zaktualizować: ten identyfikator (ID) jest już zajęty przez inną próbkę!",
                    "Błąd aktualizacji",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
    }
}