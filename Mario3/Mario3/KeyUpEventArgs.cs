using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Mario3
{
    class KeyUpEventArgs : EventArgs
    {
        private readonly Keys key;

        public Keys Key
        {
            get { return key; }
        }

        public KeyUpEventArgs(Keys key)
        {
            this.key = key;
        }
    }
}
