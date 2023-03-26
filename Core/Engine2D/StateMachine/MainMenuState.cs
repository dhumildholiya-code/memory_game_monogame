using Core.Engine2D.Helper;
using Core.Engine2D.Ui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Core.Engine2D.StateMachine
{
    public class MainMenuState : GameState
    {
        private TextButton _button;
        public MainMenuState(GameManager ctx) : base(ctx)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            string title = "Memory Game";
            string info = "Press Space to Continue";
            Vector2 pos = new Vector2(Screen.Width / 2, Screen.Height * .3f);
            Text.Draw(Ctx.TitleFont, spriteBatch, title, pos, Color.White);
            pos = new Vector2(Screen.Width / 2, Screen.Height * .8f);
            Text.Draw(Ctx.Font, spriteBatch, info, pos, Color.Green);
        }

        public override void Enter()
        {
            _button = UiManager.CreateButton(Ctx.PointTex, Color.White, Ctx.Font, new Vector2(Screen.Width / 2, Screen.Height / 2));
            //_button = UiManager.CreateButton(Ctx.PointTex, Color.White, Ctx.Font, Vector2.Zero);
        }

        public override void Exit()
        {
            UiManager.Remove(_button);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void CheckChangeState()
        {
            if (Input.IsKeyDown(Keys.Space))
            {
                Ctx.ChangeState(Ctx.GetGameplayState());
            }
        }
    }
}
