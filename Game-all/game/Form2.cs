using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game
{
    public partial class Form2 : Form
    {
        MyButton start = new MyButton();
        Button btn;
        Bitmap startButton = Resource1.play;
        Bitmap endB = Resource1.exit;
        bool firstTime = true;
        public Form2()
        {
            InitializeComponent();
            timer3.Interval = 200;
            timer3.Tick += new EventHandler(update);
            timer3.Start();
            Invalidate();
        }
        public void update(object sender, EventArgs e)
        {
            if (firstTime) start.CreateMyButton(btn, this, this.Width / 2 - startButton.Width, this.Height / 2 - startButton.Height, startButton.Width * 2, startButton.Height * 2, startButton, StartGame);
            if (firstTime) start.CreateMyButton(btn, this, this.Width / 2 - endB.Width, this.Height / 2 + startButton.Height, endB.Width * 2, endB.Height * 2, endB, Exit);
            firstTime = false;
        }
        public void StartGame(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            form1.form2 = this;
            this.Hide();
        }
        public void Exit(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Prorisovka(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(Resource1.backStart, 0, 0, this.Width, this.Height);
        }
    }
}
