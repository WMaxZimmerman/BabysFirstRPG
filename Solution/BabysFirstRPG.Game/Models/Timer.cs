using System;

namespace BabysFirstRPG.Game.Models
{
    public class Timer
    {
        private readonly decimal _duration;
        private decimal _elapsed;
        public bool IsReady { get; set; }

        public Timer(decimal duration)
        {
            _duration = duration;
        }

        public void Increment(TimeSpan elapsedTime)
        {
            var delta = elapsedTime.Milliseconds / 1000m;

            if (!IsReady)
            {
                _elapsed += delta;
                if (_elapsed >= _duration) IsReady = true;
            }
        }

        public void Reset()
        {
            IsReady = false;
            _elapsed = 0;
        }
    }
}
