using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
namespace game
{
    class Enemy
        { 
        public int x;
        public int y;
        public int sizeX;
        public int sizeY;
        public int speed;
        Random rand = new Random();
        public bool live = true;
        public bool reverse = false;
        public int distanceToP;
        public int attackRange = 50 * Form1.scale;
        public int currentAnimation = 0;
        public int currentFrame = 0;
        public int currentLimit = 3;
        public int flyAnimations = 3;
        public bool isMoving = true;
        public int deathAnimations = 5;
        public Image spriteSheet = Resource1.demonSprite;
        public Enemy(int x, int y, bool reverse, int sizeX = 44  , int sizeY = 47 )
        {
            this.x = x;
            this.y = y;
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            speed = rand.Next(20, 25);
            this.reverse = reverse;
        }

        public void Move()
        {
            if (!reverse) x += speed;
            else x -= speed;
        }
        public void Death()
        {
            live = false;
        }
        public void PlayAnimation(Graphics g)
        {
            SetAnimation();
            int localX = this.x;
            if (currentFrame < currentLimit - 1)
                currentFrame++;
            else currentFrame = 0;
            Image pic = TakePic(currentFrame, currentAnimation);
            var scale = Form1.scale;
            pic = Resize(pic, scale);
            if (reverse)
            {
                pic.RotateFlip(RotateFlipType.RotateNoneFlipX);
                localX -= 300;
            }
            else localX -= 300;
            g.DrawImage(pic, localX, this.y);
            if (currentFrame == currentLimit-1 && currentAnimation == 1)
            {
                currentAnimation = 0;
                this.Death();
                currentFrame = 0;
            }
        }
        public Bitmap TakePic(int currentFrame, int currentAnimation)
        {
            Bitmap bmp = new Bitmap(120, 170);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(spriteSheet, 0, 0, new Rectangle(new Point(120 * currentFrame, 170 * currentAnimation), new Size(120, 170)), GraphicsUnit.Pixel);
            return bmp;
        }
        static Image Resize(Image img, float scale)
        {
            var res = new Bitmap((int)(img.Width * scale), (int)(img.Height * scale));
            using (var gr = Graphics.FromImage(res))
            {
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                gr.DrawImage(img, 0, 0, res.Width, res.Height);
            }

            return res;
        }
        public void SetAnimation()
        {
            switch (currentAnimation)
            {
                case 0:
                    {
                        currentLimit = flyAnimations;
                        break;
                    }
                case 1:
                    {
                        currentLimit = deathAnimations;
                        break;
                    }
            }

        }
    }
}
