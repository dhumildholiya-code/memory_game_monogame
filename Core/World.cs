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
        #region Textures
        private Texture2D _ballTex;
        #endregion

        private Ball[] _balls;

        public World(ContentManager content, SpriteBatch spriteBatch)
        {
            _content = content;
            _spriteBatch = spriteBatch;
        }

        public void LoadContent()
        {
            _ballTex = _content.Load<Texture2D>("ball");
            _balls = new Ball[4];

            Random rand = new Random();
            for (int i = 0; i < _balls.Length; i++)
            {
                Vector2 randPosition = new Vector2(rand.Next(30, Screen.Width - 30), rand.Next(30, Screen.Height - 30));
                int dirX = rand.NextDouble() < .5 ? -1 : 1;
                int dirY = rand.NextDouble() < .5 ? -1 : 1;

                _balls[i] = BallFactory.CreateBall(_ballTex, randPosition, null);
                _balls[i].velocity = new Vector2(rand.Next(2, 4) * dirX, rand.Next(2, 4) * dirY);
            }

        }
        public void Draw(GameTime gameTime)
        {
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
