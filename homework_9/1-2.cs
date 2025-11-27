using Homework9;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
public class Q2 : Form2
{
    public void createPoker(int num)
    {
        Poker poker = new Poker();
        panel1.Controls.Add(poker);
        poker.Tag = $"pokers/poker{num}.png";
        poker.Image = Image.FromFile($"pokers/poker{num}.png");
        poker.SetPosition(random.Next(0, poker.Parent.Size.Width - poker.Size.Width),
                          random.Next(0, poker.Parent.Size.Height - poker.Size.Height));
        poker.Click += new EventHandler(clickPoker);
        pokers.Add(poker);
    }
    internal Q2()
    {
        for(int i = 0; i < 52; i++)
        {
            createPoker(i);
            createPoker(i);
        }
    }
    public override void button1_Click(object sender, EventArgs e)
    {
        timer1.Enabled = !timer1.Enabled;
    }
    public override void timer1_Tick(object sender, EventArgs e)
    {
        foreach(Poker poker in pokers)
        {
            poker.move();
        }
        panel1.Invalidate();
    }
    public void clickPoker(object sender, EventArgs e)
    {
       if(prePoker == null)
       {
            prePoker = sender as Poker;
            prePoker_pictureBox.Image = Image.FromFile(prePoker.Tag as string);
        }
       else
       {
            if(prePoker != sender as Poker)
            {
                if(prePoker.Tag as String == (sender as Poker).Tag as String)
                {
                    panel1.Controls.Remove(prePoker);
                    panel1.Controls.Remove(sender as Poker);
                    pokers.Remove(prePoker);
                    pokers.Remove(sender as Poker);
                }
                prePoker_pictureBox.Image = null;
                prePoker = null;
            }
        }
    }
    private List<Poker> pokers = new List<Poker>();
    private Poker prePoker;
    private static Random random = new Random();
}
internal class Poker : PictureBox
{
    public Poker()
    {
        this.Size = new Size(70, 100);
        this.SizeMode = PictureBoxSizeMode.StretchImage;
        angle = (float)(random.NextDouble() * 2 * Math.PI);
    }
    public Point GetPosition()
    {
        return this.Location;
    }
    public void SetPosition(int x, int y)
    {
        this.position_x = x;
        this.position_y = y;
        this.Location = new Point(x, y);
    }
    public void move()
    {
        this.position_x += (int)(Math.Cos(angle) * 20);
        this.position_y += (int)(Math.Sin(angle) * 20);
        Point newPoint = new Point((int)position_x, (int)position_y);
        this.Location = newPoint;
        if (this.Location.X + this.Size.Width > this.Parent.Size.Width)
        {
            angle = (float)Math.PI - angle;
            isUp = !isUp;
        }
        else if (this.Location.X < 0)
        {
            angle = (float)Math.PI - angle;
            isUp = !isUp;
        }
        if (this.Location.Y + this.Size.Height > this.Parent.Size.Height)
        {
            angle = -angle;
            isUp = !isUp;
        }
        else if (this.Location.Y < 0)
        {
            angle = -angle;
            isUp = !isUp;
        }
        if(isUp)
        {
            this.Image = Image.FromFile(this.Tag as String);
        }
        else
        {
            this.Image = Image.FromFile("pokers/pokerBack-3.png");
        }
    }
    private float angle;
    private int position_x;
    private int position_y;
    private bool isUp = true;
    private Random random = new Random();
}

