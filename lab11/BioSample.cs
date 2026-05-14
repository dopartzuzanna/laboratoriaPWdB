using System;
using System.Collections.Generic;
using System.Text;

namespace lab11
{
    public class BioSample
    {
        public int Id { get; set; } // Unikalny klucz dla SQLite
        public string SampleId { get; set; } = ""; // Np. "DNA-2024-001"
        public string Name { get; set; } = "";
        public string Type { get; set; } = "DNA"; // DNA, RNA, Białko, Inny
        public DateTimeOffset CollectionDate { get; set; } = DateTimeOffset.Now;
        public string Description { get; set; } = "";

        // Metoda pomocnicza, która zbierze dane do kodu QR
        public string GetQrData() => $"ID:{SampleId}|Name:{Name}|Type:{Type}|Date:{CollectionDate:yyyy-MM-dd}";
    }
}
