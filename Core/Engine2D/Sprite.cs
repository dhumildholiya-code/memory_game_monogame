using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Engine2D
{
    public class Sprite
    {
        private Texture2D _texture;
        private Color _color;
        private Transform _transform;

        public float HalfWidth => _texture.Width / 2f;
        public float HalfHeight => _texture.Height / 2f;
        public float LayerDepth { get; set; }
        public Color Color
        {
            get => _color;
            set => _color = value;
        }

        public Vector2 Origin => new Vector2(_texture.Width / 2f, _texture.Height / 2);

        public Sprite(Texture2D texture, Transform transform, Color color)
        {
            _texture = texture;
            _color = color;
            _transform = transform;
            LayerDepth = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _transform.position, null, _color,
                _transform.rotation, Origin, _transform.scale, SpriteEffects.None, LayerDepth);
        }
    }
}
