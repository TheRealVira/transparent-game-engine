using System;
using System.Drawing;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using TransparentGameEngine;

namespace ExampleUseCases.Game
{
    class Typewriter:TransparentGameComponent
    {
        public Typewriter()
        {
            Hook.GlobalEvents().KeyPress += Typewriter_KeyPress;
        }

        private void Typewriter_KeyPress(object sender, KeyPressEventArgs e)
        {
            CurrentPressedKey += e.KeyChar;
            if (CurrentPressedKey.Length > 5)
            {
                CurrentPressedKey = CurrentPressedKey.Substring(1, 5);
            }
        }

        private string CurrentPressedKey = string.Empty;

        public override void Update(TimeSpan elapsedTimeSpan)
        {
        }

        public override void Draw(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0,0,100,50));
            e.Graphics.DrawString(CurrentPressedKey + "", new Font(FontFamily.GenericMonospace, 20), new SolidBrush(Color.Aqua), new PointF(10, 10));
        }
    }
}
