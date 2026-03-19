using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dodaj_Click(object sender, EventArgs e)
        {
            int nextId = 1; 
            try
            {
                int idColIndex = 0;
                var idCol = dataGridView1.Columns
                    .Cast<DataGridViewColumn>()
                    .FirstOrDefault(c =>
                        string.Equals(c.HeaderText, "ID", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(c.Name, "ID", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(c.Name, "id", StringComparison.OrdinalIgnoreCase));

                if (idCol != null)
                    idColIndex = idCol.Index;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;
                    var cell = row.Cells[idColIndex].Value;
                    if (cell != null && int.TryParse(cell.ToString(), out int val))
                        nextId = Math.Max(nextId, val + 1);
                }
            }
            catch
            {
                // fallback: nextId zostaje 1 jeśli coś pójdzie nie tak
            }

            using (var dlg = new Form2(nextId))
            {
                var result = dlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // defensywna kontrola: upewnij się, że są kolumny przed dodaniem
                    if (dataGridView1.ColumnCount == 0)
                    {
                        MessageBox.Show("Brak kolumn w tabeli — dodanie wiersza niemożliwe.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    dataGridView1.Rows.Add(
                        dlg.EmployeeId,
                        dlg.FirstName,
                        dlg.LastName,
                        dlg.Age,
                        dlg.Position);
                }
            }
        }

        private void usun_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource != null)
            {
                var selected = dataGridView1.SelectedRows.Cast<DataGridViewRow>().ToList();
                if (selected.Count == 0 && dataGridView1.CurrentCell != null)
                    selected = new List<DataGridViewRow> { dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex] };

                foreach (var row in selected)
                {
                    if (row.DataBoundItem is DataRowView drv)
                    {
                        drv.Row.Delete();
                    }
                    else if (dataGridView1.DataSource is BindingSource bs)
                    {
                        var item = row.DataBoundItem;
                        if (item != null)
                        {
                            int idx = bs.IndexOf(item);
                            if (idx >= 0) bs.RemoveAt(idx);
                        }
                    }
                    else
                    {
                        var list = dataGridView1.DataSource as System.Collections.IList;
                        var item = row.DataBoundItem;
                        if (list != null && item != null)
                        {
                            list.Remove(item);
                        }
                    }
                }

                return;
            }

            if (dataGridView1.SelectedRows.Count > 0)
            {
                var rowsToRemove = dataGridView1.SelectedRows
                    .Cast<DataGridViewRow>()
                    .Where(r => !r.IsNewRow)
                    .ToList();

                foreach (var r in rowsToRemove)
                    dataGridView1.Rows.Remove(r);

                return;
            }

            if (dataGridView1.CurrentCell != null)
            {
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                if (rowIndex >= 0 && rowIndex < dataGridView1.Rows.Count)
                {
                    var row = dataGridView1.Rows[rowIndex];
                    if (!row.IsNewRow)
                        dataGridView1.Rows.RemoveAt(rowIndex);
                }
            }
        }

        private void zapisz_Click(object sender, EventArgs e)
        {
            if (dataGridView1.ColumnCount == 0)
            {
                MessageBox.Show("Brak kolumn w tabeli — brak danych do zapisania.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                sfd.DefaultExt = "csv";
                sfd.FileName = "export.csv";
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    using (var sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
                    {
                        // Nagłówek
                        var headers = dataGridView1.Columns
                            .Cast<DataGridViewColumn>()
                            .Select(c => EscapeCsvField(c.HeaderText ?? string.Empty));
                        sw.WriteLine(string.Join(",", headers));

                        // Wiersze
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.IsNewRow) continue;

                            var fields = new List<string>();
                            for (int i = 0; i < dataGridView1.ColumnCount; i++)
                            {
                                var cell = row.Cells[i].Value;
                                var text = cell?.ToString() ?? string.Empty;
                                fields.Add(EscapeCsvField(text));
                            }

                            sw.WriteLine(string.Join(",", fields));
                        }
                    }

                    MessageBox.Show($"Zapisano do pliku:\n{sfd.FileName}", "Zapisano", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd zapisu: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string EscapeCsvField(string input)
        {
            if (input == null) return string.Empty;
            // Podwajamy cudzysłowy i jeśli pole zawiera separator/newline/cudzysłów, otaczamy je cudzysłowami.
            var escaped = input.Replace("\"", "\"\"");
            if (escaped.Contains(",") || escaped.Contains("\"") || escaped.Contains("\r") || escaped.Contains("\n"))
                return $"\"{escaped}\"";
            return escaped;
        }

        private List<string> ParseCsvLine(string line)
        {
            var result = new List<string>();
            if (line == null)
                return result;

            var sb = new StringBuilder();
            bool inQuotes = false;
            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (inQuotes)
                {
                    if (c == '"')
                    {
                        // podwójny cudzysłów oznacza znak "
                        if (i + 1 < line.Length && line[i + 1] == '"')
                        {
                            sb.Append('"');
                            i++;
                        }
                        else
                        {
                            inQuotes = false;
                        }
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }
                else
                {
                    if (c == '"')
                    {
                        inQuotes = true;
                    }
                    else if (c == ',')
                    {
                        result.Add(sb.ToString());
                        sb.Clear();
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }
            }
            result.Add(sb.ToString());
            return result;
        }

        private void odczyt_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource != null)
            {
                MessageBox.Show("DataGridView jest powiązany z DataSource. Usuń powiązanie przed wczytaniem pliku.", "Odczyt CSV", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                ofd.DefaultExt = "csv";
                ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                if (ofd.ShowDialog() != DialogResult.OK)
                    return;

                string[] lines;
                try
                {
                    lines = File.ReadAllLines(ofd.FileName, Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd odczytu pliku: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var nonEmpty = lines.Where(l => !string.IsNullOrWhiteSpace(l)).ToList();
                if (nonEmpty.Count == 0)
                {
                    MessageBox.Show("Plik jest pusty.", "Odczyt CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int startIndex = 0;
                // rozpoznajemy, czy pierwsza linia to nagłówek:
                var firstFields = ParseCsvLine(nonEmpty[0]);
                bool looksLikeHeader = firstFields.Count > 0 &&
                    firstFields.Any(f => dataGridView1.Columns
                        .Cast<DataGridViewColumn>()
                        .Any(c => string.Equals(c.HeaderText, f, StringComparison.OrdinalIgnoreCase) ||
                                  string.Equals(c.Name, f, StringComparison.OrdinalIgnoreCase)));

                if (looksLikeHeader)
                    startIndex = 1;

                // Wyczyść istniejące wiersze i wczytaj nowe
                dataGridView1.Rows.Clear();

                for (int idx = startIndex; idx < nonEmpty.Count; idx++)
                {
                    var fields = ParseCsvLine(nonEmpty[idx]);
                    var rowValues = new object[dataGridView1.ColumnCount];
                    for (int i = 0; i < dataGridView1.ColumnCount; i++)
                    {
                        if (i < fields.Count)
                            rowValues[i] = fields[i];
                        else
                            rowValues[i] = string.Empty;
                    }

                    dataGridView1.Rows.Add(rowValues);
                }

                MessageBox.Show($"Wczytano {Math.Max(0, nonEmpty.Count - startIndex)} rekordów z pliku:\n{ofd.FileName}", "Odczyt zakończony", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
