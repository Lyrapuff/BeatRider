namespace General.AudioTracks.Analyzing
{
    public interface IAudioAnalyzer
    {
        float SpeedMultiplier { get; set; }
        float Speed { get; }
        float PureSpeed { get; }
        float[] Band { get; }
    }
}