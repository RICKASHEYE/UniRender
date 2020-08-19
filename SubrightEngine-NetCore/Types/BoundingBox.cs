using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SubrightEngine.Types
{
    public class BoundingBox
    {
        [DataMember]
        public Vector3 Min;

        [DataMember]
        public Vector3 Max;

        public BoundingBox(Vector3 min, Vector3 max)
        {
            this.Min = min;
            this.Max = max;
        }

        public ContainmentType Contains(BoundingBox box)
        {
            //test if all corner is in the same side of a face by just checking min and max
            if (box.Max.x < Min.x
                || box.Min.x > Max.x
                || box.Max.y < Min.y
                || box.Min.y > Max.y
                || box.Max.z < Min.z
                || box.Min.z > Max.z)
                return ContainmentType.Disjoint;


            if (box.Min.x >= Min.x
                && box.Max.x <= Max.x
                && box.Min.y >= Min.y
                && box.Max.y <= Max.y
                && box.Min.z >= Min.z
                && box.Max.z <= Max.z)
                return ContainmentType.Contains;

            return ContainmentType.Intersects;
        }

        public void Contains(ref BoundingBox box, out ContainmentType result)
        {
            result = Contains(box);
        }

        public ContainmentType Contains(Vector3 point)
        {
            ContainmentType result;
            this.Contains(ref point, out result);
            return result;
        }

        public void Contains(ref Vector3 point, out ContainmentType result)
        {
            //first we get if point is out of box
            if (point.x < this.Min.x
                || point.x > this.Max.x
                || point.y < this.Min.y
                || point.y > this.Max.y
                || point.z < this.Min.z
                || point.z > this.Max.z)
            {
                result = ContainmentType.Disjoint;
            }
            else
            {
                result = ContainmentType.Contains;
            }
        }

        private static readonly Vector3 MaxVector3 = new Vector3(float.MaxValue);
        private static readonly Vector3 MinVector3 = new Vector3(float.MinValue);

        public static BoundingBox CreateFromPoints(Vector3[] points, int index = 0, int count = -1)
        {
            if (points == null || points.Length == 0)
                throw new ArgumentException();

            if (count == -1)
                count = points.Length;

            var minVec = MaxVector3;
            var maxVec = MinVector3;
            for (int i = index; i < count; i++)
            {
                minVec.x = (minVec.x < points[i].x) ? minVec.x : points[i].x;
                minVec.y = (minVec.y < points[i].y) ? minVec.y : points[i].y;
                minVec.z = (minVec.z < points[i].z) ? minVec.z : points[i].z;

                maxVec.x = (maxVec.x > points[i].x) ? maxVec.x : points[i].x;
                maxVec.y = (maxVec.y > points[i].y) ? maxVec.y : points[i].y;
                maxVec.z = (maxVec.z > points[i].z) ? maxVec.z : points[i].z;
            }

            return new BoundingBox(minVec, maxVec);
        }

        public static BoundingBox CreateFromPoints(List<Vector3> points, int index = 0, int count = -1)
        {
            if (points == null || points.Count == 0)
                throw new ArgumentException();

            if (count == -1)
                count = points.Count;

            var minVec = MaxVector3;
            var maxVec = MinVector3;
            for (int i = index; i < count; i++)
            {
                minVec.x = (minVec.x < points[i].x) ? minVec.x : points[i].x;
                minVec.y = (minVec.y < points[i].y) ? minVec.y : points[i].y;
                minVec.z = (minVec.z < points[i].z) ? minVec.z : points[i].z;

                maxVec.x = (maxVec.x > points[i].x) ? maxVec.x : points[i].x;
                maxVec.y = (maxVec.y > points[i].y) ? maxVec.y : points[i].y;
                maxVec.z = (maxVec.z > points[i].z) ? maxVec.z : points[i].z;
            }

            return new BoundingBox(minVec, maxVec);
        }

        public static BoundingBox CreateFromPoints(IEnumerable<Vector3> points)
        {
            if (points == null)
                throw new ArgumentNullException();

            var empty = true;
            var minVec = MaxVector3;
            var maxVec = MinVector3;
            foreach (var ptVector in points)
            {
                minVec.x = (minVec.x < ptVector.x) ? minVec.x : ptVector.x;
                minVec.y = (minVec.y < ptVector.y) ? minVec.y : ptVector.y;
                minVec.z = (minVec.z < ptVector.z) ? minVec.z : ptVector.z;

                maxVec.x = (maxVec.x > ptVector.x) ? maxVec.x : ptVector.x;
                maxVec.y = (maxVec.y > ptVector.y) ? maxVec.y : ptVector.y;
                maxVec.z = (maxVec.z > ptVector.z) ? maxVec.z : ptVector.z;

                empty = false;
            }
            if (empty)
                throw new ArgumentException();

            return new BoundingBox(minVec, maxVec);
        }

        public static BoundingBox CreateMerged(BoundingBox original, BoundingBox additional)
        {
            BoundingBox result;
            CreateMerged(ref original, ref additional, out result);
            return result;
        }

        public static void CreateMerged(ref BoundingBox original, ref BoundingBox additional, out BoundingBox result)
        {
            result = new BoundingBox(Vector3.zero, Vector3.zero);
            result.Min.x = Math.Min(original.Min.x, additional.Min.x);
            result.Min.y = Math.Min(original.Min.y, additional.Min.y);
            result.Min.z = Math.Min(original.Min.z, additional.Min.z);
            result.Max.x = Math.Max(original.Max.x, additional.Max.x);
            result.Max.y = Math.Max(original.Max.y, additional.Max.y);
            result.Max.z = Math.Max(original.Max.z, additional.Max.z);
        }

        public bool Equals(BoundingBox other)
        {
            return (this.Min == other.Min) && (this.Max == other.Max);
        }

        public override bool Equals(object obj)
        {
            return (obj is BoundingBox) ? this.Equals((BoundingBox)obj) : false;
        }

        public Vector3[] GetCorners()
        {
            return new Vector3[] {
                new Vector3(this.Min.x, this.Max.y, this.Max.z),
                new Vector3(this.Max.x, this.Max.y, this.Max.z),
                new Vector3(this.Max.x, this.Min.y, this.Max.z),
                new Vector3(this.Min.x, this.Min.y, this.Max.z),
                new Vector3(this.Min.x, this.Max.y, this.Min.z),
                new Vector3(this.Max.x, this.Max.y, this.Min.z),
                new Vector3(this.Max.x, this.Min.y, this.Min.z),
                new Vector3(this.Min.x, this.Min.y, this.Min.z)
            };
        }

        public void GetCorners(Vector3[] corners)
        {
            if (corners == null)
            {
                throw new ArgumentNullException("corners");
            }
            if (corners.Length < 8)
            {
                throw new ArgumentOutOfRangeException("corners", "Not Enought Corners");
            }
            corners[0].x = this.Min.x;
            corners[0].y = this.Max.y;
            corners[0].z = this.Max.z;
            corners[1].x = this.Max.x;
            corners[1].y = this.Max.y;
            corners[1].z = this.Max.z;
            corners[2].x = this.Max.x;
            corners[2].y = this.Min.y;
            corners[2].z = this.Max.z;
            corners[3].x = this.Min.x;
            corners[3].y = this.Min.y;
            corners[3].z = this.Max.z;
            corners[4].x = this.Min.x;
            corners[4].y = this.Max.y;
            corners[4].z = this.Min.z;
            corners[5].x = this.Max.x;
            corners[5].y = this.Max.y;
            corners[5].z = this.Min.z;
            corners[6].x = this.Max.x;
            corners[6].y = this.Min.y;
            corners[6].z = this.Min.z;
            corners[7].x = this.Min.x;
            corners[7].y = this.Min.y;
            corners[7].z = this.Min.z;
        }

        public override int GetHashCode()
        {
            return this.Min.GetHashCode() + this.Max.GetHashCode();
        }

        public bool Intersects(BoundingBox box)
        {
            bool result;
            Intersects(ref box, out result);
            return result;
        }

        public void Intersects(ref BoundingBox box, out bool result)
        {
            if ((this.Max.x >= box.Min.x) && (this.Min.x <= box.Max.x))
            {
                if ((this.Max.y < box.Min.y) || (this.Min.y > box.Max.y))
                {
                    result = false;
                    return;
                }

                result = (this.Max.z >= box.Min.z) && (this.Min.z <= box.Max.z);
                return;
            }

            result = false;
            return;
        }

        public Nullable<float> Intersects(Ray ray)
        {
            return ray.Intersects(this);
        }

        public void Intersects(ref Ray ray, out Nullable<float> result)
        {
            result = Intersects(ray);
        }

        public static bool operator ==(BoundingBox a, BoundingBox b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(BoundingBox a, BoundingBox b)
        {
            return !a.Equals(b);
        }

        public override string ToString()
        {
            return "{{Min:" + this.Min.ToString() + " Max:" + this.Max.ToString() + "}}";
        }

        /// <summary>
        /// Deconstruction method for <see cref="BoundingBox"/>.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public void Deconstruct(out Vector3 min, out Vector3 max)
        {
            min = Min;
            max = Max;
        }
    }
}
