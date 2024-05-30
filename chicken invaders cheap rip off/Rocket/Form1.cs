using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Rocket
{
    public partial class Form1 : Form
    {
        Bitmap offimage;
        int xhero;
        int yhero;
        List<Bitmap> saru5 = new List<Bitmap>();
        int rocket = 0;
        Bitmap starim = new Bitmap("star.bmp");
        List<Star> lb = new List<Star>();
        Timer T = new Timer();
        Random r = new Random();
        int counter = 19;
        int speedd = 3;
        int flaaag = 0;
        int flaaag2 = 0;
        public class Star
        {
            public int x;
            public int y;
            public int speed;
        }
        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.Load += Form1_Load; 
            this.Paint += Form1_Paint;
            for (int i = 1; i < 2; i++) 
            {
                Bitmap pnn = new Bitmap(i.ToString() + ".bmp");
                pnn.MakeTransparent(Color.White);
                saru5.Add(pnn);
            }
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;
            T.Start();
            T.Interval = 1;
            T.Tick += T_Tick;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            flaaag2 = 0;
        }

        private void T_Tick(object sender, EventArgs e)
        {
            counter++;
            if (counter == 20)
            {
                Star pnn = new Star();
                pnn.x = r.Next(100, this.ClientSize.Width);
                pnn.y = 0;
                pnn.speed = 3;
                lb.Add(pnn);
                counter = 0;
            }
            for (int i = 0; i < lb.Count; i++)
            {
                if (lb[i].y < this.ClientSize.Height - starim.Height) 
                {
                    lb[i].y += lb[i].speed;

                    if (flaaag == 1)
                    {
                        lb[i].x += lb[i].speed;
                    }
                    if (flaaag == 2)
                    {
                        lb[i].x -= lb[i].speed;
                    }
                }   
            }
            if (flaaag2 == 1)
            {
                for(int i = 0; i < lb.Count; i++)
                {
                    if (isHit(lb[i], xhero + 100, yhero))
                    {
                        lb[i].x = -100;
                        lb[i].y = -100;
                    }
                }
                
            }
            if (counter % 10 == 0)
            {
                flaaag = 1;
            }
            if (counter % 20 == 0)
            {
                flaaag = 2;
            }

            Drawdoublebuffer(this.CreateGraphics());
        }
        bool isHit(Star ptrav, int xx,int yy)
        {
            if ((xx > ptrav.x && xx < (ptrav.x + starim.Width))
                || (((xx + 3) > ptrav.x)
                        && ((xx+ 3) < (ptrav.x + starim.Width))
                    )
                )
                return true;
            return false;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                xhero-=15;
                rocket++;
                if (rocket == saru5.Count) 
                {
                    rocket = 0;
                }
            }
            if (e.KeyCode == Keys.Right)
            {
                xhero+=15;
                rocket++;
                if (rocket == saru5.Count) 
                {
                    rocket = 0;
                }
            }
            if (e.KeyCode == Keys.Space)
            {
                flaaag2 = 1;
            }
            
            //Drawdoublebuffer(this.CreateGraphics());
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Drawdoublebuffer(e.Graphics);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            offimage = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            xhero = this.ClientSize.Width - 200;
            yhero = this.ClientSize.Height - saru5[0].Height;
            starim.MakeTransparent(Color.White);
        }
        void Drawdoublebuffer(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(offimage);
            Drawscene(g2);
            g.DrawImage(offimage, 0, 0);
        }
        void Drawscene(Graphics g) 
        {
            Pen pn = new Pen(Color.Yellow);
            g.Clear(Color.Black);
            g.DrawImage(saru5[rocket], xhero, yhero);
            for (int i = 0; i < lb.Count; i++)
            {
                g.DrawImage(starim, lb[i].x, lb[i].y);
            }
            if (flaaag2 == 1)
            {
                g.DrawLine(pn, xhero + 100, 0, xhero + 100, yhero);
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}



