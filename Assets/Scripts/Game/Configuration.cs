using System;

namespace Game
{
    [Serializable]
    public class Configuration
    {
        public static Configuration Instance { get; set; }

        public GameSpeedType GameSpeed { get; set; } = GameSpeedType.Fast;
        public TrafficType Traffic { get; set; } = TrafficType.Heavy;

        [Serializable]
        public enum GameSpeedType
        {
            Slow = 0,
            Normal = 1,
            Fast = 2
        }
        
        [Serializable]
        public enum TrafficType
        {
            Light = 0,
            Medium = 1,
            Heavy = 2
        }
    }
}