using System;
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
            return (float)Lerp((double)firstFloat, (double)secondFloat, (double)by);
        }

        public static double Lerp(double firstFloat, double secondFloat, double by)
        {
            var m1Float = Math.Abs(firstFloat);
            var m2Float = Math.Abs(secondFloat);

            var difference = m1Float > m2Float ? m1Float - m2Float : m2Float - m1Float;

            var toRet = difference - difference * by;
            return firstFloat + toRet*(firstFloat<secondFloat?1:-1);
        }

        public static Point Lerp(Point firstVector, Point secondVector, float by)
        {
            var retX = Lerp(firstVector.X, secondVector.X, by);
            var retY = Lerp(firstVector.Y, secondVector.Y, by);
            return new Point((int)Math.Round(retX), (int)Math.Round(retY));
        }

        public static Vector2 ToVector2(this Point point)=>new Vector2(point.X, point.Y);
    }
}
