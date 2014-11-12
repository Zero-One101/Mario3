using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mario3
{
    abstract class GameObject
    {
        protected Texture2D spritesheet;
        protected Vector2 position;
        protected Vector2 nextPosition;
        protected float maxMoveSpeed;
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
        protected Vector2 moveSpeed;
        protected Rectangle hitRect;

        public Rectangle HitRect
        {
            get { return hitRect; }
        }

        public virtual void Initialise(Viewport viewport, EntityManager entityManager)
        {
            this.viewport = viewport;
            this.entityManager = entityManager;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Collide(GameObject gameObject);
    }
}