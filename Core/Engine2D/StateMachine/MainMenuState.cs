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
            Vector2 pos = new Vector2(Screen.Width / 2, Screen.Height * .3f);
            Text.Draw(Ctx.TitleFont, spriteBatch, title, pos, Color.White);
        }

        public override void Enter()
        {
            _button = UiManager.CreateButton("Play", new Vector2(Screen.Width / 2, Screen.Height * .8f), Ctx.PointTex, Color.White, Ctx.Font);
            _button.OnButtonClick += OnPlayButtonClick;
        }

        public override void Exit()
        {
            _button.OnButtonClick -= OnPlayButtonClick;
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
            }
        }

        private void OnPlayButtonClick()
        {
            Ctx.ChangeState(Ctx.GetGameplayState());
        }
    }
}
