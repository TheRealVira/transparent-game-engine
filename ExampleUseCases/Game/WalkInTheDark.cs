using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using TransparentGameEngine;

namespace ExampleUseCases.Game
{
    class WalkInTheDark:TransparentGameComponent
    {
        private const int Size = 200;
        private Image Lightimage;

        public WalkInTheDark()
        {
            BackColor = Color.Black;
            Lightimage = Image.FromFile("./Content/Light.png");
            
            float[][] colorMatrixElements = {
                new float[] { TransparentGameComponent.TransparancyKey.R / 255.0f, 0, 0, 0, 0 },
                new float[] { 0, TransparentGameComponent.TransparancyKey.G / 255.0f, 0, 0, 0 },
                new float[] { 0, 0, TransparentGameComponent.TransparancyKey.B / 255.0f, 0, 0 },
                new float[] { 0, 0, 0, 1, 0 },
                new float[] { 0, 0, 0, 0, 1 }
            };

            var cmPicture = new System.Drawing.Imaging.ColorMatrix(colorMatrixElements);
            
            // Set the new color matrix
            var iaPicture = new System.Drawing.Imaging.ImageAttributes();
            var bmpPicture = new Bitmap(Lightimage.Width, Lightimage.Height);
            iaPicture.SetColorMatrix(cmPicture);
            // Set the Graphics object from the bitmap
            var gfxPicture = Graphics.FromImage(bmpPicture);
            // New rectangle for the picture, same size as the original picture
            var rctPicture = new Rectangle(0, 0, Lightimage.Width, Lightimage.Height);
            // Draw the new image
            gfxPicture.DrawImage(Lightimage, rctPicture, 0, 0, Lightimage.Width, Lightimage.Height, GraphicsUnit.Pixel, iaPicture);
            // Set the PictureBox to the new inverted colors bitmap
            Lightimage = bmpPicture;
        }

        public override void Update(TimeSpan elapsedTimeSpan)
        {
            
        }

        public override void Draw(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Lightimage, new Point[]
            {
                new Point(MousePosition.X-Size/2, MousePosition.Y-Size/2), 
                new Point(MousePosition.X+Size/2, MousePosition.Y-Size/2), 
                new Point(MousePosition.X-Size/2, MousePosition.Y+Size/2), 
            });
        }

    }
}
