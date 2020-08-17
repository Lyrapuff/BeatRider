namespace YouTubeSearch
{
    public class VideoSearchComponents
    {
        private string Author;
        private string Description;
        private string Duration;
        private string Thumbnail;
        private string Title;
        private string Url;
        private string ViewCount;

        public VideoSearchComponents(string Title, string Author, string Description, string Duration, string Url,
            string Thumbnail, string ViewCount)
        {
            setTitle(Title);
            setAuthor(Author);
            setDescription(Description);
            setDuration(Duration);
            setUrl(Url);
            setThumbnail(Thumbnail);
            setViewCount(ViewCount);
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

        public string getDescription()
        {
            return Description;
        }

        public void setDescription(string description)
        {
            Description = description;
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

        public string getViewCount()
        {
            return ViewCount;
        }

        public void setViewCount(string viewcount)
        {
            ViewCount = viewcount;
        }
    }
}