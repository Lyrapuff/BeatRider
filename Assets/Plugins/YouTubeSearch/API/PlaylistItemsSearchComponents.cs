namespace YouTubeSearch
{
    public class PlaylistItemsSearchComponents
    {
        private string Author;
        private string Duration;
        private string Thumbnail;
        private string Title;
        private string Url;

        public PlaylistItemsSearchComponents(string Title, string Author, string Duration, string Url, string Thumbnail)
        {
            setTitle(Title);
            setAuthor(Author);
            setDuration(Duration);
            setUrl(Url);
            setThumbnail(Thumbnail);
        }

        public string getTitle()
        {
            return Title;
        }

        public void setTitle(string title)
        {
            Title = title;
        }

        public string getAuthor()
        {
            return Author;
        }

        public void setAuthor(string author)
        {
            Author = author;
        }

        public string getDuration()
        {
            return Duration;
        }

        public void setDuration(string duration)
        {
            Duration = duration;
        }

        public string getUrl()
        {
            return Url;
        }

        public void setUrl(string url)
        {
            Url = url;
        }

        public string getThumbnail()
        {
            return Thumbnail;
        }

        public void setThumbnail(string thumbnail)
        {
            Thumbnail = thumbnail;
        }
    }
}