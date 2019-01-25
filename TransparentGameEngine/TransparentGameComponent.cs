using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TransparentGameEngine
{
    public abstract class TransparentGameComponent : Form
    {
        protected TransparentGameComponent()
        {
            TopMost = true;
            ShowInTaskbar = false;

            Application.EnableVisualStyles();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            BackColor = Color.LightGreen;
            TransparencyKey = Color.LightGreen;

            Paint += Draw;
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        const int GWL_EXSTYLE = -20;
        const int WS_EX_LAYERED = 0x80000;
        const int WS_EX_TRANSPARENT = 0x20;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var style = GetWindowLong(this.Handle, GWL_EXSTYLE);
            SetWindowLong(this.Handle,GWL_EXSTYLE , style | WS_EX_LAYERED | WS_EX_TRANSPARENT);
        }

        public sealed override Color BackColor
        {
            get => base.BackColor;
            set => base.BackColor = value;
        }

        public abstract void Update(TimeSpan elapsedTimeSpan);
        public abstract void Draw(object sender, PaintEventArgs e);

        public static TransparentGameComponent Generate<T>() where T : TransparentGameComponent, new() => new T();

        public static TransparentGameComponent Generate(Type componentType)
        {
            return !componentType.IsAbstract && typeof(TransparentGameComponent).IsAssignableFrom(componentType)
                ? (TransparentGameComponent) Activator.CreateInstance(componentType)
                : null;
        }
    }
}