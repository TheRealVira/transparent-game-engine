using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using TransparentGameEngine;

namespace ExampleUseCases.Game
{
    class Magnifier:TransparentGameComponent
    {
        private const int WIDTH = 150;
        private const int HEIGHT = 75;
        private new static readonly Rectangle Bounds = new Rectangle(MousePosition.X - WIDTH / 2, MousePosition.Y - HEIGHT / 2, WIDTH, HEIGHT);
        private Bitmap ScreenCapture = new Bitmap(WIDTH, HEIGHT);

        public override void Update(TimeSpan elapsedTimeSpan)
        {
            using (var g = Graphics.FromImage(ScreenCapture))
            {
                g.CopyFromScreen(new Point(MousePosition.X - WIDTH / 2, MousePosition.Y - HEIGHT / 2), Point.Empty,
                    Bounds.Size);
            }
        }

        public override void Draw(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(ScreenCapture,new []
            {
                new Point(10,10),
                new Point(310,10),
                new Point(10, 160),
            });
        }
    }
}
