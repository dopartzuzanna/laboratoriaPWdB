namespace lab6
{
    partial class MenuForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnUstawienia = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label1.Location = new System.Drawing.Point(35, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 74);
            this.label1.TabIndex = 0;
            this.label1.Text = "GDZIE JEST DYDELF?";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(41, 129);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(178, 35);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnUstawienia
            // 
            this.btnUstawienia.Location = new System.Drawing.Point(41, 191);
            this.btnUstawienia.Name = "btnUstawienia";
            this.btnUstawienia.Size = new System.Drawing.Size(178, 35);
            this.btnUstawienia.TabIndex = 2;
            this.btnUstawienia.Text = "USTAWIENIA";
            this.btnUstawienia.UseVisualStyleBackColor = true;
            this.btnUstawienia.Click += new System.EventHandler(this.btnUstawienia_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(41, 251);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(178, 35);
            this.button3.TabIndex = 3;
            this.button3.Text = "KONIEC";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 321);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnUstawienia);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label1);
            this.Name = "MenuForm";
            this.Text = "MenuForm";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnUstawienia;
        private System.Windows.Forms.Button button3;
    }
}

