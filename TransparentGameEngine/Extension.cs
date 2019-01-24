using System;
using System.Diagnostics;
using System.Drawing;

namespace TransparentGameEngine
{
    public static class Extension
    {
        public static int Clamp(this int num, int max, int min)
        {
            return Math.Min(max, Math.Max(num, min));
        }

        public static float Lerp(float firstFloat, float secondFloat, float by)
        {
            return firstFloat + (secondFloat - firstFloat) * by;
        }

        public static Point Lerp(Point firstVector, Point secondVector, float by)
        {
            float retX = Lerp(firstVector.X, secondVector.X, by);
            float retY = Lerp(firstVector.Y, secondVector.Y, by);
            return new Point((int)retX, (int)retY);
        }

        public static IntPtr GetHandle()=> Process.GetCurrentProcess().MainWindowHandle;
    }
}
