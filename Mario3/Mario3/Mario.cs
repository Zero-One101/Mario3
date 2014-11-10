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
        private readonly List<Keys> prevDownKeys = new List<Keys>();
        private float gravity = 2.0f;
        private bool isFalling = true;

        public override void Initialise(Viewport viewport, EntityManager entityManager)
        {
            this.entityManager = entityManager;
            entityManager.KeyDown += entityManager_KeyDown;
            this.viewport = viewport;
            position = new Vector2(viewport.Width / 2, viewport.Height / 2);
            frameSize = new Point(16, 32);
            moveSpeed = 2.0f;
            hitRect = new Rectangle((int)position.X, (int)position.Y, frameSize.X, frameSize.Y);
        }

        void entityManager_KeyDown(object sender, KeyDownEventArgs e)
        {
            downKeys.Add(e.Key);
        }

        public override void Update(GameTime gameTime)
        {
            CheckInput();
            ApplyGravity();
            hitRect = new Rectangle((int)position.X, (int)position.Y, frameSize.X, frameSize.Y);
        }

        private void ApplyGravity()
        {
            if (isFalling)
            {
                position.Y += gravity;
            }
        }

        private void CheckInput()
        {
            if (downKeys.Contains(Keys.Right))
            {
                position.X += moveSpeed;
            }
            if (downKeys.Contains(Keys.Left))
            {
                position.X -= moveSpeed;
            }
            if (downKeys.Contains(Keys.Up))
            {
                if (!isFalling)
                {
                    position.Y -= (moveSpeed * 30);
                    isFalling = true;
                }
            }
            if (downKeys.Contains(Keys.Down))
            {
                //position.Y += moveSpeed;
            }

            downKeys.Clear();
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
