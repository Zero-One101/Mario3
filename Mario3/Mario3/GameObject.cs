using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mario3
{
    abstract class GameObject
    {
        protected Texture2D spritesheet;
        protected Vector2 position;
        protected bool isDead;

        public bool IsDead
        {
            get { return isDead; }
            protected set { isDead = value; }
        }

        protected Rectangle sourceRect;
        protected Point frameSize;
        protected Point framePos;
        protected Viewport viewport;
        protected EntityManager entityManager;
        protected float moveSpeed;
        protected Rectangle hitRect;

        public Rectangle HitRect
        {
            get { return hitRect; }
        }

        public abstract void Initialise(Viewport viewport, Texture2D spritesheet);
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Collide(GameObject gameObject);
    }
}