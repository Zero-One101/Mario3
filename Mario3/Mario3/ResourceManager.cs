using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Mario3
{
    class ResourceManager
    {
        private ContentManager content;
        private readonly Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>(); 
        private readonly Dictionary<string, SoundEffect> sounds = new Dictionary<string, SoundEffect>();
        private readonly Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>(); 

        public ResourceManager(ContentManager content)
        {
            this.content = content;
        }

        public Texture2D LoadTexture2D(string filepath)
        {
            if (textures.ContainsKey(filepath))
            {
                return textures[filepath];
            }
            else
            {
                Texture2D texture = content.Load<Texture2D>(filepath);
                textures.Add(filepath, texture);
                return texture;
            }
        }

        public SoundEffect LoadSoundEffect(string filepath)
        {
            if (sounds.ContainsKey(filepath))
            {
                return sounds[filepath];
            }
           
            SoundEffect sound = content.Load<SoundEffect>(filepath);
            sounds.Add(filepath, sound);
            return sound;
        }

        public SpriteFont LoadSpriteFont(string filepath)
        {
            if (fonts.ContainsKey(filepath))
            {
                return fonts[filepath];
            }

            SpriteFont font = content.Load<SpriteFont>(filepath);
            fonts.Add(filepath, font);
            return font;
        }

        public void Clear()
        {
            textures.Clear();
            sounds.Clear();
        }
    }
}
