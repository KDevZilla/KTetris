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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Game Tetris;
        private void MyGame_ScoreChange(object sender, EventArgs e)
        {
            this.lblScore.Text = "Score " + Tetris.Score.ToString() +
                                "\nLines " + Tetris.Lines.ToString();

        }
        private void MyGame_GameFinished(object sender, EventArgs e)
        {
           // this.button1.Enabled = true;
            this.timer1.Enabled = false;
            MessageBox.Show("Game over");

        }
        private void NewGame()
        {

            Tetris = new Game(this.doubleBufferedPanel1,this.doubleBufferedPanel2 , 20, 10);
            Tetris.ScoreChange+=new EventHandler(MyGame_ScoreChange);
            Tetris.GameFinished +=new EventHandler(MyGame_GameFinished);
            this.timer1.Enabled = true;
            Tetris.Loop();
            /*
            MyGame.ScoreChange += new EventHandler(MyGame_ScoreChange);
            MyGame.GameFinished += new EventHandler(MyGame_GameFinished);
            MyGame.EndEditMode();
            MyGame.CellColorInEditMode = cBlockGame.enMyColor.White_0;
            HasInitial = true;
             */
        }
        private void button1_Click(object sender, EventArgs e)
        {
            NewGame();
           // this.button1.Enabled = false;
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            Tetris.MoveCell(Game.enDirection.Down);
            Tetris.Loop();
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            Tetris.MoveCell(Game.enDirection.Right );
            Tetris.Loop();
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            Tetris.MoveCell(Game.enDirection.Left );
            Tetris.Loop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Tetris.RolateBloack();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
     
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(Tetris.GameStaus == Game.enGameStatus.Finish)
            {
                return;
            }
            switch (e.KeyCode)
            {
                case Keys.Down :
                    Tetris.MoveCell(Game.enDirection.Down);
                    break;
                case Keys.Up:
                    Tetris.RolateBloack();
                   
                    break;
                case Keys.Left :
                    Tetris.MoveCell(Game.enDirection.Left);
                    break;
                case Keys.Right :
                    Tetris.MoveCell(Game.enDirection.Right);
                    break;
                case Keys.Space :
                    Tetris.FallCell();
                    break;
            }
            Tetris.Loop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Tetris.MoveCell(Game.enDirection.Down);
            Tetris.Loop();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show ("Do you want to exit ?") != DialogResult.OK)
            {
                return;
            }
            Application.Exit();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
        }
    }
}
