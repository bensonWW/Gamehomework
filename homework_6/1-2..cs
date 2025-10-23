using Homework6;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
internal class Q2 : Form2
{
    Label createLabel(int x,int y)
    {
        Label lbl = new Label();
        lbl.Text = "";
        lbl.Font = new Font("Consolas", 16f, FontStyle.Bold);
        lbl.ForeColor = Color.Blue;
        lbl.AutoSize = true;
        lbl.Location = new Point(x,y);
        lbl.BackColor = Color.Transparent;
        panel1.Controls.Add(lbl);
        return lbl;
    }
    void createBumpList()
    {
        bumbList.Clear();
        for (int i = 0; i < column; i++)
        {
            bumbList.Add(new List<int>());
            for (int j = 0; j < row; j++)
            {
                bumbList[i].Add(0);
            }
        }
    }
    public Q2()
    {
        createBumpList();
        for (int w = panel1.Size.Width / column, h = panel1.Size.Height / row, i = 0; i < bumbList.Count; i++)
        {
            labels.Add(new List<Label>());
            for (int j = 0; j < bumbList[i].Count; j++)
            {
                labels[i].Add(createLabel(w * i, h * j));
            }
        }
        panel1.Paint += paintPanel;
        panel1.Invalidate();

    }
    override public void button1_Click(object sender, EventArgs e) 
    {
        createBumpList();
        for (int i = 0;i < 16; i++)
        {
            int randomColumn = ranDom.Next(column);
            int randomRow = ranDom.Next(row);
            while (bumbList[randomColumn][randomRow] == 9)
            {
                randomColumn = ranDom.Next(column);
                randomRow = ranDom.Next(row);
            }
            bumbList[randomColumn][randomRow] = 9;
            for (int j = randomRow - 1;j <= randomRow + 1; j++)
            {
                for(int z = randomColumn - 1;z <= randomColumn + 1; z++)
                {
                    if (j < 0 || j >= row || z < 0 || z >= column || (j == randomRow && z == randomColumn) || bumbList[z][j] == 9)
                        continue;
                    bumbList[z][j] += 1;
                }
            }
            for(int j = 0;j < column; j++)
            {
                for(int z = 0;z < row; z++)
                {
                   labels[j][z].Text = bumbList[j][z].ToString();
                }
            }
        }
        
    }
    private void paintPanel(object sender,PaintEventArgs e)
    {
        int wei = panel1.Size.Width;
        int hei = panel1.Size.Height;
        int w = panel1.Size.Width / column;
        int h = panel1.Size.Height / row;
        for (int i = 1;i <   column; i++)
        {
            e.Graphics.DrawLine(Pens.Red, w * i, 0,w * i,hei);
        }
        for(int i = 1;i < row; i++)
        {
            e.Graphics.DrawLine(Pens.Red, 0, h * i, wei, h * i);
        }
    }
    private int column = 10;
    private int row = 10;
    private List<List<int>> bumbList = new List<List<int>>();
    private List<List<Label>> labels = new List<List<Label>>();
    private Random ranDom= new Random();

}