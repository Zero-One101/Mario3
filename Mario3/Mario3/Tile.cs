﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mario3
{
    class Tile : GameObject
    {
        public int TileId { get; private set; }
        public enum CollisionType
        {
            NONE = 0,
            SOLID,
        };

        private readonly CollisionType colType;

        internal CollisionType ColType
        {
            get { return colType; }
        }

        public Tile(float posX, float posY)
        {
            position.X = posX;
            position.Y = posY;
            frameSize = new Point(16, 16);
            hitRect = new RectangleF(posX, posY, frameSize.X, frameSize.Y);
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
            spriteBatch.DrawRectangle(hitRect.ToRectangle(), Color.Green);
        }

        public override void Collide(GameObject gameObject)
        {
            
        }
    }
}
