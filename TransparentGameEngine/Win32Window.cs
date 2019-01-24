using System;
using System.Windows.Forms;

namespace TransparentGameEngine
{
    public class Win32Window : System.Windows.Forms.IWin32Window
    {
        private readonly IntPtr _hWnd;

        public Win32Window(IntPtr hWnd)
        {
            _hWnd = hWnd;
        }
        
        IntPtr IWin32Window.Handle => _hWnd;
    }
}
