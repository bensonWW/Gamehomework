using Homework5;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
public class Q2 : Form2
{
    void checkrepeat(List<(int x, int y, int w, int h)> positions, int panelWidth, int panelHeight, Random random)
    {
        bool overlapExists = true;
        int maxTries = 2000;
        int tries = 0;

        while (overlapExists && tries++ < maxTries)
        {
            overlapExists = false;

            for (int i = 0; i < positions.Count; i++)
            {
                for (int j = 0; j < positions.Count; j++)
                {
                    if (i == j) continue;
                    bool overlap = !(positions[i].x + positions[i].w <= positions[j].x ||
                                     positions[j].x + positions[j].w <= positions[i].x ||
                                     positions[i].y + positions[i].h <= positions[j].y ||
                                     positions[j].y + positions[j].h <= positions[i].y);

                    if (overlap)
                    {
                        overlapExists = true;
                        var r = positions[i];
                        r.x = random.Next(0, Math.Max(1, panelWidth - r.w));
                        r.y = random.Next(0, Math.Max(1, panelHeight - r.h));
                        positions[i] = r;
                        j = -1;
                    }
                }
            }
        }
    }


    public Q2()
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
    override public void paint_circle(object sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        for (int i = 0; i <= 9; i++)
        {
            using (Pen pen = new Pen(Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255)), 3))
            {
                int R = random.Next(10, 100);
                int x = random.Next(0, panel1.Width - R);
                int y = random.Next(0, panel1.Height - R);
                g.DrawEllipse(pen, x, y, R, R);
            }
        }
    }
    override public void paint_ractangle(object sender, PaintEventArgs e)
    {
        List<(int x, int y, int w, int h)> positions = new List<(int x, int y, int w, int h)>();
        Graphics g = e.Graphics;
        for (int i = 0; i <= 9; i++)
        {
                int w = random.Next(10, 100);
                int h = random.Next(10, 100);
                int x = random.Next(0, panel1.Width - w);
                int y = random.Next(0, panel1.Height - h);
                positions.Add((x, y, w, h));
        }
        checkrepeat(positions, panel1.Width, panel1.Height, random);
        for (int i = 0; i < positions.Count; i++)
        {
            using (Pen pen = new Pen(Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255)), 3))
            {
                g.DrawRectangle(pen, positions[i].x, positions[i].y, positions[i].w, positions[i].h);
            }
        }
    }
}