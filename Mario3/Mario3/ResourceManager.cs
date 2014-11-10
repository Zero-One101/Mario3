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
            else
            {
                SoundEffect sound = content.Load<SoundEffect>(filepath);
                sounds.Add(filepath, sound);
                return sound;
            }
        }

        public void Clear()
        {
            textures.Clear();
            sounds.Clear();
        }
    }
}
