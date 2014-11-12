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
            ApplyGravity();
            position = nextPosition;
            hitRect = new Rectangle((int)position.X, (int)position.Y, frameSize.X, frameSize.Y);
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

            nextPosition = position + moveSpeed;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.FillRectangle(new Rectangle((int)position.X, (int)position.Y, frameSize.X, frameSize.Y), Color.Red);
        }

        public override void Collide(GameObject gameObject)
        {
            if (gameObject is Tile)
            {
                Tile tile = (Tile)gameObject;
                if (tile.ColType == Tile.CollisionType.SOLID)
                {
                    isFalling = false;
                }
            }
        }
    }
}
