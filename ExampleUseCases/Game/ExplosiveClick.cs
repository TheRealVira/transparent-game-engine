using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using TransparentGameEngine;

namespace ExampleUseCases.Game
{
    class ExplosiveClick:TransparentGameComponent
    {
        private List<Explotion>Explotions = new List<Explotion>();
        private Random _rand;

        public ExplosiveClick()
        {
            Hook.GlobalEvents().MouseClick += ExplosiveClick_MouseClick;
            _rand = new Random(DateTime.Now.Millisecond);
        }

        private void ExplosiveClick_MouseClick(object sender, MouseEventArgs e)
        {
            Explotions.Add(new Explotion(e.Location, _rand));
        }

        public override void Update(TimeSpan elapsedTimeSpan)
        {
            Explotions.ForEach(x=>x.Update(elapsedTimeSpan));

            Explotions.RemoveAll(x => x.FinishedExplotion);
        }

        public override void Draw(object sender, PaintEventArgs e)
        {
            Explotions.ForEach(x=>x.Draw(sender, e));
        }

        private class Explotion
        {
            private Particle[] Particles = new Particle[10];

            public Explotion(Point location, Random rand)
            {
                _location = location;

                for (var i = 0; i < Particles.Length; i++)
                {
                    var randX = ((float)rand.NextDouble() - .5f)*2;
                    var randY = ((float)rand.NextDouble() - .5f)*2;
                    Particles[i]=new Particle(location.ToVector2(), new Size(10, 10), Color.DarkGray,
                        new Vector2(randX, randY));
                }
            }


            private Point _location;
            public bool FinishedExplotion = false;

            public void Update(TimeSpan elapsedTimeSpan)
            {
                if (Particles.AsParallel().All(x=>x.Force.Equals(Vector2.Zero)))
                {
                    FinishedExplotion = true;
                    return;
                }

                foreach (var particle1 in Particles.AsParallel())
                {
                    particle1.Update(elapsedTimeSpan);
                }
            }

            public void Draw(object sender, PaintEventArgs e)
            {
                foreach (var particle1 in Particles)
                {
                    e.Graphics.DrawLine(new Pen(Color.Red, 3), _location, particle1.Location.ToPoint());
                    particle1.Draw(this, e);
                }
            }
        }
    }
}
