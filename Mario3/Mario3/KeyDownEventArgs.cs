using Microsoft.Xna.Framework.Input;
using System;

namespace Mario3
{
    class KeyDownEventArgs : EventArgs
    {
        private readonly Keys key;

        public Keys Key
        {
            get { return key; }
        }

        public KeyDownEventArgs(Keys key)
        {
            this.key = key;
        }
    }
}
