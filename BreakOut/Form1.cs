using System;
using System.Drawing;
using System.Windows.Forms;

namespace BreakOut
{
    public partial class Form1 : Form
    {
        private int bouncerPos = 0;
        private int ballPosX = 0, ballPosY = 0, ballSpeedX = 0, ballSpeedY = 0;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!timer1.Enabled && e.KeyCode == Keys.Space)//ゲームが開始しているかはタイマーの状態で判断する
            {
                Title.Text = string.Empty;
                ballPosX = Ball1.Left; 
                ballPosY = Ball1.Top;
                double a = new Random().NextDouble() * 5.8 + 1;
                ballSpeedX =(int) (Math.Sin(a) * 10);
                ballSpeedY=(int) (Math.Sin(a)/ 10);


                timer1.Start();
            }
            if (e.KeyCode == Keys.Escape)
                Application.Exit();
            if (!timer1.Enabled) 
                return;
            if (e.KeyCode == Keys.Left && (Math.Abs(bouncerPos) < 3 || bouncerPos == 3))
            {
                bouncerPos--;
            }
            if (e.KeyCode == Keys.Right && (Math.Abs(bouncerPos) < 3 || bouncerPos == -3))
            {
                bouncerPos++;
            }

            Bouncer.Location = new Point(posHolder[bouncerPos + 3], 360);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {

        }
        int[] posHolder = new int[] { 0, 130, 230, 330, 430, 530, 660};
        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
