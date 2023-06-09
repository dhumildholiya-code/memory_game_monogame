﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
            _world = new World(Ctx);
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
        }
    }
}
