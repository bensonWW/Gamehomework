using Homework8;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
public partial class Q1 : Form1
{
    void createPictureBox(int column, int row)
    {
        for (int j = 0; j < row; j++)
        {
            for (int i = 0; i < column; i++)
            {
                PictureBox pb = new PictureBox();
                pb.Width = this.ClientSize.Width / column;
                pb.Height = this.ClientSize.Height / row;

                pb.Location = new Point(i * pb.Width, j * pb.Height);

                pb.SizeMode = PictureBoxSizeMode.StretchImage;

                pb.Image = bitMaps[j * column + i];
                pictureBoxes.Add(pb);
                this.Controls.Add(pb);
            }
        }

    }
    void cutPicture(int column, int row)
    {
        Bitmap source = new Bitmap("dolamo.png");
        int pieceW = source.Width / column;
        int pieceH = source.Height / row;
        for (int r = 0; r < row; r++)
        {
            for (int c = 0; c < column; c++)
            {
                Rectangle rect = new Rectangle(
                    c * pieceW,
                    r * pieceH,
                    pieceW,
                    pieceH
                );
                Bitmap piece = source.Clone(rect, source.PixelFormat);
                bitMaps.Add(piece);
            }
        }
    }
    public Q1()
    {
        

    }

    private void button2_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < pictureBoxes.Count; i++)
        {
            this.Controls.Remove(pictureBoxes[i]);
        }
        bitMaps.Clear();
        cutPicture(4, 5);
        createPictureBox(4, 5);
    }

    private void button1_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < pictureBoxes.Count; i++)
        {
            this.Controls.Remove(pictureBoxes[i]);
        }
        bitMaps.Clear();
        cutPicture(3, 3);
        createPictureBox(3, 3);
    }
    private List<Bitmap> bitMaps = new List<Bitmap>();
    private List<PictureBox> pictureBoxes = new List<PictureBox>();
}