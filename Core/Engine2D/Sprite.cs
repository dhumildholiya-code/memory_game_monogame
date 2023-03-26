using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Engine2D
{
    public class Sprite
    {
        private Texture2D _texture;
        private Color _color;
        private Rectangle _rect;

        public float HalfWidth => Width / 2f;
        public float HalfHeight => Height / 2f;
        public int Width => _texture.Width;
        public int Height => _texture.Height;
        public Vector2 Origin => new Vector2(HalfWidth, HalfHeight);
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
            Rectangle rect = new Rectangle((int)transform.position.X, (int)transform.position.Y, (int)(Width * transform.scale.X), (int)(Height * transform.scale.Y));
            _rect = new Rectangle(rect.X - rect.Width / 2, rect.Y - rect.Height / 2, rect.Width, rect.Height);
            spriteBatch.Draw(_texture, rect, null, _color,
               transform.rotation, Origin, SpriteEffects.None, LayerDepth);
        }
    }
}
