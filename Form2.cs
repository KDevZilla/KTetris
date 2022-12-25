using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KTetris
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        int TopOffset = 15;
        int LeftOffset = 15;
        int NumberofRow = 20;
        int NumberofCol = 10;
        int[,] Board = null;
        private Point From1Dto2D(int index)
        {
            int x = index % NumberofCol;
            int y = index / NumberofRow;
            return new Point(x, y);
        }
        private int From2Dto1D(int x, int y)
        {
            return x * NumberofCol + y * NumberofRow;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Timer t1 = new Timer();
            t1.Interval = 300;
            t1.Enabled = true;
            t1.Tick += T1_Tick;
        }
        private void T1_Tick(object sender, EventArgs e)
        {

        }
        private void Form2_Load(object sender, EventArgs e)
        {
            Board = new int[NumberofRow, NumberofCol];
            int i;
            int j;
            for (i = 0; i < NumberofRow; i++)
            {
                for (j = 0; j < NumberofCol; j++)
                {
                    Board[i, j] = 0;
                }
            }
            Board[10, 2] = 1;

            pictureBox1.BackColor = Color.FromArgb(183, 199, 182);
            //   pictureBox1.ForeColor = Color.FromArgb(44, 48, 46);
            pictureBox1.Paint += PictureBox1_Paint;
            Rectangle R = new Rectangle(LeftOffset, TopOffset, BlockSize * BLockColumn, BlockSize * BlockStoy);

            pictureBox1.Height = R.Top + R.Height + TopOffset;
            pictureBox1.Width = LeftOffset + R.Width + 200;
            pictureBox1.Top = 0;
            pictureBox1.Left = 0;
            this.Width = pictureBox1.Width;
            this.Height = pictureBox1.Height + TopOffset;
        }

        int BlockSize = 20;
        int BlockStoy = 20;
        int BLockColumn = 10;
        private void DrawBox(Graphics g, int X, int Y)
        {
            Color PenColor = Color.FromArgb(44, 48, 46);

            Pen PBlock = new Pen(PenColor, 2);
            int BoxLeft = (X - 1) * BlockSize;
            int BoxTop = (Y - 1) * BlockSize;

            Rectangle RBlock = new Rectangle(LeftOffset + 1 + BoxLeft, TopOffset + 1 + BoxTop, BlockSize, BlockSize);
            Rectangle RBlockInside = new Rectangle(LeftOffset + 5 + BoxLeft, TopOffset + 5 + BoxTop, BlockSize - 8, BlockSize - 8);
            g.DrawRectangle(PBlock, RBlock);
            g.FillRectangle(new SolidBrush(PenColor), RBlockInside);
        }
        private void RenderBoard(Graphics g, int[,] pBoard)
        {
            int i;
            int j;

            for (i = 0; i < NumberofRow; i++)
            {
                for (j = 0; j < NumberofCol; j++)
                {
                    if (pBoard[i, j] == 0)
                    {
                        continue;
                    }
                    DrawBox(g, j, i);
                }
            }
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            // throw new NotImplementedException();
            Color PenColor = Color.FromArgb(44, 48, 46);
            Pen P = new Pen(PenColor, 2);


            Rectangle R = new Rectangle(LeftOffset, TopOffset, BlockSize * BLockColumn, BlockSize * BlockStoy);

            e.Graphics.DrawRectangle(P, R);
            RenderBoard(e.Graphics, Board);

            int X = 0;
            int Y = 0;
            int i = 0;
            int j = 0;
            /*
            for(i=1;i<=20;i++)
            {
                for(j=1;j<=10;j++)
                {
                    DrawBox(e.Graphics, j, i);
                }
            }
            */

            /*
             DrawBox(e.Graphics, j, i);
             */

            //e.Graphics.DrawRectangle(P, RBlockInside);

        }
    }
}
