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
        protected float terminalVelocity = 5.0f;
        protected RectangleF hitRect;

        public RectangleF HitRect
        {
            get { return hitRect; }
        }
        /// <summary>
        /// Initialises variables in a game object
        /// </summary>
        /// <param name="viewport">The viewport of the screen</param>
        /// <param name="entityManager">An instance of the EntityManager</param>
        public virtual void Initialise(Viewport viewport, EntityManager entityManager)
        {
            this.viewport = viewport;
            this.entityManager = entityManager;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        /// <summary>
        /// To be called when a collision between game objects has occured
        /// </summary>
        /// <param name="gameObject">The game object being collided with</param>
        public abstract void Collide(GameObject gameObject);
    }
}