using Homework10;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Remoting.Channels;
enum state 
    {
        bomb,
        number,
        question
    }
public class Q1 : Form1
{
    internal Q1()
    {
        createDots();
    }
    public void change_NeardotsState(int index1,int index2)
    {
        for(int i = index1 - 1;i <= index1 + 1; i++)
        {
            for(int j = index2 - 1;j <= index2 + 1; j++)
            {
                if(i >= 0 && i < 10 && j >= 0 && j < 10)
                {
                    if (dots[i][j].getState() == state.bomb)
                    {
                        continue;
                    }
                    dots[i][j].setNum(dots[i][j].getNum() + 1);
                    dots[i][j].changeState(state.number);
                }
            }
        }
    }
    public void createDots()
    {
        dots = new List<List<Dot>>();
        for (int i = 0; i < 10; i++)
        {
            dots.Add(new List<Dot>());
            for (int j = 0; j < 10; j++)
            {
                Dot dot = new Dot();
                panel1.Controls.Add(dot);
                dot.Location = new Point(150 + 80 * i, 70 + 60 * j);
                dot.Size = new Size(50, 50);
                dot.Click += ClickDot;
                dots[i].Add(dot);
            }
        }
        panel1.Paint += paintLine;
    }
    public void paintLine(Object sender,PaintEventArgs e)
    {
        Pen pen = new Pen(Color.Red, 2);
        for(int i = 0;i < dots.Count; i++)
        {
            Point roworiginPoint = new Point(dots[i][0].Location.X - 5, dots[i][0].Location.Y - 5);
            Point coloriginPoint = new Point(dots[0][i].Location.X - 5, dots[0][i].Location.Y - 5);
            Point rownewLocate = new Point(
                    dots[i][0].Location.X - 5,
                    dots[i][dots[i].Count - 1].Location.Y + dots[i][dots[i].Count - 1].Size.Height + 5
                );
            Point colnewLocate = new Point(
                    dots[dots.Count - 1][i].Location.X + dots[dots.Count - 1][i].Size.Width + 20,
                    dots[0][i].Location.Y - 5
                );
            e.Graphics.DrawLine(pen, roworiginPoint, rownewLocate);
            e.Graphics.DrawLine(pen, coloriginPoint, colnewLocate);
            if(i == dots.Count - 1)
            {
                Point lastroworiginPoint = new Point(dots[i][0].Location.X + dots[i][0].Size.Width + 20, dots[i][0].Location.Y - 5);
                Point lastcoloriginPoint = new Point(dots[0][i].Location.X - 5, dots[0][i].Location.Y + dots[0][i].Size.Height + 5);
                Point lastrownewLocate = new Point(
                    dots[i][0].Location.X + dots[i][0].Size.Width + 20,
                    dots[i][dots[i].Count - 1].Location.Y + dots[i][dots[i].Count - 1].Size.Height + 5
                );
                Point lastcolnewLocate = new Point(
                    dots[dots.Count - 1][i].Location.X + dots[dots.Count - 1][i].Size.Width + 20,
                    dots[0][i].Location.Y + dots[0][i].Size.Height + 5
                );
                e.Graphics.DrawLine(pen, lastroworiginPoint, lastrownewLocate);
                e.Graphics.DrawLine(pen, lastcoloriginPoint, lastcolnewLocate);
            }
        }

    }


    public void ClickDot(Object sender, EventArgs e)
    {
        Dot dot = sender as Dot;
        if(dot.getState() == state.bomb)
        {
            return;
        }
        dot.changeState(state.bomb);
        for(int i = 0;i < dots.Count; i++)
        {
            for(int j = 0;j < dots[i].Count; j++)
            {
                if(dots[i][j] == dot)
                {
                    change_NeardotsState(i, j);
                }
            }
        }
    }
    public override void button1_Click(object sender, EventArgs e)
    {
        for(int i = 0;i< dots.Count; i++)
        {
            for(int j = 0;j < dots[i].Count; j++)
            {
                panel1.Controls.Remove(dots[i][j]);
            }
        }
        createDots();
    }
    private List<List<Dot>> dots = new List<List<Dot>>();
}

internal class Dot : PictureBox
{
    internal Dot()
    {
        st = state.question;
        this.Image = Image.FromFile("question.png");
    }
    public int getNum()
    {
        return num;
    }
    public void setNum(int num)
    {
        this.num = num;
    }
    public state getState()
    {
        return st;
    }
    public void changeState(state state)
    {
        this.st = state;
        switch (state)
        {
            case state.bomb:
                this.Image = Image.FromFile("mine.png");
                break;
            case state.number:
                this.Image = Image.FromFile($"open{num}.png");
                break;
            case state.question:
                this.Image = Image.FromFile("question.png");
                break;
        }
    }
    private int num;
    private state st;

}