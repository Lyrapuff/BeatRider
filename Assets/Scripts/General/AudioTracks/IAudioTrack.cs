namespace General.AudioTracks
{
    public interface IAudioTrack
    {
        string Title { get; set; }
        string Id { get; set; }
        string Thumbnail { get; set; }
    }
}