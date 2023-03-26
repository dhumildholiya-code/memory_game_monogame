using Core.Engine2D.Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Engine2D.Ui
{
    public class TextButton
    {
        private Sprite _sprite;
        private SpriteFont _font;
        private Transform _transform;
        private string _text;

        public Sprite Sprite => _sprite;
        public Transform Transform => _transform;

        public TextButton(string text, Sprite sprite, SpriteFont font, Transform transform)
        {
            _sprite = sprite;
            _transform = transform;
            _font = font;
            _text = text;
            Vector2 size = _font.MeasureString(_text);
            _transform.scale = size + new Vector2(50f, 2f);
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _sprite.Draw(spriteBatch, _transform);
            Text.Draw(_font, spriteBatch, _text, _transform.position, Color.Black);
        }
    }
}
