namespace General.AudioTracks
{
    public interface IPlaylist
    {
        void Add(IAudioTrack track);
        IAudioTrack Get(IAudioTrack track);
        IAudioTrack Get(string id);
    }
}