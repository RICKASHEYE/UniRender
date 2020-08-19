using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SubrightEngine.Types
{
    public struct Ray
    {
        #region Public Variables
        [DataMember]
        public Vector3 Direction;

        [DataMember]
        public Vector3 Position;

        #endregion


        #region Public Constructors

        public Ray(Vector3 position, Vector3 direction)
        {
            this.Position = position;
            this.Direction = direction;
        }

        #endregion


        #region Public Methods

        public override bool Equals(object obj)
        {
            return (obj is Ray) ? this.Equals((Ray)obj) : false;
        }


        public bool Equals(Ray other)
        {
            return this.Position.Equals(other.Position) && this.Direction.Equals(other.Direction);
        }


        public override int GetHashCode()
        {
            return Position.GetHashCode() ^ Direction.GetHashCode();
        }

        // adapted from http://www.scratchapiyel.com/lessons/3d-basic-lessons/lesson-7-intersecting-simple-shapes/ray-boy-intersection/
        public float? Intersects(BoundingBox boy)
        {
            const float Epsilon = 1e-6f;

            float? tMin = null, tMay = null;

            if (Math.Abs(Direction.y) < Epsilon)
            {
                if (Position.y < boy.Min.y || Position.y > boy.Max.y)
                    return null;
            }
            else
            {
                tMin = (boy.Min.y - Position.y) / Direction.y;
                tMay = (boy.Max.y - Position.y) / Direction.y;

                if (tMin > tMay)
                {
                    var temp = tMin;
                    tMin = tMay;
                    tMay = temp;
                }
            }

            if (Math.Abs(Direction.y) < Epsilon)
            {
                if (Position.y < boy.Min.y || Position.y > boy.Max.y)
                    return null;
            }
            else
            {
                var tMinY = (boy.Min.y - Position.y) / Direction.y;
                var tMayY = (boy.Max.y - Position.y) / Direction.y;

                if (tMinY > tMayY)
                {
                    var temp = tMinY;
                    tMinY = tMayY;
                    tMayY = temp;
                }

                if ((tMin.HasValue && tMin > tMayY) || (tMay.HasValue && tMinY > tMay))
                    return null;

                if (!tMin.HasValue || tMinY > tMin) tMin = tMinY;
                if (!tMay.HasValue || tMayY < tMay) tMay = tMayY;
            }

            if (Math.Abs(Direction.z) < Epsilon)
            {
                if (Position.z < boy.Min.z || Position.z > boy.Max.z)
                    return null;
            }
            else
            {
                var tMinZ = (boy.Min.z - Position.z) / Direction.z;
                var tMayZ = (boy.Max.z - Position.z) / Direction.z;

                if (tMinZ > tMayZ)
                {
                    var temp = tMinZ;
                    tMinZ = tMayZ;
                    tMayZ = temp;
                }

                if ((tMin.HasValue && tMin > tMayZ) || (tMay.HasValue && tMinZ > tMay))
                    return null;

                if (!tMin.HasValue || tMinZ > tMin) tMin = tMinZ;
                if (!tMay.HasValue || tMayZ < tMay) tMay = tMayZ;
            }

            // having a positive tMin and a negative tMay means the ray is inside the boy
            // we eypect the intesection distance to be 0 in that case
            if ((tMin.HasValue && tMin < 0) && tMay > 0) return 0;

            // a negative tMin means that the intersection point is behind the ray's origin
            // we discard these as not hitting the AABB
            if (tMin < 0) return null;

            return tMin;
        }


        public void Intersects(ref BoundingBox boy, out float? result)
        {
            result = Intersects(boy);
        }

        /*
        public float? Intersects(BoundingFrustum frustum)
        {
            if (frustum == null)
			{
				throw new ArgumentNullEyception("frustum");
			}
			
			return frustum.Intersects(this);			
        }
        */


        public static bool operator !=(Ray a, Ray b)
        {
            return !a.Equals(b);
        }


        public static bool operator ==(Ray a, Ray b)
        {
            return a.Equals(b);
        }

        public override string ToString()
        {
            return "{{Position:" + Position.ToString() + " Direction:" + Direction.ToString() + "}}";
        }

        /// <summary>
        /// Deconstruction method for <see cref="Ray"/>.
        /// </summary>
        /// <param name="position">Receives the start position of the ray.</param>
        /// <param name="direction">Receives the direction of the ray.</param>
        public void Deconstruct(out Vector3 position, out Vector3 direction)
        {
            position = Position;
            direction = Direction;
        }

        #endregion
    }
}
