namespace YouTubeSearch
{
    public class PlaylistSearchComponents
    {
        private string Author;
        private string Id;
        private string Thumbnail;
        private string Title;
        private string Url;
        private string VideoCount;

        public PlaylistSearchComponents(string Id, string Title, string Author, string VideoCount, string Thumbnail,
            string Url)
        {
            setId(Id);
            setTitle(Title);
            setAuthor(Author);
            setVideoCount(VideoCount);
            setThumbnail(Thumbnail);
            setUrl(Url);
        }

        public string getId()
        {
            return Id;
        }

        public void setId(string id)
        {
            Id = id;
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

        public string getVideoCount()
        {
            return VideoCount;
        }

        public void setVideoCount(string videocount)
        {
            VideoCount = videocount;
        }

        public string getThumbnail()
        {
            return Thumbnail;
        }

        public void setThumbnail(string thumbnail)
        {
            Thumbnail = thumbnail;
        }

        public string getUrl()
        {
            return Url;
        }

        public void setUrl(string url)
        {
            Url = url;
        }
    }
}