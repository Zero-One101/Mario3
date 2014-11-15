using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Mario3
{
    class Mario : GameObject
    {
        private readonly List<Keys> downKeys = new List<Keys>();
        private readonly List<Keys> upKeys = new List<Keys>();
        private const float gravity = 0.1f;
        private const float jumpStrength = 3.0f;
        private bool hasJumped;
        private SpriteFont font;

        public override void Initialise(Viewport viewport, EntityManager entityManager)
        {
            base.Initialise(viewport, entityManager);
            entityManager.KeyDown += entityManager_KeyDown;
            entityManager.KeyUp += entityManager_KeyUp;
            position = new Vector2(viewport.Width / 2, viewport.Height / 2);
            nextPosition = position;
            frameSize = new Point(16, 32);
            hitRect = new RectangleF(position.X, position.Y, frameSize.X, frameSize.Y);
            maxMoveSpeed = 2.0f;
            font = this.entityManager.ResourceManager.LoadSpriteFont(@"SpriteFonts\SpriteFont1");
        }

        void entityManager_KeyUp(object sender, KeyUpEventArgs e)
        {
            upKeys.Add(e.Key);
        }

        void entityManager_KeyDown(object sender, KeyDownEventArgs e)
        {
            downKeys.Add(e.Key);
        }

        public override void Update(GameTime gameTime)
        {
            ApplyGravity();
            CheckInput();
            hitRect = new RectangleF(position.X, position.Y, frameSize.X, frameSize.Y);
            Debug.Print("Mario has been updated");
        }

        private void ApplyGravity()
        {
            moveSpeed.Y += gravity;
            Debug.Print("Applied gravity: " + moveSpeed.Y);
        }

        private void CheckInput()
        {
            if (downKeys.Contains(Keys.Right))
            {
                moveSpeed.X = maxMoveSpeed;
            }
            if (downKeys.Contains(Keys.Left))
            {
                moveSpeed.X = -maxMoveSpeed;
            }
            if (downKeys.Contains(Keys.X))
            {
                if (!hasJumped)
                {
                    moveSpeed.Y -= jumpStrength;
                    hasJumped = true;
                }
            }
            if (downKeys.Contains(Keys.Down))
            {
                
            }

            downKeys.Clear();

            if (upKeys.Contains(Keys.Right))
            {
                moveSpeed.X -= maxMoveSpeed;
            }
            if (upKeys.Contains(Keys.Left))
            {
                moveSpeed.X += maxMoveSpeed;
            }

            upKeys.Clear();

            position += moveSpeed;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.FillRectangle(new Rectangle((int)position.X, (int)position.Y, frameSize.X, frameSize.Y), Color.Red);
            spriteBatch.DrawRectangle(hitRect.ToRectangle(), Color.Green);
            spriteBatch.DrawString(font, moveSpeed.Y.ToString(CultureInfo.InvariantCulture), new Vector2(50, 50), Color.Red);
            Debug.Print("Mario has been drawn");
        }

        public override void Collide(GameObject gameObject)
        {
            if (gameObject is Tile)
            {
                Debug.Print("Collided");
                Tile tile = (Tile)gameObject;
                if (tile.ColType == Tile.CollisionType.SOLID)
                {
                    ResolveCollision(tile.HitRect);
                }
            }
        }

        /// <summary>
        /// Resolves a collision by finding the shortest movement out of a hitbox and applies it
        /// </summary>
        /// <param name="otherCol">The hitbox of the other object</param>
        private void ResolveCollision(RectangleF otherCol)
        {
            float left = otherCol.Left - hitRect.Right;
            float right = otherCol.Right - hitRect.Left;
            float minXPush = Math.Abs(left) < Math.Abs(right) ? left : right;

            float top = otherCol.Top - hitRect.Bottom;
            float bottom = otherCol.Bottom - hitRect.Top;
            float minYPush = Math.Abs(top) < Math.Abs(bottom) ? top : bottom;

            if (Math.Abs(minXPush) < Math.Abs(minYPush))
            {
                position.X += minXPush;
            }
            else
            {
                position.Y += minYPush;
                moveSpeed.Y = 0;
                hasJumped = false;
            }

            hitRect = new RectangleF(position.X, position.Y, frameSize.X, frameSize.Y);
        }
    }
}
