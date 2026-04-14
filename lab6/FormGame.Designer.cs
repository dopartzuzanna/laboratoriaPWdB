namespace lab6
{
    partial class FormGame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblCzas = new System.Windows.Forms.Label();
            this.gridPlansza = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // lblCzas
            // 
            this.lblCzas.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblCzas.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblCzas.Location = new System.Drawing.Point(0, 421);
            this.lblCzas.Name = "lblCzas";
            this.lblCzas.Size = new System.Drawing.Size(431, 29);
            this.lblCzas.TabIndex = 0;
            this.lblCzas.Text = "label1";
            this.lblCzas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCzas.Click += new System.EventHandler(this.lblCzas_Click);
            // 
            // gridPlansza
            // 
            this.gridPlansza.ColumnCount = 2;
            this.gridPlansza.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.gridPlansza.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.gridPlansza.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridPlansza.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPlansza.Location = new System.Drawing.Point(0, 0);
            this.gridPlansza.Name = "gridPlansza";
            this.gridPlansza.RowCount = 2;
            this.gridPlansza.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.gridPlansza.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.gridPlansza.Size = new System.Drawing.Size(431, 421);
            this.gridPlansza.TabIndex = 1;
            // 
            // FormGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 450);
            this.Controls.Add(this.gridPlansza);
            this.Controls.Add(this.lblCzas);
            this.Name = "FormGame";
            this.Text = "FormGame";
            this.Load += new System.EventHandler(this.FormGame_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblCzas;
        private System.Windows.Forms.TableLayoutPanel gridPlansza;
    }
}