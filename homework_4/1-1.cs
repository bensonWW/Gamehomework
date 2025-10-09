using Homework4;
using System;
using System.Drawing;
using System.Windows.Forms;
internal class Q1 : Form1
{
    internal Q1()
    {
        InitializeComponent();
    }
    override public void button1_Click(object sender, EventArgs e)
    {
        if (this.buttonpushNum == 0)
        {
            label1.Text = "九九乘法表:\n";
            for (int i = 1; i <= 9; i++)
            {
                for (int j = 1; j <= 9; j++)
                {
                    label1.Text += $"{i} x {j} = {i * j,-2}  ";
                }
                label1.Text += "\n";
            }
            this.buttonpushNum += 1;
        }
        else
        {
            Random randomNum = new Random();
            label1.ForeColor = Color.FromArgb(randomNum.Next(0, 256), randomNum.Next(0, 256),randomNum.Next(0, 256));
        }
    }
    private int buttonpushNum = 0;
}