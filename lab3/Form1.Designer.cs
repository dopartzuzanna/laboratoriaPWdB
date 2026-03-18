namespace lab3
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dodaj = new System.Windows.Forms.Button();
            this.usun = new System.Windows.Forms.Button();
            this.zapis = new System.Windows.Forms.Button();
            this.odczyt = new System.Windows.Forms.Button();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Imie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nazwisko = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wiek = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stanowisko = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Imie,
            this.nazwisko,
            this.wiek,
            this.stanowisko});
            this.dataGridView1.Location = new System.Drawing.Point(12, 28);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(756, 313);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.MultiSelect = false;
            // 
            // dodaj
            // 
            this.dodaj.Location = new System.Drawing.Point(792, 129);
            this.dodaj.Name = "dodaj";
            this.dodaj.Size = new System.Drawing.Size(90, 55);
            this.dodaj.TabIndex = 1;
            this.dodaj.Text = "dodaj";
            this.dodaj.UseVisualStyleBackColor = true;
            this.dodaj.Click += new System.EventHandler(this.dodaj_Click);
            // 
            // usun
            // 
            this.usun.Location = new System.Drawing.Point(792, 207);
            this.usun.Name = "usun";
            this.usun.Size = new System.Drawing.Size(90, 55);
            this.usun.TabIndex = 2;
            this.usun.Text = "usun";
            this.usun.UseVisualStyleBackColor = true;
            this.usun.Click += new System.EventHandler(this.usun_Click);
            // 
            // zapis
            // 
            this.zapis.Location = new System.Drawing.Point(101, 365);
            this.zapis.Name = "zapis";
            this.zapis.Size = new System.Drawing.Size(265, 55);
            this.zapis.TabIndex = 3;
            this.zapis.Text = "zapis do .csv";
            this.zapis.UseVisualStyleBackColor = true;
            // 
            // odczyt
            // 
            this.odczyt.Location = new System.Drawing.Point(491, 365);
            this.odczyt.Name = "odczyt";
            this.odczyt.Size = new System.Drawing.Size(265, 55);
            this.odczyt.TabIndex = 4;
            this.odczyt.Text = "odczyt z .csv";
            this.odczyt.UseVisualStyleBackColor = true;
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.MinimumWidth = 6;
            this.Id.Name = "Id";
            this.Id.Width = 125;
            // 
            // Imie
            // 
            this.Imie.HeaderText = "Imię";
            this.Imie.MinimumWidth = 6;
            this.Imie.Name = "Imie";
            this.Imie.Width = 125;
            // 
            // nazwisko
            // 
            this.nazwisko.HeaderText = "Nazwisko";
            this.nazwisko.MinimumWidth = 6;
            this.nazwisko.Name = "nazwisko";
            this.nazwisko.Width = 125;
            // 
            // wiek
            // 
            this.wiek.HeaderText = "Wiek";
            this.wiek.MinimumWidth = 6;
            this.wiek.Name = "wiek";
            this.wiek.Width = 125;
            // 
            // stanowisko
            // 
            this.stanowisko.HeaderText = "Stanowisko";
            this.stanowisko.MinimumWidth = 6;
            this.stanowisko.Name = "stanowisko";
            this.stanowisko.Width = 125;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 450);
            this.Controls.Add(this.odczyt);
            this.Controls.Add(this.zapis);
            this.Controls.Add(this.usun);
            this.Controls.Add(this.dodaj);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Glowne";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button dodaj;
        private System.Windows.Forms.Button usun;
        private System.Windows.Forms.Button zapis;
        private System.Windows.Forms.Button odczyt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Imie;
        private System.Windows.Forms.DataGridViewTextBoxColumn nazwisko;
        private System.Windows.Forms.DataGridViewTextBoxColumn wiek;
        private System.Windows.Forms.DataGridViewTextBoxColumn stanowisko;
    }
}

