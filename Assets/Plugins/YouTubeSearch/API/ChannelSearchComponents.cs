namespace YouTubeSearch
{
    public class ChannelSearchComponents
    {
        private string Description;
        private string Id;
        private string SubscriberCount;
        private string Thumbnail;
        private string Title;
        private string Url;
        private string VideoCount;

        public ChannelSearchComponents(string Id, string Title, string Description, string VideoCount,
            string SubscriberCount, string Url, string Thumbnail)
        {
            setId(Id);
            setTitle(Title);
            setDescription(Description);
            setVideoCount(VideoCount);
            setSubscriberCount(SubscriberCount);
            setUrl(Url);
            setThumbnail(Thumbnail);
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

        public string getDescription()
        {
            return Description;
        }

        public void setDescription(string description)
        {
            Description = description;
        }

        public string getVideoCount()
        {
            return VideoCount;
        }

        public void setVideoCount(string videocount)
        {
            VideoCount = videocount;
        }

        public string getSubscriberCount()
        {
            return SubscriberCount;
        }

        public void setSubscriberCount(string subscribercount)
        {
            SubscriberCount = subscribercount;
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