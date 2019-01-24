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
            BackColor = Color.White;
            TransparencyKey = Color.White;
            var initialStyle = GetWindowLong(Handle, -20);
            SetWindowLong(Handle, -20, initialStyle | 0x80000 | 0x20);

            Paint += Draw;
        }

        public sealed override Color BackColor
        {
            get => base.BackColor;
            set => base.BackColor = value;
        }

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

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