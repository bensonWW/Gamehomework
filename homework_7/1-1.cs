using Homework7;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

internal class Q1 : Form1
{
    void createImage()
    {
        pictureBoxes.Clear();
        panel1.Controls.Clear();
        int width =  panel1.Size.Width;
        int height =  panel1.Size.Height;
        for (int i = 0; i < column; i++) 
        {
            for (int j = 0; j < row; j++) 
            {
                PictureBox pictureBox = new PictureBox();
                panel1.Controls.Add(pictureBox);
                pictureBox.Location = new System.Drawing.Point(i * (width / column), j * (height / row));
                pictureBox.Size = new System.Drawing.Size(width / column, height / row);
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.ImageLocation = "PokerBack-3.png";
                pictureBox.Visible = true;
                pictureBoxes.Add(pictureBox);
            }
        }
    }
    void changeState() 
    {
        label1.Text = "¦æ : " + column.ToString();
        label2.Text = "¦C : " + row.ToString();
        panel1.Invalidate();
    }
    public override void button1_Click(object sender, EventArgs e)
    {
        column += 1;
        createImage();
        changeState();
    }
    public override void button2_Click(object sender, EventArgs e)
    {
        if (column - 1 > 0)
        {
            column -= 1;
            createImage();
            changeState();
        }
    }
    public override void button3_Click(object sender, EventArgs e)
    {
        row += 1;
        createImage();
        changeState();
        
    }
    public override void button4_Click(object sender, EventArgs e)
    {
        if (row - 1 > 0)
        {
            row -= 1;
            createImage();
            changeState();
        }
    }
    public Q1()
    {
        createImage();
    }
    private List<PictureBox> pictureBoxes = new List<PictureBox>();
    private int column = 1;
    private int row = 1;
}