using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Mario3
{
    static class ExtendedSpriteBatch
    {
        private static Texture2D WhiteTexture
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a 1px white texture. This method must be called prior to using any ExtendedSpriteBatch function
        /// </summary>
        /// <param name="spriteBatch"></param>
        public static void CreateWhiteTexture(this SpriteBatch spriteBatch)
        {
            WhiteTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            WhiteTexture.SetData(new[] { Color.White });
        }

        /// <summary>
        /// Draw a line between the two supplied points.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="start">Starting point.</param>
        /// <param name="end">End point.</param>
        /// <param name="colour">The draw colour.</param>
        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color colour)
        {
            float length = (end - start).Length();
            float rotation = (float)Math.Atan2(end.Y - start.Y, end.X - start.X);
            spriteBatch.Draw(WhiteTexture, start, null, colour, rotation, Vector2.Zero, new Vector2(length, 1), SpriteEffects.None, 0);
        }

        /// <summary>
        /// Draw the outline of a rectangle to the screen
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="rectangle">The rectangle to be drawn</param>
        /// <param name="colour">The draw color</param>
        public static void DrawRectangle(this SpriteBatch spriteBatch, Rectangle rectangle, Color colour)
        {
            spriteBatch.Draw(WhiteTexture, new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, 1), colour);
            spriteBatch.Draw(WhiteTexture, new Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, 1), colour);
            spriteBatch.Draw(WhiteTexture, new Rectangle(rectangle.Left, rectangle.Top, 1, rectangle.Height), colour);
            spriteBatch.Draw(WhiteTexture, new Rectangle(rectangle.Right, rectangle.Top, 1, rectangle.Height + 1), colour);
        }

        /// <summary>
        /// Draws a rectangle to the screen and fills it with the specified colour
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="rectangle">The rectangle to be drawn</param>
        /// <param name="colour">The fill colour</param>
        public static void FillRectangle(this SpriteBatch spriteBatch, Rectangle rectangle, Color colour)
        {
            spriteBatch.Draw(WhiteTexture, rectangle, colour);
        }
    }
}
