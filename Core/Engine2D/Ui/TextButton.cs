using Core.Engine2D.Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Core.Engine2D.Ui
{
    public class TextButton
    {
        public event Action OnButtonClick;

        public bool isActive;

        private Sprite _sprite;
        private SpriteFont _font;
        private Transform _transform;
        private string _text;

        private Color _normalColor;
        private Color _hoverColor;
        private Color _pressedColor;

        public Sprite Sprite => _sprite;
        public Transform Transform => _transform;

        public TextButton(string text, Sprite sprite, SpriteFont font, Transform transform)
        {
            isActive = true;
            _sprite = sprite;
            _transform = transform;
            _font = font;
            _text = text;

            Vector2 size = font.MeasureString(text);
            _transform.scale = size + new Vector2(50, 10);

            _normalColor = _sprite.Color;
            _hoverColor = Color.Gray;
            _pressedColor = Color.Green;

        }

        public void Update(GameTime gameTime)
        {
            if (!isActive) return;
            var mousePos = Input.GetMousePosition();
            Rectangle mouseRect = new Rectangle(mousePos.X, mousePos.Y, 1, 1);
            if (mouseRect.Intersects(_sprite.Rectangle))
            {
                _sprite.Color = _hoverColor;
                if (Input.IsMouseButtonPressed(0))
                {
                    _sprite.Color = _pressedColor;
                }
                if (Input.IsMouseButtonReleased(0))
                {
                    OnButtonClick?.Invoke();
                }
            }
            else
            {
                _sprite.Color = _normalColor;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!isActive) return;
            _sprite.Draw(spriteBatch, _transform);
            Text.Draw(_font, spriteBatch, _text, _transform.position, Color.Black);
        }
    }
}
