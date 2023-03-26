using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Core.Engine2D.Ui
{
    public static class UiManager
    {
        private static List<TextButton> _buttons;

        static UiManager()
        {
            _buttons = new List<TextButton>();
        }

        public static void Update(GameTime gameTime)
        {
            for (int i = _buttons.Count - 1; i >= 0; i--)
            {
                if (_buttons[i].isActive)
                {
                    _buttons[i].Update(gameTime);
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            for (int i = _buttons.Count - 1; i >= 0; i--)
            {
                if (_buttons[i].isActive)
                {
                    _buttons[i].Draw(spriteBatch);
                }
            }
        }

        public static void Remove(TextButton button)
        {
            _buttons.Remove(button);
        }
        public static TextButton CreateButton(string text, Vector2 pos, Texture2D texture, Color color, SpriteFont font)
        {
            Sprite sprite = new Sprite(texture, color);
            Transform transform = new Transform(pos);
            var button = new TextButton(text, sprite, font, transform);
            _buttons.Add(button);
            return button;
        }
    }
}
