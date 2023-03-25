using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Engine2D.StateMachine
{
    public interface IState
    {
        public void Enter();
        public void Draw(SpriteBatch spriteBatch);
        public void Update(GameTime gameTime);
        public void Exit();
    }
}
