using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Mario3
{
    class EntityManager
    {
        public event KeyDownHandler KeyDown;
        public event KeyUpHandler KeyUp;
        private readonly List<GameObject> entities = new List<GameObject>();
        private readonly List<GameObject> entitiesToAdd = new List<GameObject>();
        private readonly List<Keys> downKeys = new List<Keys>();
        private readonly List<Keys> upKeys = new List<Keys>();
        private Viewport viewport;
        private InputManager inputManager;

        internal ResourceManager ResourceManager { get; private set; }

        public EntityManager(InputManager inputManager, ResourceManager resourceManager)
        {
            ResourceManager = resourceManager;
            this.inputManager = inputManager;
            this.inputManager.KeyDown += inputManager_KeyDown;
            this.inputManager.KeyUp += inputManager_KeyUp;
        }

        void inputManager_KeyUp(object sender, KeyUpEventArgs e)
        {
            upKeys.Add(e.Key);
        }

        void inputManager_KeyDown(object sender, KeyDownEventArgs e)
        {
            downKeys.Add(e.Key);
        }

        public void AddEntity(GameObject entity)
        {
            entity.Initialise(viewport, this);
            entitiesToAdd.Add(entity);
        }

        public void Initialise(Viewport viewport)
        {
            this.viewport = viewport;
        }

        public void Update(GameTime gameTime)
        {
            entities.AddRange(entitiesToAdd);
            entitiesToAdd.Clear();

            UpdateKeys();

            foreach (GameObject entity in entities.Where(entity => !entity.IsDead))
            {
                entity.Update(gameTime);
            }

            entities.RemoveAll(x => x.IsDead);

            CheckCollision();

            downKeys.Clear();
            upKeys.Clear();
        }

        private void UpdateKeys()
        {
            foreach (Keys key in downKeys)
            {
                FireKeyDown(key);
            }

            foreach (Keys key in upKeys)
            {
                FireKeyUp(key);
            }
        }

        private void CheckCollision()
        {
            Debug.Print("Checking Collision");
            // TODO: Find a method that isn't O(n^2)
            foreach (GameObject firstEntity in entities)
            {
                GameObject entity = firstEntity;
                foreach (GameObject secondEntity in entities.Where(secondEntity => entity.HitRect.Intersects(secondEntity.HitRect)))
                {
                    firstEntity.Collide(secondEntity);
                    secondEntity.Collide(firstEntity);
                }
            }
        }

        private void FireKeyDown(Keys key)
        {
            if (KeyDown != null)
            {
                KeyDown(this, new KeyDownEventArgs(key));
            }
        }

        private void FireKeyUp(Keys key)
        {
            if (KeyUp != null)
            {
                KeyUp(this, new KeyUpEventArgs(key));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject entity in entities)
            {
                entity.Draw(spriteBatch);
            }
        }
    }
}
