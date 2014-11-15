using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mario3
{
    class RectangleF
    {
        /// <summary>
        /// The x co-ordinate of the rectangle
        /// </summary>
        public float X { get; private set; }
        /// <summary>
        /// The y co-ordinate of the rectangle
        /// </summary>
        public float Y { get; private set; }
        /// <summary>
        /// The width of the rectangle
        /// </summary>
        public float Width { get; private set; }
        /// <summary>
        /// The height of the rectangle
        /// </summary>
        public float Height { get; private set; }

        /// <summary>
        /// The x co-ordinate of the left side of the rectangle
        /// </summary>
        public float Left { get { return X; } }
        /// <summary>
        /// The y co-ordinate of the top side of the rectangle
        /// </summary>
        public float Top { get { return Y; } }
        /// <summary>
        /// The x co-ordinate of the right side of the rectangle
        /// </summary>
        public float Right { get { return X + Width; } }
        /// <summary>
        /// The y co-ordinate of the bottom side of the rectangle
        /// </summary>
        public float Bottom { get { return Y + Height; } }

        /// <summary>
        /// A rectangle that accepts floating point values
        /// </summary>
        public RectangleF()
        {
            
        }

        /// <summary>
        /// A rectangle that accepts floating point values
        /// </summary>
        /// <param name="x">The x co-ordinate of the rectangle</param>
        /// <param name="y">The y co-ordinate of the rectangle</param>
        /// <param name="width">The width of the rectangle</param>
        /// <param name="height">The height of the rectangle</param>
        public RectangleF(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Determines if two axis-aligned bounding boxes are intersecting
        /// </summary>
        /// <param name="otherRectangleF">The other rectangle to check</param>
        /// <returns>True if intersecting, else false</returns>
        public bool Intersects(RectangleF otherRectangleF)
        {
            return ((Right > otherRectangleF.Left && Left < otherRectangleF.Right) &&
                    (Bottom > otherRectangleF.Top && Top < otherRectangleF.Bottom));
        }

        /// <summary>
        /// Converts a RectangleF to a Rectangle
        /// </summary>
        /// <returns>The int-based Rectangle</returns>
        public Rectangle ToRectangle()
        {
            return new Rectangle((int)X, (int)Y, (int)Width, (int)Height);
        }
    }
}
