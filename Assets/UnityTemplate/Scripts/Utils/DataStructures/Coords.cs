using System;
using UnityEngine;

/// <summary>
    /// Used instead of vector2int for JSON loading puposes
    /// </summary>
    [Serializable]
    public class Coords
    {
        public int x;
        public int z;

        public Coords(int x, int z)
        {
            this.x = x;
            this.z = z;
        }

        public Coords(Vector2Int v2i)
        {
            x = v2i.x;
            z = v2i.y;
        }

        public Coords() { }
        
        public int manhattanDistance => Mathf.Abs(x) + Mathf.Abs(z);
        
        public Coords Copy()
        {
            return new Coords(this.x, this.z);
        }

        /// <summary>
        /// Returns a Vector2 that behaves similarly.
        /// Watch out as casting to V3 will result in the z value being at x instead.
        /// </summary>
        public Vector2Int ToVector2Int()
        {
            return new Vector2Int(x, z);
        }
        
        public static Coords operator+ (Coords c1, Coords c2)
        {
            return new Coords(c1.x + c2.x, c1.z + c2.z);
        }
        
        public static Coords operator- (Coords c1, Coords c2)
        {
            return new Coords(c1.x - c2.x, c1.z - c2.z);
        }
        
        public static Coords operator* (Coords c1, Coords c2)
        {
            return new Coords(c1.x * c2.x, c1.z * c2.z);
        }
        
        public static Coords operator* (int k, Coords c2)
        {
            return new Coords(k * c2.x, k * c2.z);
        }
        
        public static Coords operator* (Coords c1, int k)
        {
            return new Coords(c1.x * k, c1.z * k);
        }
        
        public static Vector2 operator* (float k, Coords c2)
        {
            return new Vector2(k * c2.x, k * c2.z);
        }
        
        public static Vector2 operator* (Coords c1, float k)
        {
            return new Vector2(c1.x * k, c1.z * k);
        }
        
        public static Vector2 operator* (Coords c1, Vector2 v2)
        {
            return new Vector2(c1.x * v2.x, c1.z * v2.y);
        }
        
        public static Coords operator/ (Coords c1, Coords c2)
        {
            return new Coords(c1.x / c2.x, c1.z / c2.z);
        }
        
        public static Coords operator/ (Coords c1, int k)
        {
            return new Coords(c1.x / k, c1.z / k);
        }
        
        public static Vector2 operator/ (Coords c1, float k)
        {
            return new Vector2(c1.x / k, c1.z / k);
        }
        
        public static Coords operator- (Coords c1)
        {
            return new Coords(-c1.x, -c1.z);
        }
        
        public static bool operator== (Coords c1, Coords c2)
        {
            if ((object)c1 == null)
                return (object)c2 == null;
    
            return c1.Equals(c2);
        }
    
        public static bool operator!= (Coords c1, Coords c2)
        {
            return !(c1 == c2);
        }
    
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
    
            Coords c2 = (Coords) obj;   
            return this.x == c2.x && this.z == c2.z;
        }
    
        // to be improved
        public override int GetHashCode()
        {
            var hash = 23;
            hash = hash * 31 + x.GetHashCode();
            hash = hash * 31 + z.GetHashCode();
            return hash;
        }
    
        public override string ToString()
        {
            return $"({x}, {z})";
        }

    }