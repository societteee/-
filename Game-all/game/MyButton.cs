using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game
{
    class MyButton : Button
    {
        public void CreateMyButton(Button btn, Form frm,  int x, int y, int w, int h, Bitmap image, EventHandler evh, string str = "")
        {
            btn = new Button();
            btn.Text = str;
            btn.Image = Resize(image, 2);
            btn.Location = new Point(x, y);
            btn.Size = new Size(w, h);
            btn.Click += evh;

            frm.Controls.Add(btn);
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
    }
}
