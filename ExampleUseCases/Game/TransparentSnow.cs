using System;
using System.Drawing;
using System.Windows.Forms;
using TransparentGameEngine;

namespace ExampleUseCases.Game
{
    internal class TransparentSnow : TransparentGameComponent
    {
        private const double SnowPace = .3;
        private const int SnowSize = 6;
        private readonly Random _rand;

        private readonly (Point position, Color color)[] Snow = new (Point position, Color color)[40];

        public TransparentSnow()
        {
            _rand = new Random(DateTime.Now.Millisecond);
        }

        public override void Update(TimeSpan elapsedTimeSpan)
        {
            for (var i = 0; i < Snow.Length; i++)
            {
                if (Snow[i].position.Y >= Height - SnowSize)
                {
                    if (_rand.Next(0, 20) == 0)
                        Snow[i] = GenerateSnowflake();

                    continue;
                }

                Snow[i].position.Y += (int) (elapsedTimeSpan.Milliseconds * SnowPace);

                if (!_rand.Next(0, 2).Equals(0)) continue;

                Snow[i].position.X += 3 * (_rand.Next(0, 2).Equals(1) ? -1 : 1);
                Snow[i].position.X = Snow[i].position.X.Clamp(Width, -SnowSize);
            }
        }

        private (Point position, Color color) GenerateSnowflake()
        {
            return (new Point(_rand.Next(0, Width), 0), GenerateRandomSnowflakeColor());
        }

        private Color GenerateRandomSnowflakeColor()
        {
            return Color.FromArgb(255, 255, _rand.Next(200, 256));
        }

        public override void Draw(object sender, PaintEventArgs e)
        {
            foreach (var snowflake in Snow)
                e.Graphics.FillEllipse(new SolidBrush(snowflake.color),
                    new Rectangle(snowflake.position, new Size(SnowSize,SnowSize)));
        }
    }
}