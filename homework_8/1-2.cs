using Homework8;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
internal class Q2 : Form2
{
    void createPictureBox(int column, int row,Panel panel)
    {
        for (int j = 0; j < row; j++)
        {
            for (int i = 0; i < column; i++)
            {
                PictureBox pb = new PictureBox();
                panel.Controls.Add(pb);
                pb.Width = panel.ClientSize.Width / column;
                pb.Height = panel.ClientSize.Height / row;

                pb.Location = new Point(i * pb.Width, j * pb.Height);

                pb.SizeMode = PictureBoxSizeMode.StretchImage;

                pb.Image = bitMaps[j * column + i];
                pictureBoxes.Add(pb);
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
    private void paintLine(object sender, PaintEventArgs g)
    {
        int height = panel1.Size.Height / row;
        int width = panel1.Size.Width / column;
        Pen pen = new Pen(Color.Red, 3);
        for (int i = 0; i <= column; i++)
        {
            g.Graphics.DrawLine(pen,i * width, 0,i * width, panel1.Size.Height);
        }
        for (int i = 0; i <= row; i++) {
            g.Graphics.DrawLine(pen, 0,i * height, panel1.Size.Width,i * height);
        }
    }
    public Q2()
    {

    }
    public override void button1_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < pictureBoxes.Count; i++)
        {
            panel2.Controls.Remove(pictureBoxes[i]);
        }
        bitMaps.Clear();
        cutPicture(3, 3);
        createPictureBox(3, 3,panel2);
        row = 3;
        column = 3;
        panel1.Paint +=paintLine;
        panel1.Refresh();
    }
    public override void button2_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < pictureBoxes.Count; i++)
        {
            panel2.Controls.Remove(pictureBoxes[i]);
        }
        bitMaps.Clear();
        cutPicture(4, 5);
        createPictureBox(4, 5,panel2);
        row = 4;
        column = 5;
        panel1.Paint += paintLine;
        panel1.Refresh();
    }
    private List<Bitmap> bitMaps = new List<Bitmap>();
    protected List<PictureBox> pictureBoxes = new List<PictureBox>();
    private int column;
    private int row;
}