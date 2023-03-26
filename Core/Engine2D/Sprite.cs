using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Engine2D
{
    public class Sprite
    {
        private Texture2D _texture;
        private Color _color;
        private Rectangle _rect;

        public float HalfWidth => _texture.Width / 2f;
        public float HalfHeight => _texture.Height / 2f;
        public int Width => _texture.Width;
        public int Height => _texture.Height;
        public Vector2 Origin { get; set; }

        public Rectangle Rectangle => _rect;
        public float LayerDepth { get; set; }
        public Color Color
        {
            get => _color;
            set => _color = value;
        }

        public Sprite(Texture2D texture, Color color)
        {
            _texture = texture;
            _color = color;
            LayerDepth = 0;
        }

        public void Draw(SpriteBatch spriteBatch, Transform transform)
        {
            _rect = new Rectangle((int)transform.position.X, (int)transform.position.Y, (int)(Width * transform.scale.X), (int)(Height * transform.scale.Y));
            spriteBatch.Draw(_texture, _rect, null, _color,
               transform.rotation, Origin, SpriteEffects.None, LayerDepth);
        }
    }
}
