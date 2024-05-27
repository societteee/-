using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;


namespace game
{
    class Player
    {
        public int x;
        public int y;
        public int sizeX ;
        public int sizeY ;
        public bool rightAttack = false;
        public int attackRange = 400*Form1.scaleP;
        public bool leftAttack = false;
        public bool reverse = false;
        public Image samImag;
        public int currentAnimation = 1;
        public int currentFrame = 0;
        public int currentLimit;

        public int idleFrames =1 ;
        public int attackFrames =4;
        public int deathFrames;
        public Image spriteSheet = Resource1.newSam;
        public Player(int x, int y, int sizeX = 25, int sizeY = 34)
        {
            samImag = Resource1.newSam;
            this.x = x - 100;
            this.y = y;
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            this.currentLimit = idleFrames;
        }
        public void AttackRight()
        {
            rightAttack = true;
            this.currentAnimation = 0;
            this.reverse = false;
        }
        public void AttackLeft()
        {
            leftAttack = true;
            this.currentAnimation = 0;
            this.reverse = true;
        }
        public void Playanimation(Graphics g)
        {
            SetAnimation();
            int localX = this.x;
            if (currentFrame < currentLimit-1)
                currentFrame++;
            else currentFrame = 0;
            Image pic = TakePic(currentFrame, currentAnimation);
            var scale = 2;
            pic = Resize(pic, scale);
            if (reverse)
            {
                pic.RotateFlip(RotateFlipType.RotateNoneFlipX);
                localX -= 150;
            }
            else localX -= 150;
            g.DrawImage(pic, localX, this.y);
        }
        public Bitmap TakePic(int currentFrame, int currentAnimation)
        {
            Bitmap bmp = new Bitmap(230, 392);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(spriteSheet, 0, 0, new Rectangle(new Point(230 * currentFrame, 392 * currentAnimation), new Size(230, 392)), GraphicsUnit.Pixel);
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
                        currentLimit = attackFrames;
                        break;
                    }
                case 1:
                    {
                        currentLimit = idleFrames;
                        break;
                    }
            }

        }
    }
}
