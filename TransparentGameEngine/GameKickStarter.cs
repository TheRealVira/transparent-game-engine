using System;
using System.Windows.Forms;

namespace TransparentGameEngine
{
    internal static class GameKickStarter
    {
        [STAThread]
        public static void KickStart(this Game game, IWin32Window owner)
        {
            Application.EnableVisualStyles();
            game?.GetGameComponent().Show(owner);
        }
    }
}
