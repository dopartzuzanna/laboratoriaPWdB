using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            }

            using (var dlg = new Form2(nextId))
            {
                var result = dlg.ShowDialog();
                if (result == DialogResult.OK)
                {
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
    }
}
