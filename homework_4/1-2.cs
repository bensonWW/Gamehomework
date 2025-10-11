using Homework4;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
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
            tempLabel.Margin = new Padding(0);
            tempLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            tempLabel.Font = new System.Drawing.Font("²Ó©úÅé", 24F);
            tempLabel.ForeColor  = Color.Blue;
            tempLabel.Paint += new PaintEventHandler(Label_Paint);
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
        this.tableLayoutPanel1.Controls.Clear();
        for (int i = 0; i < randomNum.Count; i++)
        {
            int random = new Random().Next(i, randomNum.Count);
            Label temp = this.randomNum[i];
            randomNum[i] = randomNum[random];
            randomNum[random] = temp;
            this.tableLayoutPanel1.Controls.Add(randomNum[i], (i % 5), (i / 5));
        }
    }
    private void Label_Paint(object sender, PaintEventArgs e)
    {
        Label lbl = (Label)sender;
        using (Pen pen = new Pen(Color.Red, 2))
        {
            Rectangle rect = lbl.ClientRectangle;
            rect.Width -= 1;
            rect.Height -= 1;
            e.Graphics.DrawRectangle(pen, rect);
        }
    }
    private List<Label> randomNum = new List<Label>();
}