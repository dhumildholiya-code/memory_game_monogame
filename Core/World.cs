using Core.Engine2D;
using Core.Entities;
using Core.EntityFactory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Core
{
    public class World
    {
        private SpriteBatch _spriteBatch;
        private ContentManager _content;
        private GraphicsDevice _graphics;
        #region Textures
        private Texture2D _ballTex;
        private Texture2D _pointTex;
        #endregion

        private Rectangle _wall;
        private Ball[] _balls;

        public World(ContentManager content, SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            _content = content;
            _spriteBatch = spriteBatch;
            _graphics = graphics;
        }

        public void LoadContent()
        {
            _ballTex = _content.Load<Texture2D>("ball");
            _pointTex = new Texture2D(_graphics, 1, 1);
            _pointTex.SetData(new[] { Color.White });

            _wall = new Rectangle(30, 30, 300, Screen.Height - 60);
            _balls = new Ball[4];

            Random rand = new Random();
            for (int i = 0; i < _balls.Length; i++)
            {
                Vector2 randPosition = new Vector2(rand.Next(_wall.X, _wall.X + _wall.Width), rand.Next(_wall.Y, _wall.Y + _wall.Height));
                int dirX = rand.NextDouble() < .5 ? -1 : 1;
                int dirY = rand.NextDouble() < .5 ? -1 : 1;

                _balls[i] = BallFactory.CreateBall(_ballTex, randPosition, null);
                _balls[i].wall = _wall;
                _balls[i].velocity = new Vector2(rand.Next(2, 4) * dirX, rand.Next(2, 4) * dirY);
            }
        }
        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Draw(_pointTex, _wall, Color.Gray);
            for (int i = 0; i < _balls.Length; i++)
            {
                _balls[i].Draw(_spriteBatch);
            }
        }
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < _balls.Length; i++)
            {
                _balls[i].Update(gameTime);
                _balls[i].CheckCollisionWithOtherBalls(_balls);
            }
        }
    }
}
