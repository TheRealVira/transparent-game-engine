using System;
using System.Diagnostics;
using System.Windows.Forms;
using ExampleUseCases.Game;
using TransparentGameEngine;

namespace ExampleProjectConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;
            var myGame = Game.GenerateGame<FollowingCircle>(new Win32Window(handle));

            Console.WriteLine("Starting the game..");

            myGame.Run();

            Console.WriteLine("Please press ENTER to end the game...");
            Console.ReadLine();
        }
    }
}
