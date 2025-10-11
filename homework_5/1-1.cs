using Homework5;
using System;
using System.Drawing;
using System.Windows.Forms;
class Q1 : Form1
{
    public Q1()
    {
        InitializeLifetimeService();
    }
    public override void button1_Click(object sender, EventArgs e)
    {
        panel1.Paint -= paint_circle;
        panel1.Paint -= paint_ractangle;
        panel1.Paint += paint_circle;
        panel1.Invalidate();
    }
    public override void button2_Click(object sender, EventArgs e)
    {
        panel1.Paint -= paint_circle;
        panel1.Paint -= paint_ractangle;
        panel1.Paint += paint_ractangle;
        panel1.Invalidate();
    }
    private void paint_circle(object sender,PaintEventArgs e)
    {
        var g = e.Graphics;
        int R, G, B;
        for (int i = 0; i <= 9; i++)
        {
            R = ranDom.Next(0, 255);
            G = ranDom.Next(0, 255);
            B = ranDom.Next(0, 255);
            using (Pen pen = new Pen(Color.FromArgb(R, G, B), 3))
            {
                int[] position = { ranDom.Next(0, this.panel1.Width), ranDom.Next(0, this.panel1.Height) };
                int r = ranDom.Next(10, 100);
                g.DrawEllipse(pen, position[0], position[1], r, r);
            }
        }
        
    }
    private void paint_ractangle(object sender,PaintEventArgs e)
    {
        var g = e.Graphics;
        int R, G, B;
        for (int i = 0; i <= 9; i++)
        {
            R = ranDom.Next(0, 255);
            G = ranDom.Next(0, 255);
            B = ranDom.Next(0, 255);
            using (Pen pen = new Pen(Color.FromArgb(R, G, B), 3))
            {
                int[] position = { ranDom.Next(0, this.panel1.Width), ranDom.Next(0, this.panel1.Height) };
                int r1 = ranDom.Next(10, 100);
                int r2 = ranDom.Next(10, 100);
                g.DrawRectangle(pen, position[0], position[1], r1, r2);
            }
        }
    }
    private Random ranDom = new Random();
}