using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication5
{
    public struct Ball
    {
        public Color Color;
        public bool R;
        public bool T;
        public int H;
        public int W;
        public int X;
        public int Y;
        public Ball(Color Color, bool R, bool T, int W, int H, int X, int Y)
        {
            this.Color = Color;
            this.H = H;
            this.W = W;
            this.X = X;
            this.Y = Y;
            this.T = T;
            this.R = R;
        }
    }
    public struct Bumper
    {
        public Color Color;
        public int H;
        public int W;
        public int X;
        public int Y;
        public Bumper(Color Color, int W, int H, int X, int Y)
        {
            this.Color = Color;
            this.H = H;
            this.W = W;
            this.X = X;
            this.Y = Y;
        }
    }

    public partial class Form1 : Form
    {
        Bitmap b;
        Graphics g;
        Random r = new Random();

        Ball[] Balls;

           
        Ball Ball = new Ball(Color.White, false, true, 15, 15, 500, 50);
        
        
        Bumper Bumper = new Bumper(Color.White, 30, 5, 30, 445);

 


        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Ball.T)
            {
                if (Ball.Y <= 5)
                {
                    Ball.Y += 20;
                    Ball.T = false;
                }
                Ball.Y -= 10;
            }
            else
            {
                if (Ball.Y > pictureBox1.Height - 1)
                {
                    if (Ball.X >= Bumper.X && Ball.X <= Bumper.X + Bumper.W)
                    {
                        Ball.Y -= 30;
                        Ball.T = true;

                        Color c = Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));
                        Ball.Color = c;
                        Bumper.Color = c;

                    }
                    else
                    {
                        
                        timer1.Enabled = false;
                        MessageBox.Show("game over :>");
                    }
                }
                Ball.Y += 10;
            }
            if (Ball.R)
            {
                if (Ball.X <= 5)
                {
                    Ball.X += 10;
                    Ball.R = false;
                }
                Ball.X -= 5;
            }
            else
            {
                if (Ball.X > pictureBox1.Width - 15)
                {
                    Ball.X -= 10;
                    Ball.R = true;
                }
                Ball.X += 5;
            }
            DrawField();
        }




        private void DrawField()
        {
            g.Clear(Color.Black);
            g.FillRectangle(new SolidBrush(Bumper.Color), 0, 0, pictureBox1.Width, 5);
            g.FillRectangle(new SolidBrush(Bumper.Color), 0, 0, 5, pictureBox1.Height);
            g.FillRectangle(new SolidBrush(Bumper.Color), pictureBox1.Width - 5, 0, 5, pictureBox1.Height);

            g.FillRectangle(new SolidBrush(Bumper.Color), Bumper.X, Bumper.Y, Bumper.W, Bumper.H);

            g.FillEllipse(new SolidBrush(Ball.Color), (Ball.X < 5 ? 5 : (Ball.X > pictureBox1.Width - Ball.W ? pictureBox1.Width - Ball.W : Ball.X)), (Ball.Y < 5 ? 5 : (Ball.Y > pictureBox1.Height - Ball.H ? pictureBox1.Height - Ball.H : Ball.Y)), 10, 10);

            pictureBox1.Image = b;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(b);
        }



        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Bumper.X = e.X;
        }




        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Hide();
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
        }
    }
}
