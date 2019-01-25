using System;
using System.Drawing;
using System.Windows.Forms;
using TransparentGameEngine;

namespace ExampleUseCases.Game
{
    class WalkInTheDark:TransparentGameComponent
    {
        private new const int Size = 100;
        private readonly Image _lightimage;

        public WalkInTheDark()
        {
            BackColor = Color.Black;
            _lightimage = Image.FromFile("./Content/Light.png");
            
            float[][] colorMatrixElements = {
                new [] { TransparentGameComponent.TransparancyKey.R / 255.0f, 0, 0, 0, 0 },
                new [] { 0, TransparentGameComponent.TransparancyKey.G / 255.0f, 0, 0, 0 },
                new [] { 0, 0, TransparentGameComponent.TransparancyKey.B / 255.0f, 0, 0 },
                new float[] { 0, 0, 0, TransparentGameComponent.TransparancyKey.A, 0 },
                new float[] { 0, 0, 0, 0, 1 }
            };

            var cmPicture = new System.Drawing.Imaging.ColorMatrix(colorMatrixElements);
            
            // Set the new color matrix
            var iaPicture = new System.Drawing.Imaging.ImageAttributes();
            var bmpPicture = new Bitmap(_lightimage.Width, _lightimage.Height);
            iaPicture.SetColorMatrix(cmPicture);
            // Set the Graphics object from the bitmap
            var gfxPicture = Graphics.FromImage(bmpPicture);
            // New rectangle for the picture, same size as the original picture
            var rctPicture = new Rectangle(0, 0, _lightimage.Width, _lightimage.Height);
            // Draw the new image
            gfxPicture.DrawImage(_lightimage, rctPicture, 0, 0, _lightimage.Width, _lightimage.Height, GraphicsUnit.Pixel, iaPicture);
            // Set the PictureBox to the new inverted colors bitmap
            _lightimage = bmpPicture;
        }

        public override void Update(TimeSpan elapsedTimeSpan)
        {
            
        }

        public override void Draw(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(_lightimage, new Point[]
            {
                new Point(MousePosition.X-Size/2, MousePosition.Y-Size/2), 
                new Point(MousePosition.X+Size/2, MousePosition.Y-Size/2), 
                new Point(MousePosition.X-Size/2, MousePosition.Y+Size/2), 
            });
        }

    }
}
