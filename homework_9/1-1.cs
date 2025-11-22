using Homework9;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
public class Q1 : Form1
{
    public Q1()
    {
        for (int i = 0; i < 51; i++)
        {
            pokers.Add(new Poker(this.panel1, i));
        }
        timer1.Interval = 1;
        timer1.Tick += timer1_Tick;
    }
    public class Poker : PictureBox
    {
        public Poker(Panel panel, int num)
        {
            panel.Controls.Add(this);
            this.Image = Image.FromFile($"pokers/poker{num}.png");
            this.Tag = Image.FromFile($"pokers/poker{num}.png");
            this.angle = (float)(random.NextDouble() * 2 * Math.PI);
            this.Size = new Size(70, 100);
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Location = new Point(random.Next(0, panel.Size.Width - this.Size.Width), random.Next(0, panel.Size.Height - this.Size.Height));
            this.position_x = this.Location.X;
            this.position_y = this.Location.Y;
        }
        ~Poker() { this.Dispose(); }
        public bool GetIsUp()
        {
            return isUp;
        }
        public void turnCard()
        {
            if (isUp)
            {
                this.Image = Image.FromFile("pokers/pokerBack-3.png");
            }
            else
            {
                this.Image = this.Tag as Image;
            }
            isUp = !isUp;
        }
        public void move()
        {
            this.position_x += (float)(Math.Cos(angle) * 10);
            this.position_y += (float)(Math.Sin(angle) * 10);
            Point newPoint = new Point((int)position_x, (int)position_y);
            this.Location = newPoint;
            if (this.Location.X + this.Size.Width > this.Parent.Size.Width)
            {
                angle = (float)Math.PI - angle;
                turnCard();
            }
            else if (this.Location.X < 0)
            {
                angle = (float)Math.PI - angle;
                turnCard();
            }
            if (this.Location.Y + this.Size.Height > this.Parent.Size.Height)
            {
                angle = (float)Math.PI * 2 - angle;
                turnCard();
            }
            else if (this.Location.Y < 0)
            {
                angle = (float)Math.PI * 2 - angle;
                turnCard();
            }
        }
        private float angle;
        private float position_x;
        private float position_y;
        private bool isUp = true;
        private static readonly Random random = new Random();
    }
    override public void timer1_Tick(object sender, EventArgs e)
    {
        foreach (Poker p in pokers)
        {
            p.move();
        }
        this.panel1.Refresh();
    }
    public override void button1_Click(object sender, EventArgs e)
    {
        timer1.Enabled = !timer1.Enabled;
    }
    protected List<Poker> pokers = new List<Poker>();
}

