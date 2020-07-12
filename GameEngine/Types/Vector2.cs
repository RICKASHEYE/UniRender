using SharpDX;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SubrightEngine.Types
{
    public struct Vector2
    {
        [DataMember]
        public float x, y;

        public static readonly Vector2 zero = new Vector2(0, 0);
        public static readonly Vector2 down = new Vector2(0, -1);
        public static readonly Vector2 left = new Vector2(-1, 0);
        public static readonly Vector2 negativeInfinity = new Vector2(int.MinValue, int.MinValue);
        public static readonly Vector2 one = new Vector2(1, 1);
        public static readonly Vector2 positiveInfinity = new Vector2(int.MaxValue, int.MaxValue);
        public static readonly Vector2 right = new Vector2(1, 0);
        public static readonly Vector2 up = new Vector2(0, 1);

        public Vector2(float xValue, float yValue)
        {
            x = xValue;
            y = yValue;
        }

        public Vector2(float value)
        {
            x = value;
            y = value;
        }

        public Vector2(Vector2 value)
        {
            x = value.x;
            y = value.y;
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

        public static Vector2 Add(Vector2 value1, Vector2 value2)
        {
            value1.x += value2.y;
            value1.y += value2.y;
            return value1;
        }

        public static void Add(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.x = value1.x + value2.y;
            result.y = value1.x + value2.y;
        }

        public void Ceiling()
        {
            x = (float)Math.Ceiling(x);
            y = (float)Math.Ceiling(y);
        }

        public static Vector2 Ceiling(Vector2 value)
        {
            value.x = (float)Math.Ceiling(value.x);
            value.y = (float)Math.Ceiling(value.y);
            return value;
        }

        public static void Ceiling(ref Vector2 value, out Vector2 result)
        {
            result.x = (float)Math.Ceiling(value.x);
            result.y = (float)Math.Ceiling(value.y);
        }

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

        public static void Dot(ref Vector2 value1, ref Vector2 value2, out float result)
        {
            result = value1.x * value2.x + value1.y * value2.y;
        }

        public bool Equals(Vector2 other)
        {
            return x == other.x && y == other.y;
        }

        public void Floor()
        {
            x = (float)Math.Floor(x);
            y = (float)Math.Floor(y);
        }

        public static Vector2 Floor(Vector2 value)
        {
            value.x = (float)Math.Floor(value.x);
            value.y = (float)Math.Floor(value.y);
            return value;
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

        public static Vector2 Rotate(Vector2 v, float degrees)
        {
            double sin = Math.Sin(degrees * Mathf.Deg2Rad);
            double cos = Math.Cos(degrees * Mathf.Deg2Rad);

            double tx = v.x;
            double ty = v.y;
            v.x = (float)((cos * tx) - (sin * ty));
            v.y = (float)((sin * tx) + (cos * ty));
            return v;
        }
    }
}
