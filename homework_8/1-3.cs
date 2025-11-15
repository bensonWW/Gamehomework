using Homework8;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

class Q3 : Form3
{
    // 拖曳用狀態
    private bool _isDragging = false;
    private Point _mouseDownPos;              // 滑鼠按下時相對於拼圖塊的座標
    private PictureBox _draggingPiece = null; // 目前正在被拖曳的那一塊

    // 資料
    private List<Bitmap> bitMaps = new List<Bitmap>();         // 被切好的圖片
    private List<PictureBox> pieceBoxes = new List<PictureBox>(); // 右邊 panel2 的拼圖塊
    private List<PictureBox> slotBoxes = new List<PictureBox>();  // 左邊 panel1 的格子
    private int column;
    private int row;

    public Q3()
    {
        // Form3 的 InitializeComponent() 會先跑完
        // 我們只要訂一次 Paint 事件就好
        panel1.Paint += paintLine;
    }

    //=======================
    //  按鈕：3x3 / 4x5
    //=======================
    public override void button1_Click(object sender, EventArgs e)
    {
        SetupPuzzle(3, 3);
    }

    public override void button2_Click(object sender, EventArgs e)
    {
        SetupPuzzle(5, 4);   // 4x5：4 row, 5 column（跟你原本一樣 row=4, column=5）
    }

    private void SetupPuzzle(int col, int r)
    {
        // 清空舊的
        panel1.Controls.Clear();
        panel2.Controls.Clear();
        bitMaps.Clear();
        pieceBoxes.Clear();
        slotBoxes.Clear();
        _draggingPiece = null;
        _isDragging = false;

        column = col;
        row = r;

        // 切圖 → 建拼圖塊 + 建格子
        cutPicture(column, row);
        createPieces(column, row);
        createSlots(column, row);

        panel1.Invalidate();   // 重新畫格線
    }

    //=======================
    //  切圖
    //=======================
    void cutPicture(int column, int row)
    {
        Bitmap source = new Bitmap("dolamo.png");
        int pieceW = source.Width / column;
        int pieceH = source.Height / row;

        for (int r = 0; r < row; r++)
        {
            for (int c = 0; c < column; c++)
            {
                Rectangle rect = new Rectangle(
                    c * pieceW,
                    r * pieceH,
                    pieceW,
                    pieceH
                );
                Bitmap piece = source.Clone(rect, source.PixelFormat);
                bitMaps.Add(piece);
            }
        }
    }

    //=======================
    //  建立右邊 panel2 的拼圖塊
    //=======================
    void createPieces(int column, int row)
    {
        int w = panel2.ClientSize.Width / column;
        int h = panel2.ClientSize.Height / row;

        for (int j = 0; j < row; j++)
        {
            for (int i = 0; i < column; i++)
            {
                PictureBox pb = new PictureBox();
                pb.Width = w;
                pb.Height = h;
                pb.Location = new Point(i * w, j * h);
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.Image = bitMaps[j * column + i];

                // 只給拼圖塊掛拖曳事件
                pb.MouseDown += Piece_MouseDown;
                pb.MouseMove += Piece_MouseMove;
                pb.MouseUp += Piece_MouseUp;

                panel2.Controls.Add(pb);
                pieceBoxes.Add(pb);
            }
        }
    }

    //=======================
    //  建立左邊 panel1 的格子（空白框）
    //=======================
    void createSlots(int column, int row)
    {
        int w = panel1.ClientSize.Width / column;
        int h = panel1.ClientSize.Height / row;

        for (int j = 0; j < row; j++)
        {
            for (int i = 0; i < column; i++)
            {
                PictureBox slot = new PictureBox();
                slot.Width = w;
                slot.Height = h;
                slot.Location = new Point(i * w, j * h);
                slot.SizeMode = PictureBoxSizeMode.StretchImage;
                slot.BackColor = Color.Transparent;

                panel1.Controls.Add(slot);
                slotBoxes.Add(slot);
            }
        }
    }

    //=======================
    //  panel1 畫格線
    //=======================
    private void paintLine(object sender, PaintEventArgs e)
    {
        if (row <= 0 || column <= 0) return;

        int height = panel1.ClientSize.Height / row;
        int width = panel1.ClientSize.Width / column;

        using (Pen pen = new Pen(Color.Red, 3))
        {
            for (int i = 0; i <= column; i++)
            {
                e.Graphics.DrawLine(pen, i * width, 0, i * width, panel1.ClientSize.Height);
            }
            for (int i = 0; i <= row; i++)
            {
                e.Graphics.DrawLine(pen, 0, i * height, panel1.ClientSize.Width, i * height);
            }
        }
    }

    //=======================
    //  拖曳事件：MouseDown
    //=======================
    private void Piece_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left) return;

        _isDragging = true;
        _mouseDownPos = e.Location;
        _draggingPiece = sender as PictureBox;

        if (_draggingPiece == null) return;

        // 把拼圖塊搬到整個 Form 上，才可以拖出 panel2
        if (_draggingPiece.Parent != this)
        {
            Point screenPos = _draggingPiece.Parent.PointToScreen(_draggingPiece.Location); // parent 座標 → 螢幕
            _draggingPiece.Parent.Controls.Remove(_draggingPiece);
            this.Controls.Add(_draggingPiece);
            _draggingPiece.Location = this.PointToClient(screenPos); // 螢幕 → Form 座標
        }

        _draggingPiece.BringToFront(); // 提到最上面的 ZIndex
    }

    //=======================
    //  拖曳事件：MouseMove
    //=======================
    private void Piece_MouseMove(object sender, MouseEventArgs e)
    {
        if (!_isDragging || _draggingPiece == null) return;

        // 直接用 Form 當基準
        Point mousePos = this.PointToClient(Cursor.Position);
        _draggingPiece.Left = mousePos.X - _mouseDownPos.X;
        _draggingPiece.Top = mousePos.Y - _mouseDownPos.Y;
    }

    //=======================
    //  拖曳事件：MouseUp（放下時決定要不要放進格子）
    //=======================
    private void Piece_MouseUp(object sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left) return;

        _isDragging = false;
        if (_draggingPiece == null) return;

        // 滑鼠位置（Form 座標）
        Point mousePos = this.PointToClient(Cursor.Position);

        // 先算出 panel1 在 Form 座標的矩形
        Rectangle boardRect = new Rectangle(
            this.PointToClient(panel1.PointToScreen(Point.Empty)), // 左上角
            panel1.ClientSize                                      // 大小
        );

        // 如果滑鼠沒在 panel1 範圍內，就什麼都不做
        if (!boardRect.Contains(mousePos))
        {
            _draggingPiece = null;
            return;
        }

        // 在所有格子裡找有沒有包含這個點
        foreach (var slot in slotBoxes)
        {
            // slot 是 panel1 的小格子，它的位置是「相對於 panel1」
            // 先 panel1 座標 → 螢幕 → Form 座標
            Point slotPosOnForm = this.PointToClient(slot.Parent.PointToScreen(slot.Location));
            Rectangle slotRect = new Rectangle(slotPosOnForm, slot.Size);

            if (slotRect.Contains(mousePos))
            {
                // 把拼圖塊的圖貼到格子中
                slot.Image = _draggingPiece.Image;
                _draggingPiece.Image = null;      // 手上的那一塊變空
                _draggingPiece.Visible = false;   // 直接隱藏掉（也可以 this.Controls.Remove）
                break;
            }
        }

        _draggingPiece = null;
    }
}
