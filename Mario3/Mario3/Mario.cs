using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mario3
{
    class Mario : GameObject
    {
        private readonly List<Keys> downKeys = new List<Keys>();
        private readonly List<Keys> upKeys = new List<Keys>();
        private const float gravity = 0.1f;
        private const float jumpStrength = 3.0f;
        private bool isFalling = true;
        private SpriteFont font;

        public override void Initialise(Viewport viewport, EntityManager entityManager)
        {
            base.Initialise(viewport, entityManager);
            entityManager.KeyDown += entityManager_KeyDown;
            entityManager.KeyUp += entityManager_KeyUp;
            position = new Vector2(viewport.Width / 2, viewport.Height / 2);
            nextPosition = position;
            frameSize = new Point(16, 32);
            hitRect = new Rectangle((int)position.X, (int)position.Y, frameSize.X, frameSize.Y);
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
            CheckInput();
            hitRect = new Rectangle((int)position.X, (int)position.Y, frameSize.X, frameSize.Y);
            ApplyGravity();
        }

        private void ApplyGravity()
        {
            if (isFalling)
            {
                moveSpeed.Y += gravity;
            }
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
                if (!isFalling)
                {
                    moveSpeed.Y -= jumpStrength;
                    isFalling = true;
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
            spriteBatch.DrawRectangle(hitRect, Color.Green);
            spriteBatch.DrawString(font, moveSpeed.Y.ToString(), new Vector2(50, 50), Color.Red);
        }

        public override void Collide(GameObject gameObject)
        {
            if (gameObject is Tile)
            {
                Tile tile = (Tile)gameObject;
                if (tile.ColType == Tile.CollisionType.SOLID)
                {
                    Rectangle objectCol = tile.HitRect;

                    // X-axis collision
                    if (hitRect.Right > objectCol.Left && hitRect.Right < objectCol.Right)
                    {
                        if (moveSpeed.X > 0)
                        {
                            position.X = objectCol.X - objectCol.Width;
                        }
                    }
                    else if (hitRect.Left < objectCol.Right && hitRect.Left > objectCol.Left)
                    {
                        if (moveSpeed.X < 0)
                        {
                            position.X = objectCol.X + hitRect.Width;
                        }
                    }

                    // Y-axis collision
                    if (hitRect.Bottom > objectCol.Top && hitRect.Bottom < objectCol.Bottom)
                    {
                        if (moveSpeed.Y > 0)
                        {
                            position.Y = objectCol.Y - hitRect.Height - 0.1f;
                        }
                        moveSpeed.Y = 0;
                        //isFalling = false;
                    }
                }
            }
        }
    }
}
