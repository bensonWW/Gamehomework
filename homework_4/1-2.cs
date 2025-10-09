using Homework4;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
internal class Q2 : Form2
{
    internal Q2()
    {
        InitializeLifetimeService();
        for(int i = 1;i <= 25; i++)
        {
            Label tempLabel = new Label();
            tempLabel.Text = i.ToString();
            tempLabel.Dock = DockStyle.Fill;
            tempLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            tempLabel.Font = new System.Drawing.Font("²Ó©úÅé", 24F);
            randomNum.Add(tempLabel);


        }
        for(int i = 0; i < randomNum.Count; i++)
        {
            int random = new Random().Next(i,randomNum.Count);
            Label temp = this.randomNum[i];
            randomNum[i] = randomNum[random];
            randomNum[random] = temp;
            this.tableLayoutPanel1.Controls.Add(randomNum[i], (i % 5) ,(i / 5) );
        }
    }
    override protected void button1_Click(object sender, EventArgs e)
    {
        
    }
    private List<Label> randomNum = new List<Label>();
}