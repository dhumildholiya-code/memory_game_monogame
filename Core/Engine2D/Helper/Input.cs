using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Core.Engine2D.Helper
{

    public static class Input
    {
        private static Dictionary<Keys, GameKey> _gameKeys;

        private static KeyboardState _prevKeyboardState;

        static Input()
        {
            _prevKeyboardState = Keyboard.GetState();
            _gameKeys = new Dictionary<Keys, GameKey>
            {
                { Keys.Space, new GameKey(Keys.Space) }
            };
        }

        public static bool IsKeyDown(Keys key)
        {
            if (_gameKeys.ContainsKey(key))
            {
                return _gameKeys[key].down;
            }
            return false;
        }
        public static bool IskeyReleased(Keys key)
        {
            if (_gameKeys.ContainsKey(key))
            {
                return _gameKeys[key].released;
            }
            return false;
        }

        public static void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            foreach (Keys key in _gameKeys.Keys)
            {
                if (state.IsKeyDown(key))
                {
                    GameKey gameKey = _gameKeys[key];
                    gameKey.pressed = true;
                    if (_prevKeyboardState.IsKeyUp(key))
                    {
                        gameKey.down = true;
                    }
                    else
                    {
                        gameKey.down = false;
                    }
                    _gameKeys[key] = gameKey;
                }
                else
                {
                    GameKey gameKey = _gameKeys[key];
                    gameKey.pressed = false;
                    if (_prevKeyboardState.IsKeyDown(key))
                    {
                        gameKey.released = true;
                    }
                    else
                    {
                        gameKey.released = false;
                    }
                    _gameKeys[key] = gameKey;
                }
            }
            _prevKeyboardState = state;
        }
    }
}
