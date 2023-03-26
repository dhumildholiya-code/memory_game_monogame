using Core.Engine2D;
using Core.Engine2D.Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Core
{
    public class MemoryGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private RenderTarget2D _doubleBuffer;
        private Rectangle _renderRectangle;

        private GameManager _gameManager;

        public MemoryGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Screen.Width = 640;
            Screen.Height = 480;
            _doubleBuffer = new RenderTarget2D(GraphicsDevice, Screen.Width, Screen.Height);
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();

            Window.ClientSizeChanged += HandleClientSizeChange;
            HandleClientSizeChange(null, null);

            _gameManager = new GameManager();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _gameManager.LoadContent(Content, GraphicsDevice);
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Input Handler
            Input.Update(gameTime);

            _gameManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(_doubleBuffer);
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            _gameManager.Draw(_spriteBatch);
            _spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(_doubleBuffer, _renderRectangle, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        private void HandleClientSizeChange(object sender, EventArgs e)
        {
            var width = Window.ClientBounds.Width;
            var height = Window.ClientBounds.Height;
            if (height < width / (float)_doubleBuffer.Width * _doubleBuffer.Height)
            {
                width = (int)(height / (float)_doubleBuffer.Height * _doubleBuffer.Width);
            }
            else
            {
                height = (int)(width / (float)_doubleBuffer.Width * _doubleBuffer.Height);
            }
            var x = (Window.ClientBounds.Width - width) / 2;
            var y = (Window.ClientBounds.Height - height) / 2;
            _renderRectangle = new Rectangle(x, y, width, height);
        }
    }
}