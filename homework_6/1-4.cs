using Homework6;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
class Q4 : Form3
{
    bool inRange(int x,int y,int r)
    {
        if (x + r >= panel1.Size.Width || x <= 0 || y + r >=  panel1.Size.Height || y <= 0)
        {
            return false;
        }
        return true;
    }
    bool left_right_Collision(int x, int y, int r)
    {
        if(x + r >= panel1.Size.Width || x <= 0)
        {
            return true;
        }
        return false;
    }
    bool up_down_Collision(int x, int y, int r)
    {
        if(y + r >= panel1.Size.Height || y <= 0)
        {
            return true;
        }
        return false;
    }
    public Q4()
    {
        int panelSize_h = panel1.Size.Height;
        int panelSize_w = panel1.Size.Width;
        (int panelPosition_x, int panelPosition_y) panelPosition = (panel1.Location.X, panel1.Location.Y);
        for (int i = 0;i < 7; i++)
        {
            int objectPosition_x = ranDom.Next(panelPosition.panelPosition_x, panelPosition.panelPosition_x + panelSize_w);
            int objectPosition_y = ranDom.Next(panelPosition.panelPosition_y, panelPosition.panelPosition_y + panelSize_h);
            int r = ranDom.Next(100, 200);
            float angle = ranDom.Next(361);
            while (!inRange(objectPosition_x, objectPosition_y, r))
            {
                objectPosition_x = ranDom.Next(panelPosition.panelPosition_x, panelPosition.panelPosition_x + panelSize_w);
                objectPosition_y = ranDom.Next(panelPosition.panelPosition_y, panelPosition.panelPosition_y + panelSize_h);
                r = ranDom.Next(100, 200);
            }
            int R = ranDom.Next(0, 255);
            int G = ranDom.Next(0, 255);
            int B = ranDom.Next(0, 255);
            Pen pen = new Pen(Color.FromArgb(R, G, B),2);
            circleInfo.Add((objectPosition_x, objectPosition_y,r,angle,pen));       
        }
        for(int i = 0; i < 7; i++)
        {
            int objectPosition_x = ranDom.Next(panelPosition.panelPosition_x, panelPosition.panelPosition_x + panelSize_w);
            int objectPosition_y = ranDom.Next(panelPosition.panelPosition_y, panelPosition.panelPosition_y + panelSize_h);
            int r1 = ranDom.Next(100, 200);
            int r2 = ranDom.Next(100, 200);
            float angle = ranDom.Next(361);
            while (up_down_Collision(objectPosition_x, objectPosition_y, r1) || left_right_Collision(objectPosition_x, objectPosition_y, r2))
            {
                objectPosition_x = ranDom.Next(panelPosition.panelPosition_x, panelPosition.panelPosition_x + panelSize_w);
                objectPosition_y = ranDom.Next(panelPosition.panelPosition_y, panelPosition.panelPosition_y + panelSize_h);
                r1 = ranDom.Next(100, 200);
                r2 = ranDom.Next(100, 200);
            }
            int R = ranDom.Next(0, 255);
            int G = ranDom.Next(0, 255);
            int B = ranDom.Next(0, 255);
            Pen pen = new Pen(Color.FromArgb(R, G, B), 2);
            rectangleInfo.Add((objectPosition_x, objectPosition_y, r1,r2, angle, pen));
        }
    }
    public override void button1_Click(object sender, EventArgs e)
    {
        timer1.Enabled = !timer1.Enabled;
        panel1.Paint -= panelPaint;
        panel1.Paint += panelPaint;
    }
    override public void timer1_Tick(object sender, EventArgs e) 
    {
        panel1.Invalidate();
        for (int i = 0; i < circleInfo.Count; i++)
        {
            int x = circleInfo[i].x + (int)(10 * Math.Cos(circleInfo[i].angle * Math.PI / 180));
            int y = circleInfo[i].y + (int)(10 * Math.Sin(circleInfo[i].angle * Math.PI / 180));
            circleInfo[i] = (x, y, circleInfo[i].r, circleInfo[i].angle, circleInfo[i].pen);
            if (!inRange(circleInfo[i].x, circleInfo[i].y, circleInfo[i].r))
            {
                float angle = circleInfo[i].angle;
                if (up_down_Collision(circleInfo[i].x, circleInfo[i].y, circleInfo[i].r))
                {
                    angle = -angle;
                }
                else if (left_right_Collision(circleInfo[i].x, circleInfo[i].y, circleInfo[i].r))
                {
                    angle = 180 - angle;
                }

                circleInfo[i] = (circleInfo[i].x, circleInfo[i].y, circleInfo[i].r, angle, circleInfo[i].pen);
            }
        }
        for (int i = 0; i < rectangleInfo.Count; i++)
        {
            int x = rectangleInfo[i].x + (int)(10 * Math.Cos(rectangleInfo[i].angle * Math.PI / 180));
            int y = rectangleInfo[i].y + (int)(10 * Math.Sin(rectangleInfo[i].angle * Math.PI / 180));
            rectangleInfo[i] = (x, y, rectangleInfo[i].r1, rectangleInfo[i].r2, rectangleInfo[i].angle, rectangleInfo[i].pen);
            float angle = rectangleInfo[i].angle;
            if (up_down_Collision(rectangleInfo[i].x, rectangleInfo[i].y, rectangleInfo[i].r1))
            {
                angle = -angle;
            }
            else if (left_right_Collision(rectangleInfo[i].x, rectangleInfo[i].y, rectangleInfo[i].r2))
            {
                angle = 180 - angle;
            }

            rectangleInfo[i] = (rectangleInfo[i].x, rectangleInfo[i].y, rectangleInfo[i].r1, rectangleInfo[i].r2, angle, rectangleInfo[i].pen);
        }

    }
    public void panelPaint(object sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        for (int i = 0; i < circleInfo.Count; i++) 
        {
            g.DrawEllipse(circleInfo[i].pen, circleInfo[i].x, circleInfo[i].y, circleInfo[i].r, circleInfo[i].r);
        }
        for(int i = 0;i < rectangleInfo.Count; i++)
        {
            g.DrawRectangle(rectangleInfo[i].pen, rectangleInfo[i].x, rectangleInfo[i].y, rectangleInfo[i].r1, rectangleInfo[i].r2);
        }
    }
    private List<(int x, int y,int r,float angle,Pen pen)> circleInfo = new List<(int x, int y,int r,float angle,Pen pen)>();
    private List<(int x, int y, int r1,int r2, float angle, Pen pen)> rectangleInfo = new List<(int x, int y, int r1,int r2, float angle, Pen pen)>();
    private Random ranDom = new Random();
}