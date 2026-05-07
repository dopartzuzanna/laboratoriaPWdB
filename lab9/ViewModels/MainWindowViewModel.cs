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

        public void LoadFromHistory(WniosekModel model)
        {
            if (model == null) return;
            AktualnyWniosek = model;
            StatusMessage = "Wczytano wniosek z historii.";
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
