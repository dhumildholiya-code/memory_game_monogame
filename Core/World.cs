using Core.Engine2D;
using Core.Entities;
using Core.EntityFactory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Threading.Tasks;

namespace Core
{
    public class WorldData
    {
        public Texture2D PointTex;
        public Texture2D BallTex;

        public WorldData(Texture2D pointTex, Texture2D ballTex)
        {
            PointTex = pointTex;
            BallTex = ballTex;
        }
    }
    public class World
    {
        private WorldData _data;
        private Rectangle _wall;
        private SequenceGenerator _sequenceGenerator;
        private Ball[] _balls;

        public World(WorldData data)
        {
            _data = data;
        }

        public void Initialize()
        {
            _wall = new Rectangle(30, 30, 300, Screen.Height - 60);
            _balls = new Ball[4];

            Random rand = new Random();
            for (int i = 0; i < _balls.Length; i++)
            {
                Vector2 randPosition = new Vector2(rand.Next(_wall.X, _wall.X + _wall.Width), rand.Next(_wall.Y, _wall.Y + _wall.Height));
                int dirX = rand.NextDouble() < .5 ? -1 : 1;
                int dirY = rand.NextDouble() < .5 ? -1 : 1;

                _balls[i] = BallFactory.CreateBall(_data.BallTex, randPosition, GameManager.colors[i]);
                _balls[i].wall = _wall;
                _balls[i].velocity = new Vector2(rand.Next(2, 4) * dirX, rand.Next(2, 4) * dirY);
            }

            int[] sequence = _sequenceGenerator.CreateSequence(4, GameManager.BallNumber);
            PlaySequence(sequence);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_data.PointTex, _wall, Color.Gray);
            for (int i = 0; i < _balls.Length; i++)
            {
                _balls[i].Draw(spriteBatch);
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

        private async Task PlaySequence(int[] sequence)
        {
            for (int i = 0; i < sequence.Length; i++)
            {
                _balls[sequence[i]].Pulse();
            }
        }
    }
}
