using System;
using System.Drawing;

namespace TransparentGameEngine
{
    public struct Vector2
    {
        public float X { get; set; }
        public float Y { get; set; }

        public static readonly Vector2 Zero = new Vector2(0, 0);
        public static readonly Vector2 One = new Vector2(1, 1);

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj) =>
            obj is Vector2 vector2 && vector2.X.Equals(X) && vector2.Y.Equals(Y);

        public static Vector2 operator +(Vector2 v1, Vector2 v2)=>new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        public static Vector2 operator -(Vector2 v1, Vector2 v2)=>new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        public static Vector2 operator *(Vector2 v1, Vector2 v2)=>new Vector2(v1.X * v2.X, v1.Y * v2.Y);
        public static Vector2 operator /(Vector2 v1, Vector2 v2)=>new Vector2(v1.X / v2.X, v1.Y / v2.Y);
        public static bool operator < (Vector2 v1, Vector2 v2) => v1.X < v2.X && v1.Y < v2.Y;
        public static bool operator > (Vector2 v1, Vector2 v2) => v1.X > v2.X && v1.Y > v2.Y;

        public static Vector2 operator +(Vector2 v1, float velocity) => new Vector2(v1.X + velocity, v1.Y + velocity);
        public static Vector2 operator -(Vector2 v1, float velocity) => new Vector2(v1.X - velocity, v1.Y -velocity);
        public static Vector2 operator *(Vector2 v1, float velocity) => new Vector2(v1.X * velocity, v1.Y * velocity);
        public static Vector2 operator /(Vector2 v1, float velocity) => new Vector2(v1.X / velocity, v1.Y / velocity);

        public static bool operator <(Vector2 v1, float velocity) => v1.X < velocity && v1.Y < velocity;
        public static bool operator >(Vector2 v1, float velocity) => v1.X > velocity && v1.Y > velocity;

        public double Length() => Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
        public Point ToPoint()=>new Point((int)X, (int)Y);

        public override string ToString()
        {
            return "{"+X+";"+Y+"}";
        }
    }
}
