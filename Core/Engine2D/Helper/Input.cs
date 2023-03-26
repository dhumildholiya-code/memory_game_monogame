using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Core.Engine2D.Helper
{

    public static class Input
    {
        private static Dictionary<Keys, GameKey> _gameKeys;
        private static Dictionary<int, MouseButton> _mouseButtons;

        private static KeyboardState _prevKeyboardState;
        private static MouseState _prevMouseState;


        static Input()
        {
            _prevKeyboardState = Keyboard.GetState();
            _prevMouseState = Mouse.GetState();
            _mouseButtons = new Dictionary<int, MouseButton>
            {
                {0, new MouseButton(0) },
                {1, new MouseButton(1) },
                {2, new MouseButton(2) },
            };
            _gameKeys = new Dictionary<Keys, GameKey>
            {
                { Keys.Space, new GameKey(Keys.Space) },
                {Keys.Left, new GameKey(Keys.Left) },
                {Keys.Right, new GameKey(Keys.Right) },
                {Keys.Up, new GameKey(Keys.Up) },
                {Keys.Down, new GameKey(Keys.Down) },
                {Keys.Back, new GameKey(Keys.Back) },
            };
        }

        public static Point GetMousePosition()
        {
            var state = Mouse.GetState();
            Point point = Util.RemapRect(state.Position, Screen.ScreenRect);
            return point;
        }

        public static bool IsMouseButtonPressed(int index)
        {
            if (_mouseButtons.ContainsKey(index))
            {
                return _mouseButtons[index].pressed;
            }
            return false;
        }
        public static bool IsMouseButtonDown(int index)
        {
            if (_mouseButtons.ContainsKey(index))
            {
                return _mouseButtons[index].down;
            }
            return false;
        }
        public static bool IsMouseButtonReleased(int index)
        {
            if (_mouseButtons.ContainsKey(index))
            {
                return _mouseButtons[index].released;
            }
            return false;
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
            HandleKeyboardInput();
            HandleMouseInput();
        }

        private static void HandleKeyboardInput()
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

        private static void HandleMouseInput()
        {
            MouseState mouseState = Mouse.GetState();
            foreach (int key in _mouseButtons.Keys)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    MouseButton button = _mouseButtons[key];
                    button.pressed = true;
                    if (_prevMouseState.LeftButton == ButtonState.Released)
                    {
                        button.down = true;
                    }
                    else
                    {
                        button.down = false;
                    }
                    _mouseButtons[key] = button;
                }
                else
                {
                    MouseButton button = _mouseButtons[key];
                    button.pressed = false;
                    if (_prevMouseState.LeftButton == ButtonState.Pressed)
                    {
                        button.released = true;
                    }
                    else
                    {
                        button.released = false;
                    }
                    _mouseButtons[key] = button;
                }
            }
            _prevMouseState = mouseState;
        }
    }
}
