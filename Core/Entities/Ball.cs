using Core.Engine2D;
using Core.Engine2D.Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Core.Entities
{
    public class Ball
    {
        private Transform _transform;
        private Sprite _sprite;

        public Vector2 velocity;
        public Rectangle wall;

        public Transform Transform => _transform;
        public Sprite Sprite => _sprite;

        public float Radius => _sprite.HalfWidth * _transform.scale.X;

        private Color _originalColor;
        private bool _isPulsing;
        private float _timer;

        public Ball(Transform transform, Sprite sprite)
        {
            _transform = transform;
            _transform.scale *= 1.5f;
            _sprite = sprite;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _sprite.Draw(spriteBatch, _transform);
        }
        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _transform.position += velocity;
            CheckWallCollision();


            if (_timer >= 0f && _isPulsing)
            {
                _timer -= deltaTime;
                if (_timer <= 0f)
                {
                    _isPulsing = false;
                    _sprite.Color = _originalColor;
                }
            }
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
                if (distLength < radiusSum)
                {
                    float overlap = radiusSum - distLength;
                    Transform.position -= Vector2.Normalize(dist) * overlap;
                    balls[i].Transform.position += Vector2.Normalize(dist) * overlap;

                    //Swap Velocity
                    Vector2 temp = velocity;
                    velocity = balls[i].velocity;
                    balls[i].velocity = temp;
                }
            }
        }
        public void Pulse(Color pulseColor, float duration)
        {
            _isPulsing = true;
            _originalColor = _sprite.Color;
            _sprite.Color = pulseColor;
            _timer = duration;
        }

        private void CheckWallCollision()
        {
            if (_transform.position.X > wall.X + wall.Width - Radius)
            {
                _transform.position.X = wall.X + wall.Width - Radius;
                velocity.X *= -1f;
            }
            else if (_transform.position.X < wall.X + Radius)
            {
                _transform.position.X = wall.X + Radius;
                velocity.X *= -1f;
            }
            if (_transform.position.Y > wall.Y + wall.Height - Radius)
            {
                _transform.position.Y = wall.Y + wall.Height - Radius;
                velocity.Y *= -1f;
            }
            if (Transform.position.Y < wall.Y + Radius)
            {
                _transform.position.Y = wall.Y + Radius;
                velocity.Y *= -1f;
            }
        }
    }
}
