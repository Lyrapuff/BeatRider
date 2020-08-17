namespace General.Audio
{
    public interface IAudioAnalyzer
    {
        float SpeedMultiplier { get; set; }
        float Speed { get; }
        float PureSpeed { get; }
    }
}