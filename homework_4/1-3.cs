using Homework4;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
internal class Q3 : Form3
{   public Q3()
    {
        InitializeLifetimeService();
    }
    override public void button1_Click(object sender, EventArgs e)
    {
        this.BackColor = Color.Black;
        this.Paint += new PaintEventHandler(Form3_Paint);
    }

    private void Q3_Paint(object sender, PaintEventArgs e)
    {
        throw new NotImplementedException();
    }

    public void Form3_Paint(object sender , PaintEventArgs e)
    {
        int Y = this.ClientSize.Height / 2;
        using (Pen pen = new Pen(Color.Blue, 3))
        {
            e.Graphics.DrawLine(pen, 0, Y, this.ClientSize.Width, Y);
        }
        using (Pen pen = new Pen(Color.Red, 3))
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            int w = this.ClientSize.Width;
            int h = this.ClientSize.Height;
            int midY = h / 2;

            var pts = new List<PointF>();
            for (int x = 0; x < w; x += 6) // 取樣密度(越小越平滑)
            {
                float y = (float)(midY - 180 * Math.Sin((x / 360.0) * 2 * Math.PI));
                pts.Add(new PointF(x, y));
            }
            e.Graphics.DrawCurve(pen, pts.ToArray());
        }
        using (Pen pen = new Pen(Color.Yellow, 3))
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            int w = this.ClientSize.Width;
            int h = this.ClientSize.Height;
            int midY = h / 2;

            var pts = new List<PointF>();
            for (int x = 0; x < w; x += 6) // 取樣密度(越小越平滑)
            {
                // 使用 cos 而不是 sin
                float y = (float)(midY - 180 * Math.Cos((x / 360.0) * 2 * Math.PI));
                pts.Add(new PointF(x, y));
            }

            e.Graphics.DrawCurve(pen, pts.ToArray());
        }
    }
}