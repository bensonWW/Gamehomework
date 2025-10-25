using Homework6;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Emit;
class Q1 : Form1    
{
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
    public Q1()
    {

    }
    override public void button1_Click(object sender, EventArgs e)
    {
        createBumpList();
        for (int i = 0; i < 16; i++)
        {
            int randomColumn = ranDom.Next(column);
            int randomRow = ranDom.Next(row);
            while (bumbList[randomColumn][randomRow] == 9)
            {
                randomColumn = ranDom.Next(column);
                randomRow = ranDom.Next(row);
            }
            bumbList[randomColumn][randomRow] = 9;
            for (int j = randomRow - 1; j <= randomRow + 1; j++)
            {
                for (int z = randomColumn - 1; z <= randomColumn + 1; z++)
                {
                    if (j < 0 || j >= row || z < 0 || z >= column || (j == randomRow && z == randomColumn) || bumbList[z][j] == 9)
                        continue;
                    bumbList[z][j] += 1;
                }
            }
        }
        string printString = "";
        for (int i = 0;i < bumbList.Count; i++)
        {
            printString += string.Join("   ", bumbList[i]) + "\n";
        }
        label1.Text = printString;
    }
    private int column = 10;
    private int row = 10;
    private List<List<int>> bumbList = new List<List<int>>();
    private Random ranDom = new Random();
}