using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace game
{
    public partial class Form1 : Form
    {
        Player samurai;
        Enemy chert;
        public Random rnd = new Random();
        public int i = 1;
        public static int scale = 5;
        public static int scaleP = 1;
        public bool firstTime = true;
        public bool firstCostil = true;
        public Form2 form2;
        public bool end = false;
        public int score = 0;
        public Label mes = new Label();
        public Form1()
        {
            InitializeComponent();
            
            Invalidate();
            Init();
            Invalidate();
            MessageBox.Show(String.Format("Стрелка вправо - атака врпаво, влево - атака влево"));

            timer1.Interval = 150;
            timer1.Tick += new EventHandler(updaete);
            timer1.Start();
            Invalidate();
            timer2.Interval = 90;
            timer2.Tick += new EventHandler(update1);
            timer2.Start();
            Invalidate();
        }

        private void updaete(object sender, EventArgs e)
        {
            if (firstTime)
            {
                Init();
                firstTime = false;
            }
            Invalidate();
        }
        private void update1(object sender, EventArgs e)
        {
            if (chert.isMoving) chert.Move();
            if (chert.isMoving && chert.live && Math.Abs(this.Width / 2 - chert.x) <= 200)
            {
                end = true;
                Invalidate();
            }
        }

        public void Init()
        {
            samurai = new Player(this.Width/ 2 - 10, this.Height - 800, 25*scale, 34*scale);
            CreateChert();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.DrawImage(Resource1.backgorundpng, 0, 0, this.Width, this.Height);
            samurai.Playanimation(e.Graphics);
            if(chert.live==true)
                chert.PlayAnimation(e.Graphics);
            else CreateChert();
            if (end)
            {

                graphics.DrawImage(Resource1.died, this.Width / 2 - Resource1.died.Width/2, this.Height / 2 - Resource1.died.Height/2);
                if (!firstCostil)
                {
                    Thread.Sleep(1500);
                    Lose();
                }
                firstCostil = false;
            }
        }
        public void CreateChert()
        {
            int leftOrRight = rnd.Next(1, 100);
            int edge;
            bool reverse;
            if (!firstTime) Score();
            if (leftOrRight <50)
            {
                edge = 0;
                reverse = false;
            }
            else
            {
                edge = this.Width;
                reverse = true;
            }
            chert = new Enemy(edge, this.Height-1200, reverse);
        }
        private void RightStrelka(object sender, KeyEventArgs e)
        {
            int middle = this.Width/2;
            switch (e.KeyCode)
            {
                case Keys.Right:
                    {
                        if (chert.x<=middle+400  && chert.x>= middle)
                        {
                            chert.currentAnimation = 1;
                            chert.isMoving = false;
                        }
                        samurai.AttackRight();
                        break;
                    }
                case Keys.Left:
                    {
                        if (chert.x<=middle && chert.x>=middle-400)
                        {
                            chert.currentAnimation = 1;
                            chert.isMoving = false;
                        }
                        samurai.AttackLeft();
                        break;
                    }
            }
        }

        private void Up(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    {
                        samurai.currentAnimation = 1;

                        break;
                    }
                case Keys.Left:
                    {
                        samurai.currentAnimation = 1;
                        break;
                    }
            }

        }
        public void Lose()
        {
            Thread.Sleep(3000);
            form2.Show();
            this.Close();
        }
        public void Score()
        {
            score++;
            mes.Location = new Point(10,10);
            mes.Text = "Ваш счёт:"+ score.ToString();
            this.Controls.Add(mes);
        }
    }
}
