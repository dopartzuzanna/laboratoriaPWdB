using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace lab9.Models
{
    public class WniosekModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected bool RaiseAndSetIfChanged<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _miejscowosc = string.Empty;
        public string Miejscowosc { get => _miejscowosc; set => RaiseAndSetIfChanged(ref _miejscowosc, value); }

        private string _data = string.Empty;
        public string Data { get => _data; set => RaiseAndSetIfChanged(ref _data, value); }

        private string _numerAlbumu = string.Empty;
        public string NumerAlbumu { get => _numerAlbumu; set => RaiseAndSetIfChanged(ref _numerAlbumu, value); }

        private string _imieNazwisko = string.Empty;
        public string ImieNazwisko { get => _imieNazwisko; set => RaiseAndSetIfChanged(ref _imieNazwisko, value); }

        private string _semestr = string.Empty;
        public string Semestr { get => _semestr; set => RaiseAndSetIfChanged(ref _semestr, value); }

        private string _rok = string.Empty;
        public string Rok { get => _rok; set => RaiseAndSetIfChanged(ref _rok, value); }

        private string _kierunek = string.Empty;
        public string Kierunek { get => _kierunek; set => RaiseAndSetIfChanged(ref _kierunek, value); }

        private string _przedmiot = string.Empty;
        public string Przedmiot { get => _przedmiot; set => RaiseAndSetIfChanged(ref _przedmiot, value); }

        private string _punkty = string.Empty;
        public string Punkty { get => _punkty; set => RaiseAndSetIfChanged(ref _punkty, value); }

        private string _prowadzacy = string.Empty;
        public string Prowadzacy { get => _prowadzacy; set => RaiseAndSetIfChanged(ref _prowadzacy, value); }

        private string _uzasadnienie = string.Empty;
        public string Uzasadnienie { get => _uzasadnienie; set => RaiseAndSetIfChanged(ref _uzasadnienie, value); }

        private string _decyzja = string.Empty;
        public string Decyzja { get => _decyzja; set => RaiseAndSetIfChanged(ref _decyzja, value); }

        private string _czlonekKomisji1 = string.Empty;
        public string CzlonekKomisji1 { get => _czlonekKomisji1; set => RaiseAndSetIfChanged(ref _czlonekKomisji1, value); }

        private string _czlonekKomisji2 = string.Empty;
        public string CzlonekKomisji2 { get => _czlonekKomisji2; set => RaiseAndSetIfChanged(ref _czlonekKomisji2, value); }

        private string _czlonekKomisji3 = string.Empty;
        public string CzlonekKomisji3 { get => _czlonekKomisji3; set => RaiseAndSetIfChanged(ref _czlonekKomisji3, value); }
    }
}
