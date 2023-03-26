using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
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
            foreach (var button in _buttons)
            {
                button.Update(gameTime);
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (var button in _buttons)
            {
                button.Draw(spriteBatch);
            }
        }

        public static void Remove(TextButton button)
        {
            _buttons.Remove(button);
        }
        public static TextButton CreateButton(Texture2D texture, Color color, SpriteFont font, Vector2 pos)
        {
            Sprite sprite = new Sprite(texture, color);
            Transform transform = new Transform(pos);
            var button = new TextButton("Button", sprite, font, transform);
            _buttons.Add(button);
            return button;
        }
    }
}
