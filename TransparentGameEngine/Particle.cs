using System;
using System.Drawing;
using System.Windows.Forms;

namespace TransparentGameEngine
{
    public class Particle:TransparentGameComponent
    {
        public Vector2 Location { get; set; }
        public Size MySize { get; set; }
        public Color Color { get; set; }
        public Vector2 Force { get; set; }

        public double Counterforce = .5;

        public Particle(){}

        public Particle(Vector2 location, Size size, Color color, Vector2 force)
        {
            Location = location;
            MySize = size;
            Color = color;
            Force = force;
        }

        public override void Update(TimeSpan elapsedTimeSpan)
        {
            Location += Force * elapsedTimeSpan.Milliseconds;
            var newX = Extension.Lerp(Force.X, 0, Counterforce);
            var newY = Extension.Lerp(Force.Y, 0, Counterforce);

            Force = new Vector2((float)newX, (float)newY);
        }

        public override void Draw(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(new SolidBrush(Color), new Rectangle(Location.ToPoint(), MySize));
        }
    }
}
