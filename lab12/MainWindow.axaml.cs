using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;

using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace lab12
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<AnalysisSession> Sessions { get; set; } = new();
        private List<AnalysisAttachment> _tempAttachments = new();
        private readonly string _dataFilePath = "notebook_data.json";

        public MainWindow()
        {
            InitializeComponent();
            LoadDataFromJson();
            lstSessions.ItemsSource = Sessions;
            DataContext = this;
        }

        private void OnCreateSessionClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewSessionTitle.Text)) return;

            var newSession = new AnalysisSession
            {
                Title = txtNewSessionTitle.Text,
                CreatedDate = DateTime.Now,
                Entries = new List<NotebookEntry>()
            };

            Sessions.Add(newSession);
            txtNewSessionTitle.Text = "";
            SaveDataToJson();
        }

        private void OnSessionSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstSessions.SelectedItem is AnalysisSession selectedSession)
            {
                lblSessionTitle.Text = selectedSession.Title;
                RefreshEntriesList(selectedSession);
            }
            else
            {
                lblSessionTitle.Text = "Wybierz sesję z listy po lewej stronie";
                RefreshEntriesList(null);
            }

            ResetEntryForm();
        }

        // 3. Dodawanie załącznika - TERAZ POKAZUJE NAZWY PLIKÓW!
        private async void OnAddAttachmentClick(object sender, RoutedEventArgs e)
        {
            var selectedSession = lstSessions.SelectedItem as AnalysisSession;
            if (selectedSession == null) return;

            var files = await this.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Wybierz plik załącznika",
                AllowMultiple = false
            });

            if (files != null && files.Count > 0)
            {
                var file = files[0];
                var attachment = new AnalysisAttachment
                {
                    FileName = file.Name,
                    FilePath = file.Path.LocalPath,
                    FileType = Path.GetExtension(file.Name).ToUpper().Replace(".", "")
                };

                _tempAttachments.Add(attachment);

                // Łączymy nazwy plików, żeby użytkownik widział co dodał
                var fileNames = string.Join(", ", _tempAttachments.Select(a => a.FileName));
                lblAttachmentStatus.Text = $"Wybrane: {fileNames}";
            }
        }

        private void OnSaveEntryClick(object sender, RoutedEventArgs e)
        {
            var selectedSession = lstSessions.SelectedItem as AnalysisSession;
            if (selectedSession == null || string.IsNullOrWhiteSpace(txtEntryContent.Text)) return;

            var newEntry = new NotebookEntry
            {
                Content = txtEntryContent.Text,
                Timestamp = DateTime.Now,
                Attachments = new List<AnalysisAttachment>(_tempAttachments)
            };

            selectedSession.Entries.Add(newEntry);
            SaveDataToJson();

            // Odświeżenie listy sesji (licznik wpisów)
            ForceSessionsListRefresh();
            RefreshEntriesList(selectedSession);
            ResetEntryForm();
        }

        // METODA: Usuwanie całej sesji analitycznej
        private void OnDeleteSessionClick(object sender, RoutedEventArgs e)
        {
            // Pobieramy obiekt sesji powiązany z klikniętym przyciskiem w wierszu
            if (sender is Button btn && btn.DataContext is AnalysisSession sessionToDelete)
            {
                // Jeśli usuwamy aktualnie wybraną sesję, czyścimy prawy panel
                if (lstSessions.SelectedItem == sessionToDelete)
                {
                    lstSessions.SelectedItem = null;
                }

                Sessions.Remove(sessionToDelete);
                SaveDataToJson();
            }
        }

        // METODA: Usuwanie pojedynczego wpisu z sesji
        private void OnDeleteEntryClick(object sender, RoutedEventArgs e)
        {
            var selectedSession = lstSessions.SelectedItem as AnalysisSession;
            if (selectedSession == null) return;

            // Pobieramy konkretny wpis przypisany do tego kafelka
            if (sender is Button btn && btn.DataContext is NotebookEntry entryToDelete)
            {
                selectedSession.Entries.Remove(entryToDelete);
                SaveDataToJson();

                // Odświeżamy oba panele, by zaktualizować widok i liczniki wpisów
                ForceSessionsListRefresh();
                RefreshEntriesList(selectedSession);
            }
        }

        private void ResetEntryForm()
        {
            txtEntryContent.Text = "";
            _tempAttachments.Clear();
            lblAttachmentStatus.Text = "Brak załączników";
        }

        private void RefreshEntriesList(AnalysisSession session)
        {
            if (session != null && session.Entries != null)
            {
                itemsEntries.ItemsSource = session.Entries.ToArray();
            }
            else
            {
                itemsEntries.ItemsSource = null;
            }
        }

        // Pomocniczy wymuszacz odświeżenia lewego panelu bez gubienia zaznaczenia
        private void ForceSessionsListRefresh()
        {
            lstSessions.SelectionChanged -= OnSessionSelectionChanged;
            var currentSelectedIndex = lstSessions.SelectedIndex;
            lstSessions.ItemsSource = null;
            lstSessions.ItemsSource = Sessions;
            lstSessions.SelectedIndex = currentSelectedIndex;
            lstSessions.SelectionChanged += OnSessionSelectionChanged;
        }

        private void SaveDataToJson()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(Sessions, options);
                File.WriteAllText(_dataFilePath, jsonString);
            }
            catch (Exception) { }
        }

        private void LoadDataFromJson()
        {
            if (!File.Exists(_dataFilePath)) return;

            try
            {
                string jsonString = File.ReadAllText(_dataFilePath);
                var deserialized = JsonSerializer.Deserialize<ObservableCollection<AnalysisSession>>(jsonString);
                if (deserialized != null) Sessions = deserialized;
            }
            catch (Exception)
            {
                Sessions = new ObservableCollection<AnalysisSession>();
            }
        }

        private async void OnExportPdfClick(object sender, RoutedEventArgs e)
        {
            var selectedSession = lstSessions.SelectedItem as AnalysisSession;
            if (selectedSession == null) return;

            var file = await this.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
            {
                Title = "Zapisz raport z analizy jako PDF",
                DefaultExtension = ".pdf",
                SuggestedFileName = $"Raport_Analizy_{selectedSession.Title.Replace(" ", "_")}.pdf",
                FileTypeChoices = new[] { new FilePickerFileType("Dokument PDF") { Patterns = new[] { "*.pdf" } } }
            });

            if (file == null) return;

            QuestPDF.Settings.License = LicenseType.Community;

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);

                    page.Header().Column(column =>
                    {
                        column.Item().Text("BioInfo Notebook — Raport Laboratoryjny").FontSize(24).Bold();
                        column.Item().Text($"Temat sesji: {selectedSession.Title}").FontSize(14).Bold();
                        column.Item().Text($"Data rozpoczęcia analizy: {selectedSession.CreatedDate:yyyy-MM-dd HH:mm}").FontSize(10).Italic();
                        column.Item().PaddingTop(10).LineHorizontal(1).LineColor(Colors.Grey.Lighten1);
                    });

                    page.Content().PaddingTop(15).Column(column =>
                    {
                        if (selectedSession.Entries == null || selectedSession.Entries.Count == 0)
                        {
                            column.Item().Text("Brak wpisów w tej sesji analitycznej.").Italic();
                            return;
                        }

                        foreach (var entry in selectedSession.Entries)
                        {
                            column.Item().PaddingBottom(15).Background(Colors.Grey.Lighten3).Padding(10).Column(entryCol =>
                            {
                                entryCol.Item().Text($"{entry.Timestamp:yyyy-MM-dd HH:mm:ss}").FontSize(10).Bold();
                                entryCol.Item().PaddingTop(5).Text(entry.Content).FontSize(12);

                                if (entry.Attachments != null && entry.Attachments.Count > 0)
                                {
                                    entryCol.Item().PaddingTop(8).Text("Załączone pliki źródłowe:").FontSize(10).Bold();
                                    foreach (var att in entry.Attachments)
                                    {
                                        entryCol.Item().Text($"• [{att.FileType}] {att.FileName} (Ścieżka: {att.FilePath})").FontSize(9).Italic();
                                    }
                                }
                            });
                        }
                    });

                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.CurrentPageNumber();
                        x.Span(" / ");
                        x.TotalPages();
                    });
                });
            });

            using (var stream = await file.OpenWriteAsync())
            {
                document.GeneratePdf(stream);
            }
        }
    }
}