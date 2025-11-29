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
        question,
        space
}
public class Q1 : Form1
{
    internal Q1()
    {
        createDots();
    }
    public void createDots()
    {
        for (int i = 0; i < dots.Count; i++)
        {
            for (int j = 0; j < dots[i].Count; j++)
            {
                panel1.Controls.Remove(dots[i][j]);
            }
        }
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
        for(int i = 0;i < 10; i++)
        {
            int randomNum1 = random.Next(0, 10);
            int randomNum2 = random.Next(0, 10);
            while (dots[randomNum1][randomNum2].getState() == state.bomb)
            {
                randomNum1 = random.Next(0, 10);
                randomNum2 = random.Next(0, 10);
            }
            dots[randomNum1][randomNum2].changeState(state.bomb);
            for(int a = randomNum1 - 1;a <= randomNum1 + 1; a++)
            {
                for(int j = randomNum2 - 1;j <= randomNum2 + 1; j++)
                {
                    if(a >= 0 && a < 10 && j >= 0 && j < 10)
                    {
                        if (dots[a][j].getState() == state.bomb)
                        {
                            dots[a][j].Image = Image.FromFile("question.png");
                            continue;
                        }
                        dots[a][j].setNum(dots[a][j].getNum() + 1);
                        dots[a][j].changeState(state.number);
                        dots[a][j].Image = Image.FromFile("question.png");
                    }
                }
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

    public void trunDot(int index1,int index2)
    {
        if (index1 < 0 || index2 < 0 || index1 >= 10 || index2 >= 10)
        {
            return;
        }
        else if(dots[index1][index2].getState() == state.bomb)
        {
            dots[index1][index2].changeState(state.bomb);
            MessageBox.Show("You clicked on a bomb! Game Over.");
            createDots();
        }
        else if(dots[index1][index2].getState() == state.space)
        {
            return;
        }
        else if (dots[index1][index2].getNum() > 0)
        {
            dots[index1][index2].changeState(state.number);
            return;
        }
        else if (dots[index1][index2].getNum() == 0)
        {
            dots[index1][index2].changeState(state.space);
            for (int i = index1 - 1; i <= index1 + 1; i++)
            {
                for (int j = index2 - 1; j <= index2 + 1; j++)
                {
                    if (index1 == i && index2 == j)
                    {
                        continue;
                    }
                    trunDot(i, j);
                }
            }
            return;
        }
    }
    public void ClickDot(Object sender, EventArgs e)
    {
        int index1 = 0, index2 = 0;
        for(int i = 0;i < dots.Count; i++)
        {
            for(int j = 0;j < dots[i].Count; j++)
            {
                if(dots[i][j] == sender)
                {
                    index1 = i;
                    index2 = j;
                }
            }
        }
        trunDot(index1, index2);
    }
    public override void button1_Click(object sender, EventArgs e)
    {
        createDots();
    }
    private List<List<Dot>> dots = new List<List<Dot>>();
    private static Random random = new Random();
}

internal class Dot : PictureBox
{
    internal Dot()
    {
        num = 0;
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
            case state.space:
                this.Image = Image.FromFile("space.png");
                break;
        }
    }
    private int num;
    private state st;

}