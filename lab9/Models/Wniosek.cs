using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace lab9.Models
{
    public class Wniosek : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (Equals(field, value))
                return false;
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return true;
        }

        private int _id;
        public int Id { get => _id; set => SetProperty(ref _id, value); }

        private string? _miejscowoscData;
        public string? MiejscowoscData { get => _miejscowoscData; set => SetProperty(ref _miejscowoscData, value); }

        private string? _album;
        public string? Album { get => _album; set => SetProperty(ref _album, value); }

        private string? _nazwiskoImie;
        public string? NazwiskoImie { get => _nazwiskoImie; set => SetProperty(ref _nazwiskoImie, value); }

        private string? _semestrRok;
        public string? SemestrRok { get => _semestrRok; set => SetProperty(ref _semestrRok, value); }

        private string? _kierunek;
        public string? Kierunek { get => _kierunek; set => SetProperty(ref _kierunek, value); }

        private string? _stopien;
        public string? Stopien { get => _stopien; set => SetProperty(ref _stopien, value); }

        private string? _nazwaPrzedmiotu;
        public string? NazwaPrzedmiotu { get => _nazwaPrzedmiotu; set => SetProperty(ref _nazwaPrzedmiotu, value); }

        private string? _ects;
        public string? ECTS { get => _ects; set => SetProperty(ref _ects, value); }

        private string? _prowadzacy;
        public string? Prowadzacy { get => _prowadzacy; set => SetProperty(ref _prowadzacy, value); }

        private string? _uzasadnienie;
        public string? Uzasadnienie { get => _uzasadnienie; set => SetProperty(ref _uzasadnienie, value); }

        private string? _dataPodpisStudenta;
        public string? DataPodpisStudenta { get => _dataPodpisStudenta; set => SetProperty(ref _dataPodpisStudenta, value); }

        private string? _decyzja;
        public string? Decyzja { get => _decyzja; set => SetProperty(ref _decyzja, value); }

        private string? _skladKomisji;
        public string? SkladKomisji { get => _skladKomisji; set => SetProperty(ref _skladKomisji, value); }

        private string? _dataDecyzji;
        public string? DataDecyzji { get => _dataDecyzji; set => SetProperty(ref _dataDecyzji, value); }

        private string? _podpisProdziekana;
        public string? PodpisProdziekana { get => _podpisProdziekana; set => SetProperty(ref _podpisProdziekana, value); }
    }
}
