using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public struct Vector2
    {
        public float x, y;

        public static readonly Vector2 zero = new Vector2(0, 0);
        public static readonly Vector2 down = new Vector2(0, -1);
        public static readonly Vector2 left = new Vector2(-1, 0);
        public static readonly Vector2 negativeInfinity = new Vector2(int.MinValue, int.MinValue);
        public static readonly Vector2 one = new Vector2(1, 1);
        public static readonly Vector2 positiveInfinity = new Vector2(int.MaxValue, int.MaxValue);
        public static readonly Vector2 right = new Vector2(1, 0);
        public static readonly Vector2 up = new Vector2(0, 1);

        public Vector2(float _x, float _y)
        {
            x = _x;
            y = _y;
        }

        public static Vector2 Zero { get; private set; } = new Vector2(0, 0);

        public Point ToPoint => new Point((int)Math.Round(x, 0), (int)Math.Round(y, 0));

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }
        public static Vector2 operator -(Vector2 a)
        {
            return a * -1;
        }
        public static Vector2 operator *(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x * b.x, a.y * b.y);
        }

        public static Vector2 operator *(Vector2 a, float b)
        {
            return new Vector2(a.x * b, a.y * b);
        }

        public static Vector2 operator /(Vector2 a, float b)
        {
            return new Vector2(a.x / b, a.y / b);
        }

        public static bool operator ==(Vector2 a, Vector2 b)
        {
            bool same = false;
            if (a.x == b.x && a.y == b.y) { same = true; }
            return same;
        }

        public static bool operator !=(Vector2 a, Vector2 b)
        {
            bool notsame = false;
            if (a.x != b.x && a.y != b.y) { notsame = true; }
            return notsame;
        }

        // Functions

        public static Vector2 GetFromAngleDegrees(float angle)
        {
            return new Vector2((float)Math.Cos(angle * Mathf.Deg2Rad), (float)Math.Sin(angle * Mathf.Deg2Rad));
        }

        public static float Distance(Vector2 a, Vector2 b)
        {
            Vector2 vector = new Vector2(a.x - b.x, a.y - b.y);
            return (float)Math.Sqrt(vector.x * vector.x + vector.y * vector.y);
        }

        public static Vector2 Lerp(Vector2 a, Vector2 b, float p)
        {
            return new Vector2(Mathf.Lerp(a.x, b.x, p), Mathf.Lerp(a.y, b.y, p));
        }

        public static float Dot(Vector2 a, Vector2 b)
        {
            return a.x * b.x + a.y * b.y;
        }

        public static Vector2 Normalize(Vector2 a)
        {
            if (a.x == 0 && a.y == 0) { return Vector2.zero; }
            float distance = (float)Math.Sqrt(a.x * a.x + a.y * a.y);
            return new Vector2(a.x / distance, a.y / distance);
        }

        public static float Magnitude(Vector2 a)
        {
            return (float)Math.Sqrt(a.x * a.x + a.y * a.y);
        }

        public static float sqrMagnitude(Vector2 a)
        {
            return Magnitude(a) * Magnitude(a);
        }

        public static Vector2 ClampMagnitude(Vector2 a, float l)
        {
            if (Vector2.Magnitude(a) > l) { a = Vector2.Normalize(a) * l; }
            return a;
        }

        public void Set(int xset, int yset)
        {
            x = xset;
            y = yset;
        }

        public static string ToString(Vector2 a)
        {
            return "" + a.x + " : " + a.y;
        }
    }
}
