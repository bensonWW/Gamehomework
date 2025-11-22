using Homework9;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
enum State
{
    Number,
    Question,
    Bomb
}
class Q3 : Form3
{
    public void createBomb()
    {
        for (int i = 0; i < 20; i++)
        {
            int random_i = random.Next(0, 16);
            int random_j = random.Next(0, 20);
            while (dots[random_i][random_j].getState() == State.Bomb)
            {
                random_i = random.Next(0, 16);
                random_j = random.Next(0, 20);
            }
            dots[random_i][random_j].changeState(State.Bomb);
        }
    }
    public void createDot()
    {
        for (int i = 0; i < 16; i++)
        {
            dots.Add(new List<Dot>());
            for (int j = 0; j < 20; j++)
            {
                Dot dot = new Dot();
                this.panel1.Controls.Add(dot);
                dots[i].Add(dot);
                dot.Size = new Size(50, 50);
                dot.Location = new Point(i * 75, j * 50);
                dot.changeState(State.Question);
            }
        }
    }
    public void createNumber()
    {
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                if (dots[i][j].getState() == State.Question)
                {
                    for (int z = i - 1; z <= i + 1; z++)
                    {
                        for (int a = j - 1; a <= j + 1; a++)
                        {
                            if (z >= 0 && z < 16 && a >= 0 && a < 20)
                            {
                                if (dots[z][a].getState() == State.Bomb)
                                {
                                    dots[i][j].setNumber(dots[i][j].getNumber() + 1);
                                    dots[i][j].changeState(State.Number);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    public Q3()
    {
        createDot();
        createBomb();
        createNumber();
    }
    private List<List<Dot>> dots = new List<List<Dot>>();
    private Random random = new Random();
}
class Dot : PictureBox
{
    public Dot()
    {
        state = State.Question;
    }
    public void changeState(State state)
    {
        this.state = state;
        switch (state)
        {
            case State.Number:
                this.Image = Image.FromFile($"bombgane/open{this.number}.png");
                break;
            case State.Question:
                this.Image = Image.FromFile("bombgane/question.png");
                break;
            case State.Bomb:
                this.Image = Image.FromFile("bombgane/mine-ceil.png");
                break;
        }
    }
    public State getState()
    {
        return state;
    }
    public int getNumber()
    {
        return number;
    }
    public void setNumber(int number)
    {
        this.number = number;
    }
    private State state;
    private int number;
}
