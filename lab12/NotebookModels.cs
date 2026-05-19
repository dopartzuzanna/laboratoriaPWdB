using System;
using System.Collections.Generic;

namespace lab12
{
    // 1. Klasa reprezentująca załącznik do wpisu
    public class AnalysisAttachment
    {
        public string FileName { get; set; } = "";     // Nazwa pliku (np. "wyniki.csv")
        public string FilePath { get; set; } = "";     // Ścieżka do pliku na dysku
        public string FileType { get; set; } = "";     // Rozszerzenie (FASTA, CSV, PNG itp.)
    }

    // 2. Klasa reprezentująca pojedynczy wpis w notatniku
    public class NotebookEntry
    {
        public string Content { get; set; } = "";      // Treść notatki / opis działań
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public List<AnalysisAttachment> Attachments { get; set; } = new();
    }

    // 3. Klasa reprezentująca całą sesję analityczną
    public class AnalysisSession
    {
        public string Title { get; set; } = "";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public List<NotebookEntry> Entries { get; set; } = new();

        // Pomocnicze podsumowanie do wyświetlenia na liście sesji
        public string Summary => $"{Title} ({CreatedDate:yyyy-MM-dd}) - Wpisów: {Entries.Count}";
    }
}