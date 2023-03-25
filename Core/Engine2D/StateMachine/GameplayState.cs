using Core.Engine2D.Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Core.Engine2D.StateMachine
{
    public class GameplayState : GameState
    {
        private World _world;
        public GameplayState(GameManager ctx) : base(ctx)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _world.Draw(spriteBatch);
        }

        public override void Enter()
        {
            WorldData data = new WorldData(Ctx.PointTex, Ctx.BallTex);
            _world = new World(data);
            _world.Initialize();
        }

        public override void Exit()
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _world.Update(gameTime);
        }
        protected override void CheckChangeState()
        {
            if (Input.IsKeyDown(Keys.Space))
            {
                Ctx.ChangeState(Ctx.GetMainMenuState());
            }
        }
    }
}
