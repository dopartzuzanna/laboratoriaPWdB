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
                // If model has an Id, update existing record, otherwise insert new
                if (model.Id > 0)
                {
                    command.CommandText = @"
                        UPDATE Wnioski SET
                            Miejscowosc = @Miejscowosc,
                            Data = @Data,
                            NumerAlbumu = @NumerAlbumu,
                            ImieNazwisko = @ImieNazwisko,
                            Semestr = @Semestr,
                            Rok = @Rok,
                            Kierunek = @Kierunek,
                            Przedmiot = @Przedmiot,
                            Punkty = @Punkty,
                            Prowadzacy = @Prowadzacy,
                            Uzasadnienie = @Uzasadnienie,
                            Decyzja = @Decyzja,
                            CzlonekKomisji1 = @CzlonekKomisji1,
                            CzlonekKomisji2 = @CzlonekKomisji2,
                            CzlonekKomisji3 = @CzlonekKomisji3
                        WHERE Id = @Id;";

                    command.Parameters.AddWithValue("@Id", model.Id);
                }
                else
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
                }

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

                    // If we inserted a new record, set the Id on the model
                    if (model.Id == 0)
                    {
                        using (var idCmd = connection.CreateCommand())
                        {
                            idCmd.CommandText = "SELECT last_insert_rowid();";
                            var result = idCmd.ExecuteScalar();
                            if (result != null && int.TryParse(result.ToString(), out var newId))
                                model.Id = newId;
                        }
                    }
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
                            var model = new WniosekModel();
                            if (reader["Id"] != null && int.TryParse(reader["Id"].ToString(), out var id))
                                model.Id = id;
                            model.Miejscowosc = reader["Miejscowosc"]?.ToString() ?? string.Empty;
                            model.Data = reader["Data"]?.ToString() ?? string.Empty;
                            model.NumerAlbumu = reader["NumerAlbumu"]?.ToString() ?? string.Empty;
                            model.ImieNazwisko = reader["ImieNazwisko"]?.ToString() ?? string.Empty;
                            model.Semestr = reader["Semestr"]?.ToString() ?? string.Empty;
                            model.Rok = reader["Rok"]?.ToString() ?? string.Empty;
                            model.Kierunek = reader["Kierunek"]?.ToString() ?? string.Empty;
                            model.Przedmiot = reader["Przedmiot"]?.ToString() ?? string.Empty;
                            model.Punkty = reader["Punkty"]?.ToString() ?? string.Empty;
                            model.Prowadzacy = reader["Prowadzacy"]?.ToString() ?? string.Empty;
                            model.Uzasadnienie = reader["Uzasadnienie"]?.ToString() ?? string.Empty;
                            model.Decyzja = reader["Decyzja"]?.ToString() ?? string.Empty;
                            model.CzlonekKomisji1 = reader["CzlonekKomisji1"]?.ToString() ?? string.Empty;
                            model.CzlonekKomisji2 = reader["CzlonekKomisji2"]?.ToString() ?? string.Empty;
                            model.CzlonekKomisji3 = reader["CzlonekKomisji3"]?.ToString() ?? string.Empty;

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

        public System.Collections.Generic.List<WniosekModel> ReadAll()
        {
            var list = new System.Collections.Generic.List<WniosekModel>();
            using (var connection = new SQLiteConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Wnioski ORDER BY Id DESC;";
                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var model = new WniosekModel();
                            if (reader["Id"] != null && int.TryParse(reader["Id"].ToString(), out var id))
                                model.Id = id;
                            model.Miejscowosc = reader["Miejscowosc"]?.ToString() ?? string.Empty;
                            model.Data = reader["Data"]?.ToString() ?? string.Empty;
                            model.NumerAlbumu = reader["NumerAlbumu"]?.ToString() ?? string.Empty;
                            model.ImieNazwisko = reader["ImieNazwisko"]?.ToString() ?? string.Empty;
                            model.Semestr = reader["Semestr"]?.ToString() ?? string.Empty;
                            model.Rok = reader["Rok"]?.ToString() ?? string.Empty;
                            model.Kierunek = reader["Kierunek"]?.ToString() ?? string.Empty;
                            model.Przedmiot = reader["Przedmiot"]?.ToString() ?? string.Empty;
                            model.Punkty = reader["Punkty"]?.ToString() ?? string.Empty;
                            model.Prowadzacy = reader["Prowadzacy"]?.ToString() ?? string.Empty;
                            model.Uzasadnienie = reader["Uzasadnienie"]?.ToString() ?? string.Empty;
                            model.Decyzja = reader["Decyzja"]?.ToString() ?? string.Empty;
                            model.CzlonekKomisji1 = reader["CzlonekKomisji1"]?.ToString() ?? string.Empty;
                            model.CzlonekKomisji2 = reader["CzlonekKomisji2"]?.ToString() ?? string.Empty;
                            model.CzlonekKomisji3 = reader["CzlonekKomisji3"]?.ToString() ?? string.Empty;
                            list.Add(model);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error reading data: " + ex.Message);
                }
            }

            return list;
        }

        public System.Collections.Generic.List<WniosekModel> ReadPage(int page, int pageSize, string? nameFilter = null, string? dateFilter = null)
        {
            var list = new System.Collections.Generic.List<WniosekModel>();
            using (var connection = new SQLiteConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                var where = "";
                if (!string.IsNullOrWhiteSpace(nameFilter) || !string.IsNullOrWhiteSpace(dateFilter))
                {
                    var clauses = new System.Collections.Generic.List<string>();
                    if (!string.IsNullOrWhiteSpace(nameFilter))
                    {
                        clauses.Add("ImieNazwisko LIKE @NameFilter");
                        command.Parameters.AddWithValue("@NameFilter", "%" + nameFilter + "%");
                    }
                    if (!string.IsNullOrWhiteSpace(dateFilter))
                    {
                        clauses.Add("Data LIKE @DateFilter");
                        command.Parameters.AddWithValue("@DateFilter", "%" + dateFilter + "%");
                    }
                    where = "WHERE " + string.Join(" AND ", clauses);
                }

                var offset = (page - 1) * pageSize;
                command.CommandText = $"SELECT * FROM Wnioski {where} ORDER BY Id DESC LIMIT @Limit OFFSET @Offset;";
                command.Parameters.AddWithValue("@Limit", pageSize);
                command.Parameters.AddWithValue("@Offset", offset);
                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var model = new WniosekModel();
                            if (reader["Id"] != null && int.TryParse(reader["Id"].ToString(), out var id))
                                model.Id = id;
                            model.Miejscowosc = reader["Miejscowosc"]?.ToString() ?? string.Empty;
                            model.Data = reader["Data"]?.ToString() ?? string.Empty;
                            model.NumerAlbumu = reader["NumerAlbumu"]?.ToString() ?? string.Empty;
                            model.ImieNazwisko = reader["ImieNazwisko"]?.ToString() ?? string.Empty;
                            model.Semestr = reader["Semestr"]?.ToString() ?? string.Empty;
                            model.Rok = reader["Rok"]?.ToString() ?? string.Empty;
                            model.Kierunek = reader["Kierunek"]?.ToString() ?? string.Empty;
                            model.Przedmiot = reader["Przedmiot"]?.ToString() ?? string.Empty;
                            model.Punkty = reader["Punkty"]?.ToString() ?? string.Empty;
                            model.Prowadzacy = reader["Prowadzacy"]?.ToString() ?? string.Empty;
                            model.Uzasadnienie = reader["Uzasadnienie"]?.ToString() ?? string.Empty;
                            model.Decyzja = reader["Decyzja"]?.ToString() ?? string.Empty;
                            model.CzlonekKomisji1 = reader["CzlonekKomisji1"]?.ToString() ?? string.Empty;
                            model.CzlonekKomisji2 = reader["CzlonekKomisji2"]?.ToString() ?? string.Empty;
                            model.CzlonekKomisji3 = reader["CzlonekKomisji3"]?.ToString() ?? string.Empty;
                            list.Add(model);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error reading data: " + ex.Message);
                }
            }

            return list;
        }

        public int CountFiltered(string? nameFilter = null, string? dateFilter = null)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                var where = "";
                if (!string.IsNullOrWhiteSpace(nameFilter) || !string.IsNullOrWhiteSpace(dateFilter))
                {
                    var clauses = new System.Collections.Generic.List<string>();
                    if (!string.IsNullOrWhiteSpace(nameFilter))
                    {
                        clauses.Add("ImieNazwisko LIKE @NameFilter");
                        command.Parameters.AddWithValue("@NameFilter", "%" + nameFilter + "%");
                    }
                    if (!string.IsNullOrWhiteSpace(dateFilter))
                    {
                        clauses.Add("Data LIKE @DateFilter");
                        command.Parameters.AddWithValue("@DateFilter", "%" + dateFilter + "%");
                    }
                    where = "WHERE " + string.Join(" AND ", clauses);
                }

                command.CommandText = $"SELECT COUNT(*) FROM Wnioski {where};";
                try
                {
                    connection.Open();
                    var result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out var count))
                        return count;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error counting data: " + ex.Message);
                }
            }

            return 0;
        }

        public void DeleteById(int id)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM Wnioski WHERE Id = @Id;";
                command.Parameters.AddWithValue("@Id", id);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error deleting data: " + ex.Message);
                }
            }
        }

        public void ClearAll()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM Wnioski;";
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error clearing data: " + ex.Message);
                }
            }
        }
    }
}
