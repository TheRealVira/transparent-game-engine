using System;
using System.Drawing;
using System.Windows.Forms;
using TransparentGameEngine;

namespace ExampleUseCases.Game
{
    class FireCursor:TransparentGameComponent
    {
        public FireCursor()
        {
            FireImage = Image.FromFile("Content/fire.gif");
            ImageAnimator.Animate(FireImage, null);
        }

        private Image FireImage;
        private Point LastFirePosition;
        private const int FireSize = 75;

        public override void Update(TimeSpan elapsedTimeSpan)
        {
            LastFirePosition = Extension.Lerp(LastFirePosition, MousePosition, .2f);
            ImageAnimator.UpdateFrames(FireImage);
        }

        public override void Draw(object sender, PaintEventArgs e)
        {
            // e.Graphics.DrawLine(Pens.Red, MousePosition, LastFirePosition);

            e.Graphics.DrawImage(FireImage, new []
            {
                new Point(LastFirePosition.X-FireSize/2, LastFirePosition.Y-FireSize),
                new Point(LastFirePosition.X+FireSize/2, LastFirePosition.Y-FireSize),
                new Point(MousePosition.X-FireSize/2, MousePosition.Y),
            });
        }
    }
}
