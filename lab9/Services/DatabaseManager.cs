using lab9.Models;
using System;
using System.Data.SQLite;
using System.IO;

namespace lab9.Services
{
    public class DatabaseManager
    {
        private readonly string _dbPath;
        private readonly string _connectionString;

        public DatabaseManager(string? dbDirectory = null)
        {
            dbDirectory ??= AppContext.BaseDirectory;
            _dbPath = Path.Combine(dbDirectory, "wnioski.db");
            _connectionString = $"Data Source={_dbPath};Version=3;";

            EnsureDatabase();
        }

        private void EnsureDatabase()
        {
            if (!File.Exists(_dbPath))
            {
                SQLiteConnection.CreateFile(_dbPath);
            }

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Wnioski (
                            Id INTEGER PRIMARY KEY,
                            Miejscowosc TEXT,
                            Data TEXT,
                            NumerAlbumu TEXT,
                            ImieNazwisko TEXT,
                            Semestr TEXT,
                            Rok TEXT,
                            Kierunek TEXT,
                            Przedmiot TEXT,
                            Punkty TEXT,
                            Prowadzacy TEXT,
                            Uzasadnienie TEXT,
                            Decyzja TEXT,
                            CzlonekKomisji1 TEXT,
                            CzlonekKomisji2 TEXT,
                            CzlonekKomisji3 TEXT
                        );";
                    command.ExecuteNonQuery();
                }
            }
        }

        public void WriteData(WniosekModel model)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                    INSERT INTO Wnioski (
                        Miejscowosc, Data, NumerAlbumu, ImieNazwisko, Semestr, Rok, Kierunek,
                        Przedmiot, Punkty, Prowadzacy, Uzasadnienie, Decyzja,
                        CzlonekKomisji1, CzlonekKomisji2, CzlonekKomisji3
                    ) VALUES (
                        @Miejscowosc, @Data, @NumerAlbumu, @ImieNazwisko, @Semestr, @Rok, @Kierunek,
                        @Przedmiot, @Punkty, @Prowadzacy, @Uzasadnienie, @Decyzja,
                        @CzlonekKomisji1, @CzlonekKomisji2, @CzlonekKomisji3
                    );";

                command.Parameters.AddWithValue("@Miejscowosc", model.Miejscowosc ?? string.Empty);
                command.Parameters.AddWithValue("@Data", model.Data ?? string.Empty);
                command.Parameters.AddWithValue("@NumerAlbumu", model.NumerAlbumu ?? string.Empty);
                command.Parameters.AddWithValue("@ImieNazwisko", model.ImieNazwisko ?? string.Empty);
                command.Parameters.AddWithValue("@Semestr", model.Semestr ?? string.Empty);
                command.Parameters.AddWithValue("@Rok", model.Rok ?? string.Empty);
                command.Parameters.AddWithValue("@Kierunek", model.Kierunek ?? string.Empty);
                command.Parameters.AddWithValue("@Przedmiot", model.Przedmiot ?? string.Empty);
                command.Parameters.AddWithValue("@Punkty", model.Punkty ?? string.Empty);
                command.Parameters.AddWithValue("@Prowadzacy", model.Prowadzacy ?? string.Empty);
                command.Parameters.AddWithValue("@Uzasadnienie", model.Uzasadnienie ?? string.Empty);
                command.Parameters.AddWithValue("@Decyzja", model.Decyzja ?? string.Empty);
                command.Parameters.AddWithValue("@CzlonekKomisji1", model.CzlonekKomisji1 ?? string.Empty);
                command.Parameters.AddWithValue("@CzlonekKomisji2", model.CzlonekKomisji2 ?? string.Empty);
                command.Parameters.AddWithValue("@CzlonekKomisji3", model.CzlonekKomisji3 ?? string.Empty);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error writing data: " + ex.Message);
                }
            }
        }

        public WniosekModel? ReadData()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Wnioski ORDER BY Id DESC LIMIT 1;";
                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var model = new WniosekModel
                            {
                                Miejscowosc = reader["Miejscowosc"]?.ToString() ?? string.Empty,
                                Data = reader["Data"]?.ToString() ?? string.Empty,
                                NumerAlbumu = reader["NumerAlbumu"]?.ToString() ?? string.Empty,
                                ImieNazwisko = reader["ImieNazwisko"]?.ToString() ?? string.Empty,
                                Semestr = reader["Semestr"]?.ToString() ?? string.Empty,
                                Rok = reader["Rok"]?.ToString() ?? string.Empty,
                                Kierunek = reader["Kierunek"]?.ToString() ?? string.Empty,
                                Przedmiot = reader["Przedmiot"]?.ToString() ?? string.Empty,
                                Punkty = reader["Punkty"]?.ToString() ?? string.Empty,
                                Prowadzacy = reader["Prowadzacy"]?.ToString() ?? string.Empty,
                                Uzasadnienie = reader["Uzasadnienie"]?.ToString() ?? string.Empty,
                                Decyzja = reader["Decyzja"]?.ToString() ?? string.Empty,
                                CzlonekKomisji1 = reader["CzlonekKomisji1"]?.ToString() ?? string.Empty,
                                CzlonekKomisji2 = reader["CzlonekKomisji2"]?.ToString() ?? string.Empty,
                                CzlonekKomisji3 = reader["CzlonekKomisji3"]?.ToString() ?? string.Empty
                            };

                            return model;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error reading data: " + ex.Message);
                }
            }

            return null;
        }
    }
}
