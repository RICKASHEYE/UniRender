using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubrightEngine.Types
{
    public struct Vector3
    {
        public float x, y, z;

        public static readonly Vector3 zero = new Vector3(0, 0, 0);

        public Vector3(float _x, float _y, float _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }

        public static Vector3 Zero { get; private set; } = new Vector3(0, 0, 0);

        public Vector2 ToVector2 => new Vector2((int)Math.Round(x, 0), (int)Math.Round(y, 0));

        public static Vector3 operator -(Vector3 a)
        {
            return a * -1;
        }

        public static Vector3 operator *(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public static Vector3 operator *(Vector3 a, float b)
        {
            return new Vector3(a.x * b, a.y * b, a.z * b);
        }
        public static Vector3 operator /(Vector3 a, float b)
        {
            return new Vector3(a.x / b, a.y / b, a.z / b);
        }

        public static bool operator ==(Vector3 a, Vector3 b)
        {
            bool same = false;
            if (a.x == b.x && a.y == b.y && a.z == b.z) { same = true; }
            return same;
        }

        public static bool operator !=(Vector3 a, Vector3 b)
        {
            bool notsame = false;
            if (a.x != b.x && a.y != b.y && a.z != b.z) { notsame = true; }
            return notsame;
        }

        // Functions

        public static Vector3 GetFromAngleDegrees(float angle)
        {
            return new Vector3((float)Math.Cos(angle * Mathf.Deg2Rad), (float)Math.Sin(angle * Mathf.Deg2Rad), (float)Math.Sin(angle * Mathf.Deg2Rad));
        }

        public static float Distance(Vector3 a, Vector3 b)
        {
            Vector3 vector = new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
            return (float)Math.Sqrt(vector.x * vector.x + vector.y * vector.y);
        }

        public static Vector3 Lerp(Vector3 a, Vector3 b, float p)
        {
            return new Vector3(Mathf.Lerp(a.x, b.x, p), Mathf.Lerp(a.y, b.y, p), Mathf.Lerp(a.z, b.z, p));
        }

        public static float Dot(Vector3 a, Vector3 b)
        {
            return a.x * b.x + a.y * b.y;
        }

        public static Vector3 Normalize(Vector3 a)
        {
            if (a.x == 0 && a.y == 0) { return Vector3.zero; }
            float distance = (float)Math.Sqrt(a.x * a.x + a.y * a.y);
            return new Vector3(a.x / distance, a.y / distance, a.z / distance);
        }

        public static float Magnitude(Vector3 a)
        {
            return (float)Math.Sqrt(a.x * a.x + a.y * a.y);
        }

        public static Vector3 ClampMagnitude(Vector3 a, float l)
        {
            if (Vector3.Magnitude(a) > l) { a = Vector3.Normalize(a) * l; }
            return a;
        }

    }
}
