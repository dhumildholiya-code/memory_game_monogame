using Core.Engine2D.StateMachine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Core
{
    public class GameManager
    {
        private GameState _currentState;

        #region Fonts
        private SpriteFont _font;
        private SpriteFont _titleFont;

        public SpriteFont Font => _font;
        public SpriteFont TitleFont => _titleFont;
        #endregion
        #region Textures

        public Texture2D PointTex { get; private set; }
        public Texture2D BallTex { get; private set; }
        #endregion

        public const int BallNumber = 4;
        public static Color[] colors = new Color[BallNumber] {
            new Color(new Vector3(163f, 37f, 18f)/ 255f), // red
            new Color(new Vector3(49f, 163f, 18f)/ 255f), // green
            new Color(new Vector3(18f, 98f, 163f)/ 255f),  // blue
            new Color(new Vector3(163f, 158f, 18f)/ 255f),  // yellow
        };

        public GameManager()
        {
            ChangeState(GetMainMenuState());
        }

        public void LoadContent(ContentManager content, GraphicsDevice graphics)
        {
            _font = content.Load<SpriteFont>("TestFont");
            _titleFont = content.Load<SpriteFont>("TitleFont");

            BallTex = content.Load<Texture2D>("ball");
            PointTex = new Texture2D(graphics, 1, 1);
            PointTex.SetData(new[] { Color.White });
        }

        public void ChangeState(GameState newState)
        {
            if (_currentState != null)
            {
                _currentState.Exit();
            }
            _currentState = newState;
            _currentState.Enter();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _currentState.Draw(spriteBatch);
        }
        public void Update(GameTime gameTime)
        {
            _currentState.Update(gameTime);
        }

        #region State Factory
        public GameState GetMainMenuState()
        {
            return new MainMenuState(this);
        }
        public GameState GetGameplayState()
        {
            return new GameplayState(this);
        }
        #endregion
    }
}
