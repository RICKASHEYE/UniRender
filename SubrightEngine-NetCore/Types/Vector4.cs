using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SubrightEngine.Types
{
    public struct Vector4
    {
        private static readonly Vector4 Zero = new Vector4();
        private static readonly Vector4 One = new Vector4(1f, 1f, 1f, 1f);

        [DataMember]
        public float x,y,z,w;

        public Vector4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Vector4(Vector2 value, float z, float w)
        {
            this.x = value.x;
            this.y = value.y;
            this.z = z;
            this.w = w;
        }

        public Vector4(Vector3 value, float w)
        {
            this.x = value.x;
            this.y = value.y;
            this.z = value.z;
            this.w = w;
        }

        public Vector2 ToVector2 => new Vector2((int)Math.Round(x, 0), (int)Math.Round(y, 0));
        public Vector3 ToVector3 => new Vector3(x, y, z);

        public static Vector4 operator -(Vector4 a)
        {
            return a * -1;
        }

        public static Vector4 operator *(Vector4 a, Vector4 b)
        {
            return new Vector4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
        }

        public static Vector4 operator *(Vector4 a, float b)
        {
            return new Vector4(a.x * b, a.y * b, a.z * b, a.w * b);
        }
        public static Vector4 operator /(Vector4 a, float b)
        {
            return new Vector4(a.x / b, a.y / b, a.z / b, a.w / b);
        }

        public static bool operator ==(Vector4 a, Vector4 b)
        {
            bool same = false;
            if (a.x == b.x && a.y == b.y && a.z == b.z && a.w == b.w) { same = true; }
            return same;
        }

        public static bool operator !=(Vector4 a, Vector4 b)
        {
            bool notsame = false;
            if (a.x != b.x && a.y != b.y && a.z != b.z && a.w != b.w) { notsame = true; }
            return notsame;
        }

        // Functions

        public static Vector4 Add(Vector4 value1, Vector4 value2)
        {
            value1.x += value2.x;
            value1.y += value2.y;
            value1.z += value2.z;
            value1.w += value2.w;
            return value1;
        }

        public static void Add(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result.x = value1.x + value2.x;
            result.y = value2.y + value2.y;
            result.z = value1.z + value2.z;
            result.w = value1.w + value2.w;
        }

        public static float Distance(Vector4 a, Vector4 b)
        {
            Vector4 vector = new Vector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
            return (float)Math.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z + vector.w * vector.w);
        }

        public static Vector4 Lerp(Vector4 a, Vector4 b, float p)
        {
            return new Vector4(Mathf.Lerp(a.x, b.x, p), Mathf.Lerp(a.y, b.y, p), Mathf.Lerp(a.z, b.z, p), Mathf.Lerp(a.w, b.w, p));
        }

        public static float Dot(Vector4 a, Vector4 b)
        {
            return a.x * b.x + a.y * b.y + a.w * b.w;
        }

        public static void Dot(ref Vector4 value1, ref Vector4 value2, out float result)
        {
            result = value1.x * value2.x + value1.y * value2.y + value1.z * value2.z + value1.w * value2.w;
        }

        public static Vector4 Normalize(Vector4 a)
        {
            if (a.x == 0 && a.y == 0) { return Vector4.Zero; }
            float distance = (float)Math.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z + a.w * a.w);
            return new Vector4(a.x / distance, a.y / distance, a.z / distance, a.w / distance);
        }

        public static float Magnitude(Vector4 a)
        {
            return (float)Math.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z + a.w * a.w);
        }

        public static Vector4 ClampMagnitude(Vector4 a, float l)
        {
            if (Vector4.Magnitude(a) > l) { a = Vector4.Normalize(a) * l; }
            return a;
        }
    }
}
