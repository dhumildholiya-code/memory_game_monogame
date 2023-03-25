using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Engine2D.Helper
{
    public static class Text
    {
        public static void Draw(SpriteFont font, SpriteBatch spriteBatch, string text, Vector2 pos, Color color)
        {
            Vector2 size = font.MeasureString(text);
            spriteBatch.DrawString(font, text, pos, color,
                0f, size / 2f, 1f, SpriteEffects.None, 0f);
        }
    }
}
