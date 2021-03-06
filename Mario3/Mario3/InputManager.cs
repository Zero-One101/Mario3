﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace Mario3
{
    class InputManager
    {
        public event KeyDownHandler KeyDown;
        public event KeyUpHandler KeyUp;
        KeyboardState keyboardState;
        readonly List<Keys> downKeys = new List<Keys>();
        readonly List<Keys> upKeys = new List<Keys>();
        readonly List<Keys> prevDownKeys = new List<Keys>();

        public void Update()
        {
            keyboardState = Keyboard.GetState();

            prevDownKeys.Clear();
            prevDownKeys.AddRange(downKeys);
            downKeys.Clear();
            upKeys.Clear();

            downKeys.AddRange(keyboardState.GetPressedKeys());

            foreach (Keys key in downKeys)
            {
                FireKeyDown(key);
            }

            foreach (Keys key in prevDownKeys.Where(key => !downKeys.Contains(key)))
            {
                upKeys.Add(key);
            }

            foreach (Keys key in upKeys)
            {
                FireKeyUp(key);
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
    }
}
