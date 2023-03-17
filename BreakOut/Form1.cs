using System;
using System.Windows.Forms;

namespace BreakOut
{
    public partial class Form1 : Form
    {
        private int paddlePos = 0;
        private int ballSpeedX = 0, ballSpeedY = 0;
        System.Collections.Generic.List<Label> Blocks = new System.Collections.Generic.List<Label>();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!timer1.Enabled && e.KeyCode == Keys.Space)//ゲームが開始しているかはタイマーの状態で判断する
            {
                Title.Text = string.Empty;
                double a = new Random().NextDouble() * 2.8 + 0.1;
                ballSpeedX = (int) (Math.Cos(a) * 14);
                ballSpeedY = (int) -(Math.Sin(a) * 14);
                Ball.Top = 300;
                Ball.Left = 380;
                paddlePos = 0;
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 5; y++)
                    {
                        int startx = 40, starty = 20;
                        Label block = new Label() { Size = new System.Drawing.Size(80, 20), BackColor = System.Drawing.Color.Cyan, Left = startx + 90 * x, Top = starty + 40 * y};
                        Blocks.Add(block);
                        Controls.Add(block);
                    }
                }
                timer1.Start();
            }
            if (e.KeyCode == Keys.Escape)
                Application.Exit();
            if (!timer1.Enabled) 
                return;
            if (e.KeyCode == Keys.Left && (Math.Abs(paddlePos) < 3 || paddlePos == 3))
            {
                paddlePos--;
            }
            if (e.KeyCode == Keys.Right && (Math.Abs(paddlePos) < 3 || paddlePos == -3))
            {
                paddlePos++;
            }
            Paddle.Location = new System.Drawing.Point(posHolder[paddlePos + 3], 360);
        }
        int[] posHolder = new int[] { 0, 130, 230, 330, 430, 530, 660};
        private void timer1_Tick(object sender, EventArgs e)
        {
            Ball.Left += ballSpeedX;
            Ball.Top += ballSpeedY;
            if (Ball.Left < 0 || Ball.Left > Size.Width - 32)
            {
                ballSpeedX = -ballSpeedX;
            }
            if (Ball.Top < 0)
            {
                ballSpeedY = -ballSpeedY;
            }
            if (Blocks.Count == 0)
            {
                Title.Text = "Game Clear!!";
                timer1.Stop();
            }
            if (Ball.Top > Size.Height - 32){
                Title.Text = "Game Over!!\nPress Space to restart.";
                for (int i = 0; i < Blocks.Count; i++)
                {
                    Controls.Remove(Blocks[i]);
                }
                Blocks.RemoveRange(0, Blocks.Count);
                timer1.Stop();
            }
            if (Ball.Left > Paddle.Left && Ball.Left < Paddle.Left + 140 && Ball.Top + 32 > 360 && Ball.Top + 32 < 375)
                ballSpeedY = -ballSpeedY;
            foreach (var block in Blocks)
            {
                if (block.Bounds.IntersectsWith(Ball.Bounds))
                {
                    block.Name = "R";//コントロールから削除済タグをつける
                    if (Ball.Left + 32 > block.Left && Ball.Left + 32 < block.Left + 80)
                    {
                        ballSpeedY = -ballSpeedY;
                    } else
                    {
                        ballSpeedX = -ballSpeedX;
                    }
                    Controls.Remove(block);
                }
            }
            Blocks.RemoveAll(l => l.Name == "R");
        }
    }
}
