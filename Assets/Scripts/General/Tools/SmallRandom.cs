using System;

namespace General.Tools
{
    public static class SmallRandom
    {
        private static Random _random;

        public static void SetSeed(int seed)
        {
            _random = new Random(seed);
        }

        public static int GetNumber(int min, int max)
        {
            if (_random == null)
            {
                _random = new Random();
            }
            
            return _random.Next(min, max);
        }
        
        public static float GetNumber(float min, float max)
        {
            if (_random == null)
            {
                _random = new Random();
            }
            
            return (float)(_random.NextDouble() * (max - min) + min);
        }
    }
}