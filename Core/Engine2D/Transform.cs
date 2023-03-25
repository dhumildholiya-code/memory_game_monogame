using Microsoft.Xna.Framework;

namespace Core.Engine2D
{
    public class Transform
    {
        public Vector2 position;
        public float rotation;
        public Vector2 scale;

        public Transform()
        {
            position = Vector2.Zero;
            rotation = 0f;
            scale = Vector2.One;
        }

        public Transform(Vector2 position)
        {
            this.position = position;
            rotation = 0f;
            scale = Vector2.One;
        }
    }
}
