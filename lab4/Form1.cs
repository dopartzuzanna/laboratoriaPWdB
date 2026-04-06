using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        //funkcja wczytywania
        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
            }
        }

        private void btnRotate_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null) return; 

            Bitmap bmp = new Bitmap(pictureBox1.Image);

            if (radioButton1.Checked)
                bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            else if (radioButton2.Checked)
                bmp.RotateFlip(RotateFlipType.Rotate180FlipNone);
            else if (radioButton3.Checked)
                bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);

            pictureBox1.Image = bmp;
        }

        private void btnUpsideDown_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null) return;
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            pictureBox1.Image = bmp;
        }

        private void btnInvert_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null) return;
            Bitmap bmp = new Bitmap(pictureBox1.Image);

            // Przechodzimy przez każdy piksel obrazka
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color oldColor = bmp.GetPixel(x, y);
                    Color newColor = Color.FromArgb(255 - oldColor.R, 255 - oldColor.G, 255 - oldColor.B);
                    bmp.SetPixel(x, y, newColor);
                }
            }
            pictureBox1.Image = bmp;
        }

        private void btnOnlyGreen_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null) return;

            Bitmap bmp = new Bitmap(pictureBox1.Image);

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);

                    if (!(c.G > c.R && c.G > c.B))
                    {
                        bmp.SetPixel(x, y, Color.Black);
                    }
                }
            }

            pictureBox1.Image = bmp;
        }
    }
}
