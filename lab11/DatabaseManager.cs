using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace lab11
{
    public class DatabaseManager
    {
        private string connectionString = "Data Source=biolab.db;Version=3;";

        public DatabaseManager()
        {
            // Przy starcie tworzymy tabelę, jeśli jej nie ma 
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string sql = @"CREATE TABLE IF NOT EXISTS Samples (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                SampleId TEXT UNIQUE,
                                Name TEXT,
                                Type TEXT,
                                CollectionDate TEXT,
                                Description TEXT)";
                var cmd = new SQLiteCommand(sql, conn);
                cmd.ExecuteNonQuery(); 
            }
        }

        public void AddSample(BioSample s)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open(); 
                string sql = "INSERT INTO Samples (SampleId, Name, Type, CollectionDate, Description) VALUES (@sid, @n, @t, @d, @desc)";
                var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@sid", s.SampleId); 
                cmd.Parameters.AddWithValue("@n", s.Name);
                cmd.Parameters.AddWithValue("@t", s.Type);
                cmd.Parameters.AddWithValue("@d", s.CollectionDate.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@desc", s.Description);
                cmd.ExecuteNonQuery(); 
            }
        }

        public List<BioSample> GetAllSamples()
        {
            var list = new List<BioSample>();
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM Samples";
                var cmd = new SQLiteCommand(sql, conn);
                using (var reader = cmd.ExecuteReader()) 
                {
                    while (reader.Read()) 
                    {
                        list.Add(new BioSample {
                            Id = reader.GetInt32(0), 
                            SampleId = reader.GetString(1), 
                            Name = reader.GetString(2),
                            Type = reader.GetString(3),
                            CollectionDate = DateTimeOffset.Parse(reader.GetString(4)),
                            Description = reader.GetString(5)
                        });
                    }
                }
            }
            return list;
        }

        public void DeleteSample(int id)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM Samples WHERE Id = @id";
                var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateSample(BioSample s)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string sql = @"UPDATE Samples 
                        SET SampleId = @sid, Name = @n, Type = @t, 
                            CollectionDate = @d, Description = @desc 
                        WHERE Id = @id";
                var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@sid", s.SampleId);
                cmd.Parameters.AddWithValue("@n", s.Name);
                cmd.Parameters.AddWithValue("@t", s.Type);
                cmd.Parameters.AddWithValue("@d", s.CollectionDate.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@desc", s.Description);
                cmd.Parameters.AddWithValue("@id", s.Id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
