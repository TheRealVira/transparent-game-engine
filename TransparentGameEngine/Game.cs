using System;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace TransparentGameEngine
{
    public class Game: Form
    {
        private TransparentGameComponent _aGameComponent;
        private DateTime _lastTimeStamp;
        private readonly Timer _timer;
        private IWin32Window _owner;

        public TransparentGameComponent GetGameComponent() => _aGameComponent;

        public Game(TransparentGameComponent component, IWin32Window owner)
        {
            _owner = owner;
            LoadGameComponent(component);
            
            _timer = new Timer {Interval = 20, Enabled = false};
            _timer.Tick += Timer_Tick;
            Closing += Game_Closing;
            
            Capture = false;
        }

        public void LoadGameComponent(TransparentGameComponent component)
        {
            _aGameComponent?.Close();
            _aGameComponent = component;
        }

        private void Game_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Pause();
        }
        
        public void Run()
        {
            this.KickStart(_owner);
            Resume();
        }

        public void Resume()
        {
            _timer.Start();
            _lastTimeStamp = DateTime.Now;
        }

        public void Pause()
        {
            _timer.Stop();
        }

        public void SetUpdateInterval(int interval)
        {
            _timer.Interval = interval;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!_aGameComponent.Visible) _aGameComponent.Show();

            var elapsedTime = DateTime.Now - _lastTimeStamp;

            _aGameComponent.Update(elapsedTime);

            _aGameComponent.Invalidate();
            
            _aGameComponent.Update();

            _lastTimeStamp = DateTime.Now;
        }

        public static Game GenerateGame<T>(IWin32Window owner) where T : TransparentGameComponent, new() =>
            new Game(TransparentGameComponent.Generate<T>(), owner);
    }
}

