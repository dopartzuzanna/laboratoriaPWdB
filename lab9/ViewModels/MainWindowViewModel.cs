using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using lab9.Models;
using lab9.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace lab9.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private WniosekModel _aktualnyWniosek;
        private string _statusMessage = string.Empty;
        private readonly DatabaseManager _dbManager;
        private readonly Stack<WniosekModel> _undoStack = new Stack<WniosekModel>();

        public event PropertyChangedEventHandler? PropertyChanged;

        public WniosekModel AktualnyWniosek
        {
            get => _aktualnyWniosek;
            set
            {
                if (Equals(_aktualnyWniosek, value))
                    return;
                _aktualnyWniosek = value;
                OnPropertyChanged();
            }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                if (_statusMessage == value) return;
                _statusMessage = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand LoadCommand { get; }
        public ICommand ClearCommand { get; }

        // expose history for external windows
        public ObservableCollection<WniosekModel> GetHistoryItems()
        {
            var list = _dbManager.ReadAll();
            return new ObservableCollection<WniosekModel>(list);
        }

        public ObservableCollection<WniosekModel> GetHistoryPage(int page, int pageSize, string? nameFilter = null, string? dateFilter = null)
        {
            var list = _dbManager.ReadPage(page, pageSize, nameFilter, dateFilter);
            return new ObservableCollection<WniosekModel>(list);
        }

        public int GetHistoryCount(string? nameFilter = null, string? dateFilter = null)
        {
            return _dbManager.CountFiltered(nameFilter, dateFilter);
        }

        public void LoadFromHistory(WniosekModel model)
        {
            if (model == null) return;
            AktualnyWniosek = model;
            StatusMessage = "Wczytano wniosek z historii.";
        }

        public void DeleteFromHistory(WniosekModel model)
        {
            if (model == null) return;
            try
            {
                if (model.Id > 0)
                {
                    // push to undo stack before deleting
                    _undoStack.Push(new WniosekModel
                    {
                        Id = model.Id,
                        Miejscowosc = model.Miejscowosc,
                        Data = model.Data,
                        NumerAlbumu = model.NumerAlbumu,
                        ImieNazwisko = model.ImieNazwisko,
                        Semestr = model.Semestr,
                        Rok = model.Rok,
                        Kierunek = model.Kierunek,
                        Przedmiot = model.Przedmiot,
                        Punkty = model.Punkty,
                        Prowadzacy = model.Prowadzacy,
                        Uzasadnienie = model.Uzasadnienie,
                        Decyzja = model.Decyzja,
                        CzlonekKomisji1 = model.CzlonekKomisji1,
                        CzlonekKomisji2 = model.CzlonekKomisji2,
                        CzlonekKomisji3 = model.CzlonekKomisji3
                    });

                    _dbManager.DeleteById(model.Id);
                    StatusMessage = "Usunięto wpis z historii. (Możesz cofnąć)";
                }
                else
                {
                    StatusMessage = "Nie można usunąć wpisu bez Id.";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = "Błąd usuwania: " + ex.Message;
            }
        }

        public void UndoDelete()
        {
            if (_undoStack.Count == 0)
            {
                StatusMessage = "Brak operacji do cofnięcia.";
                return;
            }

            var model = _undoStack.Pop();
            try
            {
                // write back the deleted entry (it will insert with original Id if possible)
                _dbManager.WriteData(model);
                StatusMessage = "Cofnięto usunięcie.";
            }
            catch (Exception ex)
            {
                StatusMessage = "Błąd cofania: " + ex.Message;
            }
        }

        public void ClearHistory()
        {
            try
            {
                _dbManager.ClearAll();
                StatusMessage = "Wyczyszczono historię wniosków.";
            }
            catch (Exception ex)
            {
                StatusMessage = "Błąd czyszczenia historii: " + ex.Message;
            }
        }

        public MainWindowViewModel()
        {
            _dbManager = new DatabaseManager();
            _aktualnyWniosek = new WniosekModel();

            SaveCommand = new RelayCommand(_ => ExecuteSave());
            LoadCommand = new RelayCommand(_ => ExecuteLoad());
            ClearCommand = new RelayCommand(_ => ExecuteClear());
        }

        private void ExecuteClear()
        {
            try
            {
                AktualnyWniosek = new WniosekModel();
                StatusMessage = "Wyczyszczono formularz.";
            }
            catch (Exception ex)
            {
                StatusMessage = "Błąd podczas czyszczenia: " + ex.Message;
            }
        }

        private void ExecuteSave()
        {
            try
            {
                _dbManager.WriteData(AktualnyWniosek);
                StatusMessage = "Zapisano wniosek pomyślnie.";
                Console.WriteLine(StatusMessage);
            }
            catch (Exception ex)
            {
                StatusMessage = "Błąd zapisu: " + ex.Message;
                Console.WriteLine(StatusMessage);
            }
        }

        private void ExecuteLoad()
        {
            try
            {
                var model = _dbManager.ReadData();
                if (model != null)
                {
                    AktualnyWniosek = model;
                    StatusMessage = "Wczytano wniosek pomyślnie.";
                    Console.WriteLine(StatusMessage);
                }
                else
                {
                    StatusMessage = "Brak zapisanych wniosków.";
                    Console.WriteLine(StatusMessage);
                }
            }
            catch (Exception ex)
            {
                StatusMessage = "Błąd wczytywania: " + ex.Message;
                Console.WriteLine(StatusMessage);
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Predicate<object?>? _canExecute;

        public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;

        public void Execute(object? parameter) => _execute(parameter);

        public event EventHandler? CanExecuteChanged;

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
