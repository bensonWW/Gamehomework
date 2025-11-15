using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Homework8
{
    public partial class Q4 : Form4
    {
        // --- 遊戲狀態欄位 ---
        private int _rows = 0;
        private int _cols = 0;

        // 所有拼圖 PictureBox
        private readonly List<PictureBox> _pieces = new List<PictureBox>();

        // 每一塊拼圖的「正確格子位置」：Point.X = col, Point.Y = row
        private readonly Dictionary<PictureBox, Point> _correctCell = new Dictionary<PictureBox, Point>();

        // 每一塊拼圖目前所在的格子（如果不在格子上就是 null）
        private readonly Dictionary<PictureBox, Point?> _currentCell = new Dictionary<PictureBox, Point?>();

        // 每一個格子目前是哪一塊拼圖佔據
        private PictureBox[,] _cellOccupant = null;

        // 拖曳相關
        private bool _isDragging = false;
        private Point _mouseOffset;          // 滑鼠在拼圖中的位置
        private PictureBox _draggingBox = null;

        private readonly Random _rng = new Random();

        public Q4()
        {

            // 事件綁定
            this.button1.Click += button1_Click;   // 3x3
            this.button2.Click += button2_Click;   // 4x5
            this.panel1.Paint += panel1_Paint;

            this.DoubleBuffered = true;
        }

        // ==============================
        // 按鈕：開始 3x3 / 4x5
        // ==============================

        private void button1_Click(object sender, EventArgs e)
        {
            StartGame(3, 3);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StartGame(4, 5);
        }

        // ==============================
        // 建立遊戲
        // ==============================

        private void StartGame(int rows, int cols)
        {
            _rows = rows;
            _cols = cols;

            // 清掉舊的拼圖
            foreach (var pb in _pieces)
            {
                this.Controls.Remove(pb);
                pb.Dispose();
            }
            _pieces.Clear();
            _correctCell.Clear();
            _currentCell.Clear();

            _cellOccupant = new PictureBox[_rows, _cols];

            // 切圖並打亂順序
            var pieces = CutImage("dolamo.png", _rows, _cols);
            Shuffle(pieces);

            // 拼圖大小依照左邊 panel1 的格子大小
            int cellW = panel1.ClientSize.Width / _cols;
            int cellH = panel1.ClientSize.Height / _rows;

            // 一開始把拼圖排在右邊 panel2 的範圍中
            int startX = panel2.Left + 10;
            int startY = panel2.Top + 10;
            int margin = 5;

            int index = 0;
            foreach (var p in pieces)
            {
                var pb = new PictureBox();
                pb.Size = new Size(cellW, cellH);
                pb.Image = p.bmp;
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.BorderStyle = BorderStyle.FixedSingle;

                // 初始位置：panel2 區域內排好
                int colIndex = index % _cols;
                int rowIndex = index / _cols;
                pb.Left = startX + colIndex * (cellW + margin);
                pb.Top = startY + rowIndex * (cellH + margin);

                pb.MouseDown += Piece_MouseDown;
                pb.MouseMove += Piece_MouseMove;
                pb.MouseUp += Piece_MouseUp;

                this.Controls.Add(pb);
                pb.BringToFront();

                _pieces.Add(pb);
                _correctCell[pb] = new Point(p.col, p.row); // 正確的格子座標
                _currentCell[pb] = null;                    // 目前還沒放在格子上
                index++;
            }

            // 重畫格線
            panel1.Invalidate();
        }

        // 切圖：回傳每片圖 + 它原本對應的(row, col)
        private List<(Bitmap bmp, int row, int col)> CutImage(string fileName, int rows, int cols)
        {
            var result = new List<(Bitmap bmp, int row, int col)>();
            Bitmap source = new Bitmap(fileName);

            int pieceW = source.Width / cols;
            int pieceH = source.Height / rows;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Rectangle rect = new Rectangle(c * pieceW, r * pieceH, pieceW, pieceH);
                    Bitmap piece = source.Clone(rect, source.PixelFormat);
                    result.Add((piece, r, c));
                }
            }

            source.Dispose();
            return result;
        }

        // 洗牌
        private void Shuffle<T>(IList<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = _rng.Next(i + 1);
                T tmp = list[i];
                list[i] = list[j];
                list[j] = tmp;
            }
        }

        // ==============================
        // 拖曳事件
        // ==============================

        private void Piece_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            _draggingBox = sender as PictureBox;
            _isDragging = true;
            _mouseOffset = e.Location;

            _draggingBox.BringToFront();

            // 如果原本就綁在某個格子上，先把那格清掉
            if (_currentCell[_draggingBox].HasValue)
            {
                var cell = _currentCell[_draggingBox].Value;
                _cellOccupant[cell.Y, cell.X] = null;
                _currentCell[_draggingBox] = null;
            }
        }

        private void Piece_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDragging || _draggingBox == null) return;

            Point mousePos = this.PointToClient(Cursor.Position);
            _draggingBox.Left = mousePos.X - _mouseOffset.X;
            _draggingBox.Top = mousePos.Y - _mouseOffset.Y;
        }

        private void Piece_MouseUp(object sender, MouseEventArgs e)
        {
            if (!_isDragging || _draggingBox == null) return;

            _isDragging = false;

            // 滑鼠放開的位置（以 Form 為座標）
            Point mousePos = this.PointToClient(Cursor.Position);

            // 如果在左邊 panel1 的範圍內 → 嘗試綁定格子
            if (panel1.Bounds.Contains(mousePos))
            {
                // 算出是 panel1 的哪一格
                int cellW = panel1.ClientSize.Width / _cols;
                int cellH = panel1.ClientSize.Height / _rows;

                int relX = mousePos.X - panel1.Left;
                int relY = mousePos.Y - panel1.Top;

                int col = Math.Min(_cols - 1, Math.Max(0, relX / cellW));
                int row = Math.Min(_rows - 1, Math.Max(0, relY / cellH));

                // 如果這一格已經有別人的拼圖 → 把原本那塊移開
                var existing = _cellOccupant[row, col];
                if (existing != null && existing != _draggingBox)
                {
                    _currentCell[existing] = null;
                    _cellOccupant[row, col] = null;

                    // 移到右邊 panel2 的入口附近（讓使用者再拖）
                    existing.Left = panel2.Left + 10;
                    existing.Top = panel2.Top + 10;
                }

                // 讓這塊佔據這個格子
                _cellOccupant[row, col] = _draggingBox;
                _currentCell[_draggingBox] = new Point(col, row);

                // 對齊到格子的左上角
                Rectangle cellRect = GetCellRectangle(row, col);
                _draggingBox.Left = cellRect.Left;
                _draggingBox.Top = cellRect.Top;

                // 檢查是否過關
                CheckWinCondition();
            }

            _draggingBox = null;
        }

        // 取得某格的矩形範圍（以 Form 為座標）
        private Rectangle GetCellRectangle(int row, int col)
        {
            int cellW = panel1.ClientSize.Width / _cols;
            int cellH = panel1.ClientSize.Height / _rows;

            int x = panel1.Left + col * cellW;
            int y = panel1.Top + row * cellH;

            return new Rectangle(x, y, cellW, cellH);
        }

        // ==============================
        // 畫出左邊格線
        // ==============================

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (_rows <= 0 || _cols <= 0) return;

            int cellW = panel1.ClientSize.Width / _cols;
            int cellH = panel1.ClientSize.Height / _rows;

            using (Pen pen = new Pen(Color.Red, 2))
            {
                // 直線
                for (int c = 0; c <= _cols; c++)
                {
                    int x = c * cellW;
                    e.Graphics.DrawLine(pen, x, 0, x, panel1.ClientSize.Height);
                }
                // 橫線
                for (int r = 0; r <= _rows; r++)
                {
                    int y = r * cellH;
                    e.Graphics.DrawLine(pen, 0, y, panel1.ClientSize.Width, y);
                }
            }
        }

        // ==============================
        // 過關判定
        // ==============================

        private void CheckWinCondition()
        {
            foreach (var pb in _pieces)
            {
                // 還有沒放在格子上的 → 尚未完成
                if (!_currentCell[pb].HasValue)
                    return;

                Point cur = _currentCell[pb].Value;
                Point correct = _correctCell[pb];

                if (cur.X != correct.X || cur.Y != correct.Y)
                    return;
            }

            // 全部位置都正確，過關
            MessageBox.Show("Congratulations!", "Puzzle",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
