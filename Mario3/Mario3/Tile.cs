using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mario3
{
    class Tile : GameObject
    {
        public enum CollisionType
        {
            NONE = 0,
            SOLID,
        };

        private CollisionType colType;

        internal CollisionType ColType
        {
            get { return colType; }
        }

        public Tile(float posX, float posY)
        {
            position.X = posX;
            position.Y = posY;
            frameSize = new Point(16, 16);
            hitRect = new Rectangle((int)posX, (int)posY, frameSize.X, frameSize.Y);
            colType = CollisionType.SOLID;
        }

        public override void Initialise(Viewport viewport, EntityManager entityManager)
        {
            this.viewport = viewport;
            this.entityManager = entityManager;
            spritesheet = entityManager.ResourceManager.LoadTexture2D(@"images\woodtile");
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spritesheet, position, Color.White);
            //spriteBatch.DrawRectangle(hitRect, Color.Green);
        }

        public override void Collide(GameObject gameObject)
        {
            
        }
    }
}
