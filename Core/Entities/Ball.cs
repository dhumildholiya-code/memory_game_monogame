using Core.Engine2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Entities
{
    public class Ball
    {
        private Transform _transform;
        private Sprite _sprite;

        public Vector2 velocity;

        public Transform Transform => _transform;
        public Sprite Sprite => _sprite;

        public float Radius => _sprite.HalfWidth * _transform.scale.X;

        public Ball(Transform transform, Sprite sprite)
        {
            _transform = transform;
            _transform.scale *= 1.5f;
            _sprite = sprite;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _sprite.Draw(spriteBatch);
        }
        public void Update(GameTime gameTime)
        {
            _transform.position += velocity;
            CheckWallCollision();
        }

        public void CheckCollisionWithOtherBalls(Ball[] balls)
        {
            for (int i = 0; i < balls.Length; i++)
            {
                if (balls[i] == this) continue;
                Vector2 dist = balls[i].Transform.position - Transform.position;
                float distLength = dist.Length();
                float radiusSum = Radius + balls[i].Radius;
                
                //Balls Are Overlaping
                if(distLength < radiusSum)
                {
                    float overlap = radiusSum - distLength;
                    Transform.position -= Vector2.Normalize(dist) * overlap;
                    balls[i].Transform.position += Vector2.Normalize(dist) * overlap;

                    //Swap Velocity
                    Vector2 temp = velocity;
                    velocity.X = balls[i].velocity.X;
                    velocity.Y = balls[i].velocity.Y;
                    balls[i].velocity.X = temp.X;
                    balls[i].velocity.Y = temp.Y;
                }
            }
        }

        private void CheckWallCollision()
        {
            if(_transform.position.X + Radius > Screen.Width)
            {
                velocity.X *= -1f;
            }
            else if(_transform.position.X - Radius < 0f)
            {
                velocity.X *= -1f;
            }
            if(_transform.position.Y + Radius > Screen.Height)
            {
                velocity.Y *= -1f;
            }
            if(Transform.position.Y - Radius < 0f)
            {
                velocity.Y *= -1f;
            }
        }
    }
}
