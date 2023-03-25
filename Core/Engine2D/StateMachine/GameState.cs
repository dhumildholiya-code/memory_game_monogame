using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Engine2D.StateMachine
{
    public abstract class GameState : IState
    {
        protected GameManager Ctx;

        protected GameState(GameManager ctx)
        {
            Ctx = ctx;
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void Enter();

        public abstract void Exit();

        public virtual void Update(GameTime gameTime)
        {
            CheckChangeState();
        }

        protected abstract void CheckChangeState();
    }
}
