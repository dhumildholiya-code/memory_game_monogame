using Core.Engine2D;
using Core.Engine2D.Helper;
using Core.Engine2D.Ui;
using Core.Entities;
using Core.EntityFactory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Core
{
    public enum WorldState
    {
        GenerateSequence,
        ShowSequence,
        Input,
        Correct,
        Wrong
    }
    public class World
    {
        private GameManager _ctx;
        private Rectangle _wall;
        private SequenceGenerator _sequenceGenerator;
        private Ball[] _balls;
        private TextButton[] _ballButtons;

        private WorldState _state;

        private int _inputSequenceIndex;

        public World(GameManager data)
        {
            _ctx = data;
            _state = WorldState.GenerateSequence;
        }

        public void Initialize()
        {
            _sequenceGenerator = new SequenceGenerator(.2f, 1f);
            _sequenceGenerator.OnPlaySequenceComplete += SequencePlayingComplete;
            _wall = new Rectangle(30, 70, 300, Screen.Height - 100);
            _balls = new Ball[4];
            _ballButtons = new TextButton[4];

            Random rand = new Random();
            Vector2 buttonPos = new Vector2(330 + (Screen.Width - 330) / 2, Screen.Height * .3f + 80);
            for (int i = 0; i < _balls.Length; i++)
            {
                Vector2 randPosition = new Vector2(rand.Next(_wall.X, _wall.X + _wall.Width), rand.Next(_wall.Y, _wall.Y + _wall.Height));
                int dirX = rand.NextDouble() < .5 ? -1 : 1;
                int dirY = rand.NextDouble() < .5 ? -1 : 1;

                _balls[i] = BallFactory.CreateBall(_ctx.BallTex, randPosition, GameManager.colors[i]);
                _balls[i].wall = _wall;
                _balls[i].velocity = new Vector2(rand.Next(2, 4) * dirX, rand.Next(2, 4) * dirY);

                _ballButtons[i] = UiManager.CreateButton(GameManager.ballNames[i], buttonPos, _ctx.PointTex, GameManager.colors[i], _ctx.Font);
                int index = i;
                _ballButtons[i].OnButtonClick += () => { ClickBallButton(index); };
                buttonPos += new Vector2(0, 40);
            }
        }

        private void SequencePlayingComplete()
        {
            _state = WorldState.Input;
            _inputSequenceIndex = 0;
        }

        private void ClickBallButton(int index)
        {
            if (_state != WorldState.Input) return;
            _balls[index].Pulse(Color.White, .2f);
            if (!_sequenceGenerator.CheckSequenceElement(_inputSequenceIndex, index))
            {
                _ctx.AddScore(-30);
                _state = WorldState.Wrong;
                return;
            }
            _inputSequenceIndex++;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_ctx.PointTex, _wall, Color.Gray);
            for (int i = 0; i < _balls.Length; i++)
            {
                _balls[i].Draw(spriteBatch);
            }
            Vector2 pos = new Vector2(330 + (Screen.Width - 330) / 2, Screen.Height * .3f);
            Text.Draw(_ctx.Font, spriteBatch, $"Score : {_ctx.Score}", pos + new Vector2(0, -50), Color.Green);
            Text.Draw(_ctx.Font, spriteBatch, "Backspace to Quit", pos, Color.White);
            pos += new Vector2(0, 30);
            Text.Draw(_ctx.Font, spriteBatch, "Space to Next Sequence", pos, Color.White);
            string info = string.Empty;
            Color color = Color.White;
            switch (_state)
            {
                case WorldState.ShowSequence:
                    info = "Remember Sequence";
                    break;
                case WorldState.Input:
                    info = "Enter Your Sequence";
                    break;
                case WorldState.Correct:
                    color = Color.Green;
                    info = "Correct Sequence";
                    break;
                case WorldState.Wrong:
                    color = Color.Red;
                    info = "Wrong Sequence";
                    break;
                case WorldState.GenerateSequence:
                    break;
                default:
                    break;
            }
            Text.Draw(_ctx.Font, spriteBatch, info, new Vector2(180, 70 * .7f), color);
        }
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < _balls.Length; i++)
            {
                _balls[i].Update(gameTime);
                _balls[i].CheckCollisionWithOtherBalls(_balls);
            }

            if (Input.IsKeyDown(Keys.Back))
            {
                _ctx.ChangeState(_ctx.GetMainMenuState());
            }

            switch (_state)
            {
                case WorldState.GenerateSequence:
                    _sequenceGenerator.CreateSequence(GameManager.BallNumber, GameManager.BallNumber);
                    _sequenceGenerator.PlaySequence();
                    _state = WorldState.ShowSequence;
                    break;
                case WorldState.ShowSequence:
                    _sequenceGenerator.Update(_balls, gameTime);
                    break;
                case WorldState.Input:
                    if (Input.IsKeyDown(Keys.Space))
                    {
                        _ctx.AddScore(-40);
                        _state = WorldState.GenerateSequence;
                    }
                    if (Input.IsKeyDown(Keys.Right))
                    {
                        _balls[2].Pulse(Color.White, .2f);
                        if (!_sequenceGenerator.CheckSequenceElement(_inputSequenceIndex, 2))
                        {
                            _ctx.AddScore(-30);
                            _state = WorldState.Wrong;
                            break;
                        }
                        _inputSequenceIndex++;
                    }
                    if (Input.IsKeyDown(Keys.Left))
                    {
                        _balls[3].Pulse(Color.White, .2f);
                        if (!_sequenceGenerator.CheckSequenceElement(_inputSequenceIndex, 3))
                        {
                            _ctx.AddScore(-30);
                            _state = WorldState.Wrong;
                            break;
                        }
                        _inputSequenceIndex++;
                    }
                    if (Input.IsKeyDown(Keys.Up))
                    {
                        _balls[0].Pulse(Color.White, .2f);
                        if (!_sequenceGenerator.CheckSequenceElement(_inputSequenceIndex, 0))
                        {
                            _ctx.AddScore(-30);
                            _state = WorldState.Wrong;
                            break;
                        }
                        _inputSequenceIndex++;
                    }
                    if (Input.IsKeyDown(Keys.Down))
                    {
                        _balls[1].Pulse(Color.White, .2f);
                        if (!_sequenceGenerator.CheckSequenceElement(_inputSequenceIndex, 1))
                        {
                            _ctx.AddScore(-30);
                            _state = WorldState.Wrong;
                            break;
                        }
                        _inputSequenceIndex++;
                    }
                    if (_inputSequenceIndex == _sequenceGenerator.Length)
                    {
                        _ctx.AddScore(100);
                        _state = WorldState.Correct;
                        break;
                    }
                    break;
                case WorldState.Correct:
                    if (Input.IsKeyDown(Keys.Space))
                    {
                        _state = WorldState.GenerateSequence;
                    }
                    break;
                case WorldState.Wrong:
                    if (Input.IsKeyDown(Keys.Space))
                    {
                        _state = WorldState.GenerateSequence;
                    }
                    break;
            }
        }
    }
}
