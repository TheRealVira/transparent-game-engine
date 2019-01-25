using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TransparentGameEngine;

namespace ExampleUseCases.Game
{
    class Matrix:TransparentGameComponent
    {
        private readonly Random _rand;
        private const int BufferedWaitingTime = 15;
        private int _waitingBuffer = BufferedWaitingTime;

        private readonly SpecialChar[] _specialChar = new SpecialChar[40];

        public Matrix()
        {
            _rand = new Random(DateTime.Now.Millisecond);
        }

        public override void Update(TimeSpan elapsedTimeSpan)
        {
            _waitingBuffer-=1;
            for (var i = 0; i < _specialChar.Length; i++)
            {
                if (_specialChar[i]==null||_specialChar[i].Position.Y >= Height)
                {
                    if (_rand.Next(0, 10).Equals(0))
                    {
                        _specialChar[i] = GenerateSpecialChar();
                    }
                    continue;
                }

                _specialChar[i].Position.Y += _waitingBuffer % _specialChar[i].Waiting == 0 ? 20 : 0;
            }

            if (_waitingBuffer <= 0)
            {
                _waitingBuffer = BufferedWaitingTime;
            }
        }

        private SpecialChar GenerateSpecialChar()
        {
            return new SpecialChar(new Point(_rand.Next(0, Width), 1), _rand.Next(1,BufferedWaitingTime), new string(new[] {(char) _rand.Next(char.MinValue, char.MaxValue)}));
        }

        public override void Draw(object sender, PaintEventArgs e)
        {
            foreach (var specialChar in _specialChar.Where(x=>x!=null))
            {
                e.Graphics.DrawString(specialChar.MyChar,
                    new Font(FontFamily.GenericMonospace, 12), new SolidBrush(Color.LawnGreen), specialChar.Position.X, specialChar.Position.Y);
            }
        }

        private class SpecialChar
        {
            public Point Position;
            public int Waiting { get; }
            public string MyChar { get; }

            public SpecialChar(Point position, int waiting, string myChar)
            {
                Position = position;
                Waiting = waiting;
                MyChar = myChar;
            }
        }
    }
}
