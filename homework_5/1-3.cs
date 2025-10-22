using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Homework5;

class Q3 : Form3
{
    private void LayoutCardsCross()
    {
        // �P���ؤo�P���Z�]�A�i�H�վ�^
        int cardW = 150, cardH = 218;
        int gapX = 60;   // ���k�P�����������Z��
        int gapY = 30;   // �W/�U�C�P�����������Z��
        int topOffset = 90; // ��թ��U���@�I�A�׶}�W����s

        // �H���Ȥ�Ϥ��߬����
        int cx = this.ClientSize.Width / 2;
        int cy = this.ClientSize.Height / 2 + topOffset;

        // ����
        pictureBox3.SetBounds(cx - cardW / 2, cy - cardH / 2, cardW, cardH);

        // �W�ƥ��k
        pictureBox1.SetBounds(cx - gapX - cardW, cy - (cardH + gapY) - cardH / 2, cardW, cardH);
        pictureBox2.SetBounds(cx + gapX, cy - (cardH + gapY) - cardH / 2, cardW, cardH);

        // �U�ƥ��k
        pictureBox4.SetBounds(cx - gapX - cardW, cy + (cardH + gapY) - cardH / 2, cardW, cardH);
        pictureBox5.SetBounds(cx + gapX, cy + (cardH + gapY) - cardH / 2, cardW, cardH);

        // �T�O�i���P���Q Anchor �ԧ�
        foreach (var pb in new[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5 })
        {
            pb.Visible = true;
            pb.BringToFront();
            pb.Anchor = AnchorStyles.Top;                // �T�w��W��A�קK������j�p�ɨ���
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
    public Q3()
    {
        InitializeComponent();
        for (int i = 0; i <= 51; i++)
        {
            imagepath.Add(@"pokerimage\poker" + i.ToString() + ".png");
        }
        LayoutCardsCross();
    }

    override public void button1_Click(object sender, EventArgs e)
    {
        foreach (var img in images) img.Dispose();
        images.Clear();

        for (int i = 0; i <= 4; i++)
        {
            int index = rand.Next(imagepath.Count);
            using (var fs = new FileStream(imagepath[index], FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var ms = new MemoryStream())
            {
                fs.CopyTo(ms);
                images.Add(Image.FromStream(ms));
            }
        }

        if (images.Count >= 5)
        {
            pictureBox1.Image = images[0];
            pictureBox2.Image = images[1];
            pictureBox3.Image = images[2];
            pictureBox4.Image = images[3];
            pictureBox5.Image = images[4];
        }

        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
        pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
        pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
    }

    private Random rand = new Random();
    private List<string> imagepath = new List<string>();
    private List<Image> images = new List<Image>();
}