using System;
using System.Drawing;
using System.Windows.Forms;

namespace ExampleUseCases.Game
{
    public class FollowingCircle : TransparentGameEngine.TransparentGameComponent
    {
        private Point _circlePosition;

        public override void Update(TimeSpan elapsedTimeSpan)
        {
            _circlePosition = MousePosition;
        }

        public override void Draw(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(Color.Yellow, 2), new Point(0, 0), _circlePosition);
            e.Graphics.DrawLine(new Pen(Color.Yellow, 2), new Point(Width, 0), _circlePosition);
            e.Graphics.DrawLine(new Pen(Color.Yellow, 2), new Point(Width, Height), _circlePosition);
            e.Graphics.DrawLine(new Pen(Color.Yellow, 2), new Point(0, Height), _circlePosition);

            e.Graphics.FillEllipse(new SolidBrush(Color.Aqua),
                new Rectangle(new Point(_circlePosition.X - 25 / 2, _circlePosition.Y - 25 / 2), new Size(25, 25)));
        }
    }
}