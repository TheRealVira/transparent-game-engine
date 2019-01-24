using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TransparentGameEngine;

namespace ExampleUseCases.Game
{
    class FireWorkz:TransparentGameComponent
    {
        public FireWorkz()
        {
            foreach (var file in Directory.GetFiles("./Content/Fireworks/"))
            {
                var firework = Image.FromFile(file);
                ImageAnimator.Animate(firework, null);

                Fireworks.Add(firework);
            }

            _random = new Random(DateTime.Now.Millisecond);

            for (var i = 0; i < FireworkPositions.Length; i++)
            {
                FireworkPositions[i] = GenerateFirework();
            }

            ResetTimer();
        }

        private (Point position, int indi) GenerateFirework()
        {
            return (new Point(_random.Next(0, Width), _random.Next(0, Height)),
                _random.Next(0, FireworkPositions.Length));
        }

        private void ResetTimer()
        {
            CurrentWaitingMilliseconds = _random.Next(MinWaitingMilliseconds, MaxWaitingMilliseconds);
        }

        private List<Image>Fireworks=new List<Image>();
        private (Point position, int indi)[]FireworkPositions = new (Point position, int indi)[3];
        private Random _random;

        private const int MaxWaitingMilliseconds = 5000;
        private const int MinWaitingMilliseconds = 2500;

        private int CurrentWaitingMilliseconds;

        public override void Update(TimeSpan elapsedTimeSpan)
        {
            ImageAnimator.UpdateFrames();
            CurrentWaitingMilliseconds -= elapsedTimeSpan.Milliseconds;

            if (CurrentWaitingMilliseconds > 0) return;
            ResetTimer();
            FireworkPositions[_random.Next(0, FireworkPositions.Length)] = GenerateFirework();
        }

        public override void Draw(object sender, PaintEventArgs e)
        {
            foreach (var fireworkPosition in FireworkPositions)
            {
                e.Graphics.DrawImage(Fireworks[fireworkPosition.indi], fireworkPosition.position);
            }
        }
    }
}
