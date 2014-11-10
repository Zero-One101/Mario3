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
        readonly List<GameObject> entities = new List<GameObject>();
        readonly List<GameObject> entitiesToAdd = new List<GameObject>();
        readonly List<Keys> downKeys = new List<Keys>();
        readonly List<Keys> upKeys = new List<Keys>();
        private Viewport viewport;
        private Texture2D spritesheet;
        private InputManager inputManager;
        private ResourceManager resourceManager;

        public EntityManager(InputManager inputManager, ResourceManager resourceManager)
        {
            this.resourceManager = resourceManager;
            this.inputManager = inputManager;
            this.inputManager.KeyDown += inputManager_KeyDown;
        }

        void inputManager_KeyDown(object sender, KeyDownEventArgs e)
        {
            downKeys.Add(e.Key);
        }

        public void AddEntity(GameObject entity)
        {
            entity.Initialise(viewport, spritesheet);
            entitiesToAdd.Add(entity);
        }

        public void Initialise(Viewport viewport, Texture2D spritesheet)
        {
            
        }

        public void Update(GameTime gameTime)
        {
            entities.AddRange(entitiesToAdd);
            entitiesToAdd.Clear();

            foreach (Keys key in downKeys)
            {
                FireKeyDown(key);
            }

            foreach (GameObject entity in entities.Where(entity => !entity.IsDead))
            {
                entity.Update(gameTime);
            }

            CheckCollision();

            entities.RemoveAll(x => x.IsDead);

            downKeys.Clear();
            upKeys.Clear();
        }

        private void CheckCollision()
        {
            foreach (GameObject firstEntity in entities)
            {
                foreach (GameObject secondEntity in entities)
                {
                    if (firstEntity.HitRect.Intersects(secondEntity.HitRect))
                    {
                        firstEntity.Collide(secondEntity);
                        secondEntity.Collide(firstEntity);
                    }
                }
            }
        }

        private void FireKeyDown(Keys key)
        {
            KeyDown(this, new KeyDownEventArgs(key));
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
