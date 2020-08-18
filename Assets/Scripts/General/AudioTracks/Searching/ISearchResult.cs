namespace General.AudioTracks.Searching
{
    public interface ISearchResult
    {
        string Title { get; set; }
        string Id { get; set; }
        string ThumbnailPath { get; set; }
    }
}