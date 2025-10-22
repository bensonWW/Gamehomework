using System;
using System.Drawing;
using System.Windows.Forms;
using Homework5;
using System.Collections.Generic;

class Q3 : Form3
{
    public Q3()
    {
        InitializeLifetimeService();
    }

    public override void panel1_Paint(object sender, PaintEventArgs e)
    {
        for (int i = 0; i <= 51; i++)
        {
            imagepath.Add("C:\\Users\\benson\\Documents\\Game program\\homework_5\\pokerimage\\" + i.ToString() + ".png");
        }
        for(int i = 0;i <= 4; i++)
        {
            int index = rand.Next(imagepath.Count);
            Image img = Image.FromFile(imagepath[index]);
            images.Add(img);
        }
    }
    private Random rand = new Random();
    private List<string> imagepath = new List<string>();
    private List<Image> images = new List<Image>();
}