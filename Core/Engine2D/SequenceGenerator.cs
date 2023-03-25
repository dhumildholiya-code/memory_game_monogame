using Core.Entities;
using Microsoft.Xna.Framework;
using System;

namespace Core.Engine2D
{
    public class SequenceGenerator
    {
        private int[] _sequence;

        private float _ballPulseTime;
        private float _pulseInterval;

        private bool _showSequence;
        private int _sequenceIndex;
        private float _timer;

        public int Length => _sequence.Length;

        public event Action OnPlaySequenceComplete;

        public SequenceGenerator(float ballPulseTime, float pulseInterval)
        {
            _ballPulseTime = ballPulseTime;
            _pulseInterval = pulseInterval;
            _showSequence = false;
        }

        public void CreateSequence(int n, int maxValue)
        {
            _sequence = new int[n];
            Random rand = new Random();
            for (int i = 0; i < n; i++)
            {
                _sequence[i] = rand.Next(0, maxValue);
            }
        }
        public bool CheckSequenceElement(int index, int ballIndex)
        {
            if (_sequence[index] == ballIndex)
                return true;
            else return false;
        }

        public void PlaySequence()
        {
            _showSequence = true;
            _sequenceIndex = 0;
            _timer = _pulseInterval;
        }

        public void Update(Ball[] balls, GameTime gameTime)
        {
            if (_timer >= 0f && _showSequence)
            {
                _timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_timer <= 0f)
                {
                    balls[_sequence[_sequenceIndex]].Pulse(Color.White, _ballPulseTime);
                    _sequenceIndex++;
                    _timer = _pulseInterval;
                    if (_sequenceIndex >= _sequence.Length)
                    {
                        _showSequence = false;
                        OnPlaySequenceComplete?.Invoke();
                    }
                }
            }
        }
    }
}
