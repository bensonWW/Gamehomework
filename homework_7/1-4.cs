using Homework7;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

internal class Q4 : Form4
{
    void initialPokernum()
    {
        List<int> temp = new List<int>();
        for (int i = 0; i < 52; i++)
        {
            temp.Add(i);
        }
        for (int i = 0; i < column * row / 2; i++)
        {
            int randomNum = random.Next(temp.Count);
            pokerNum.Add(temp[randomNum]);
            temp.RemoveAt(randomNum);
        }
    }
    void randomList()
    {
        for (int i = 0; i < pokerNum.Count; i++)
        {
            int randomNum = random.Next(i, pokerNum.Count);
            int temp = pokerNum[randomNum];
            pokerNum[randomNum] = pokerNum[i];
            pokerNum[i] = temp;
        }
    }
    void createImage()
    {
        pictureBoxes.Clear();
        panel1.Controls.Clear();
        pokerNum.Clear();
        initialPokernum();
        randomList();
        pokerNum.AddRange(pokerNum);
        int width = panel1.Size.Width;
        int height = panel1.Size.Height;
        for (int i = 0; i < column; i++)
        {
            for (int j = 0; j < row; j++)
            {
                int randomNum = random.Next(pokerNum.Count);
                PictureBox pictureBox = new PictureBox();
                panel1.Controls.Add(pictureBox);
                pictureBox.Location = new System.Drawing.Point(i * (width / column), j * (height / row));
                pictureBox.Size = new System.Drawing.Size(width / column, height / row);
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.ImageLocation = "pokers/PokerBack-3.png";
                pictureBox.Tag = "pokers/" + "poker" + pokerNum[randomNum].ToString() + ".png";
                pictureBox.Visible = true;
                pictureBox.MouseClick += pictureboxClick;
                pictureBoxes.Add(pictureBox);
                pokerNum.RemoveAt(randomNum);
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
        if((column + 2) * row  < 52)
        {
            column += 2;
            createImage();
            changeState();
        }

    }
    public override void button2_Click(object sender, EventArgs e)
    {
        if (column - 2 > 0)
        {
            column -= 2;
            createImage();
            changeState();
        }
    }
    public override void button3_Click(object sender, EventArgs e)
    {
        if (column * (row + 2) < 52)
        {
            row += 2;
            createImage();
            changeState();
        }
        

    }
    public override void button4_Click(object sender, EventArgs e)
    {
        if (row - 2 > 0)
        {
            row -= 2;
            createImage();
            changeState();
        }
    }
    public async void pictureboxClick(Object sender,MouseEventArgs e)
    { 
        if (isWaiting)
        {
            return;
        }
        PictureBox pictureBox = sender as PictureBox;
        if (pictureBox.ImageLocation == "pokers/PokerBack-3.png")
        {
            pictureBox.ImageLocation = pictureBox.Tag.ToString();
        }
        else
        {
            return;
        }

        if (prepictureBox == null)
        {
            prepictureBox = pictureBox;
        }
        else
        {
            if(prepictureBox.Tag.ToString() == pictureBox.Tag.ToString() && prepictureBox.ImageLocation != "pokers/PokerBack-3.png")
            {
                score += 10;
            }
            else
            {
                isWaiting = true;
                await Task.Delay(500);
                pictureBox.ImageLocation = "pokers/PokerBack-3.png";
                prepictureBox.ImageLocation = "pokers/PokerBack-3.png";
                isWaiting = false;
            }
                prepictureBox = null;
        }
        label3.Text = "¤À¼Æ : " + score.ToString();
    }
    public Q4()
    {
        createImage();
    }
    private List<int> pokerNum = new List<int>();
    private List<PictureBox> pictureBoxes = new List<PictureBox>();
    private int column = 2;
    private int row = 2;
    private PictureBox prepictureBox = null;
    private bool isWaiting = false;
    private Random random = new Random();
    private int score = 0;
}