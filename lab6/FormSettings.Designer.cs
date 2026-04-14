namespace lab6
{
    partial class FormSettings
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numX = new System.Windows.Forms.NumericUpDown();
            this.numY = new System.Windows.Forms.NumericUpDown();
            this.numDydelfy = new System.Windows.Forms.NumericUpDown();
            this.numCzas = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numKrokodyle = new System.Windows.Forms.NumericUpDown();
            this.numSzopy = new System.Windows.Forms.NumericUpDown();
            this.btnZapisz = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDydelfy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCzas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKrokodyle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSzopy)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "plansza";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(213, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Dydelfy";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(213, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Krokodyle";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "X";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Y";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 222);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "czas ";
            // 
            // numX
            // 
            this.numX.Location = new System.Drawing.Point(63, 87);
            this.numX.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numX.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numX.Name = "numX";
            this.numX.Size = new System.Drawing.Size(94, 22);
            this.numX.TabIndex = 6;
            this.numX.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // numY
            // 
            this.numY.Location = new System.Drawing.Point(63, 134);
            this.numY.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numY.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numY.Name = "numY";
            this.numY.Size = new System.Drawing.Size(94, 22);
            this.numY.TabIndex = 7;
            this.numY.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // numDydelfy
            // 
            this.numDydelfy.Location = new System.Drawing.Point(216, 88);
            this.numDydelfy.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numDydelfy.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDydelfy.Name = "numDydelfy";
            this.numDydelfy.Size = new System.Drawing.Size(94, 22);
            this.numDydelfy.TabIndex = 8;
            this.numDydelfy.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numCzas
            // 
            this.numCzas.Location = new System.Drawing.Point(63, 216);
            this.numCzas.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numCzas.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numCzas.Name = "numCzas";
            this.numCzas.Size = new System.Drawing.Size(94, 22);
            this.numCzas.TabIndex = 9;
            this.numCzas.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(213, 187);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 16);
            this.label7.TabIndex = 10;
            this.label7.Text = "Szopy";
            // 
            // numKrokodyle
            // 
            this.numKrokodyle.Location = new System.Drawing.Point(216, 151);
            this.numKrokodyle.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numKrokodyle.Name = "numKrokodyle";
            this.numKrokodyle.Size = new System.Drawing.Size(94, 22);
            this.numKrokodyle.TabIndex = 11;
            // 
            // numSzopy
            // 
            this.numSzopy.Location = new System.Drawing.Point(216, 216);
            this.numSzopy.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numSzopy.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numSzopy.Name = "numSzopy";
            this.numSzopy.Size = new System.Drawing.Size(94, 22);
            this.numSzopy.TabIndex = 12;
            this.numSzopy.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // btnZapisz
            // 
            this.btnZapisz.Location = new System.Drawing.Point(131, 255);
            this.btnZapisz.Name = "btnZapisz";
            this.btnZapisz.Size = new System.Drawing.Size(75, 23);
            this.btnZapisz.TabIndex = 13;
            this.btnZapisz.Text = "zapisz";
            this.btnZapisz.UseVisualStyleBackColor = true;
            this.btnZapisz.Click += new System.EventHandler(this.btnZapisz_Click);
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 290);
            this.Controls.Add(this.btnZapisz);
            this.Controls.Add(this.numSzopy);
            this.Controls.Add(this.numKrokodyle);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numCzas);
            this.Controls.Add(this.numDydelfy);
            this.Controls.Add(this.numY);
            this.Controls.Add(this.numX);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormSettings";
            this.Text = "FormSettings";
            ((System.ComponentModel.ISupportInitialize)(this.numX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDydelfy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCzas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKrokodyle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSzopy)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numX;
        private System.Windows.Forms.NumericUpDown numY;
        private System.Windows.Forms.NumericUpDown numDydelfy;
        private System.Windows.Forms.NumericUpDown numCzas;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numKrokodyle;
        private System.Windows.Forms.NumericUpDown numSzopy;
        private System.Windows.Forms.Button btnZapisz;
    }
}