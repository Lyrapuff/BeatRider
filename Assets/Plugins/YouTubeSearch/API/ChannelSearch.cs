using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YouTubeSearch
{
    public class ChannelSearch
    {
        private static List<ChannelSearchComponents> items;
        private string Description;

        private string Id;
        private string SubscriberCount;
        private string Thumbnail;
        private string Title;
        private string Url;
        private string VideoCount;

        public async Task<List<ChannelSearchComponents>> GetChannels(string querystring, int querypages)
        {
            items = new List<ChannelSearchComponents>();

            // Do search
            for (var i = 1; i <= querypages; i++)
            {
                // Search address
                var content = await Web.getContentFromUrlWithProperty(
                    "https://www.youtube.com/results?search_query=" + querystring.Replace(" ", "+") +
                    "&sp=EgIQAg%253D%253D&page=" + i);

                content = Helper.ExtractValue(content, "window[\"ytInitialData\"]",
                    "window[\"ytInitialPlayerResponse\"]");

                // Search string
                var pattern =
                    "channelRenderer\":\\{\"channelId\":\"(?<ID>.*?)\",\"title\":{\"simpleText\":\"(?<TITLE>.*?)\".*?\"canonicalBaseUrl\":\"(?<URL>.*?)\"}}.*?\\{\"thumbnails\":\\[\\{\"url\":\"(?<THUMBNAIL>.*?)\".*?videoCountText\":\\{\"runs\":\\[\\{\"text\":\"(?<VIDEOCOUNT>.*?)\".*?clickTrackingParams";
                MatchCollection result = Regex.Matches(content, pattern, RegexOptions.Singleline);

                for (var ctr = 0; ctr <= result.Count - 1; ctr++)
                {
                    if (Log.getMode())
                    {
                        Log.println(Helper.Folder, "Match: " + result[ctr].Value);
                    }

                    // Id
                    Id = result[ctr].Groups[1].Value;

                    if (Log.getMode())
                    {
                        Log.println(Helper.Folder, "Id: " + Id);
                    }

                    // Title
                    Title = result[ctr].Groups[2].Value;

                    if (Log.getMode())
                    {
                        Log.println(Helper.Folder, "Title: " + Title);
                    }

                    // Description
                    Description = Helper.ExtractValue(result[ctr].Value,
                        "\"descriptionSnippet\":{\"runs\":[{\"text\":\"", "\"}]},");

                    if (Log.getMode())
                    {
                        Log.println(Helper.Folder, "Description: " + Description);
                    }

                    // VideoCount
                    VideoCount = result[ctr].Groups[5].Value;

                    if (VideoCount.Contains(" ")) // -> 1 Video
                    {
                        VideoCount = VideoCount.Replace(" Video", "");
                    }

                    if (Log.getMode())
                    {
                        Log.println(Helper.Folder, "VideoCount: " + VideoCount);
                    }

                    // SubscriberCount
                    SubscriberCount = Helper.ExtractValue(result[ctr].Value,
                        "\"subscriberCountText\":{\"simpleText\":\"", " ");

                    if (Log.getMode())
                    {
                        Log.println(Helper.Folder, "SubscriberCount: " + SubscriberCount);
                    }

                    // Thumbnail
                    Thumbnail = "https:" + result[ctr].Groups[4].Value;

                    if (Log.getMode())
                    {
                        Log.println(Helper.Folder, "Thumbnail: " + Thumbnail);
                    }

                    // Url
                    Url = "http://youtube.com" + result[ctr].Groups[3].Value;

                    if (Log.getMode())
                    {
                        Log.println(Helper.Folder, "Url: " + Url);
                    }

                    // Add item to list
                    items.Add(new ChannelSearchComponents(Id, Utilities.HtmlDecode(Title),
                        Utilities.HtmlDecode(Description), VideoCount, SubscriberCount, Url, Thumbnail));
                }
            }

            return items;
        }

        public async Task<List<ChannelSearchComponents>> GetChannelsPaged(string querystring, int querypagenum)
        {
            items = new List<ChannelSearchComponents>();

            // Do search
            // Search address
            var content = await Web.getContentFromUrlWithProperty(
                "https://www.youtube.com/results?search_query=" + querystring.Replace(" ", "+") +
                "&sp=EgIQAg%253D%253D&page=" + querypagenum);

            content = Helper.ExtractValue(content, "window[\"ytInitialData\"]", "window[\"ytInitialPlayerResponse\"]");

            // Search string
            var pattern =
                "channelRenderer\":\\{\"channelId\":\"(?<ID>.*?)\",\"title\":{\"simpleText\":\"(?<TITLE>.*?)\".*?\"canonicalBaseUrl\":\"(?<URL>.*?)\"}}.*?\\{\"thumbnails\":\\[\\{\"url\":\"(?<THUMBNAIL>.*?)\".*?videoCountText\":\\{\"runs\":\\[\\{\"text\":\"(?<VIDEOCOUNT>.*?)\".*?clickTrackingParams";
            MatchCollection result = Regex.Matches(content, pattern, RegexOptions.Singleline);

            for (var ctr = 0; ctr <= result.Count - 1; ctr++)
            {
                if (Log.getMode())
                {
                    Log.println(Helper.Folder, "Match: " + result[ctr].Value);
                }

                // Id
                Id = result[ctr].Groups[1].Value;

                if (Log.getMode())
                {
                    Log.println(Helper.Folder, "Id: " + Id);
                }

                // Title
                Title = result[ctr].Groups[2].Value;

                if (Log.getMode())
                {
                    Log.println(Helper.Folder, "Title: " + Title);
                }

                // Description
                Description = Helper.ExtractValue(result[ctr].Value, "\"descriptionSnippet\":{\"runs\":[{\"text\":\"",
                    "\"}]},");

                if (Log.getMode())
                {
                    Log.println(Helper.Folder, "Description: " + Description);
                }

                // VideoCount
                VideoCount = result[ctr].Groups[5].Value;

                if (VideoCount.Contains(" ")) // -> 1 Video
                {
                    VideoCount = VideoCount.Replace(" Video", "");
                }

                if (Log.getMode())
                {
                    Log.println(Helper.Folder, "VideoCount: " + VideoCount);
                }

                // SubscriberCount
                SubscriberCount =
                    Helper.ExtractValue(result[ctr].Value, "\"subscriberCountText\":{\"simpleText\":\"", " ");

                if (Log.getMode())
                {
                    Log.println(Helper.Folder, "SubscriberCount: " + SubscriberCount);
                }

                // Thumbnail
                Thumbnail = "https:" + result[ctr].Groups[4].Value;

                if (Log.getMode())
                {
                    Log.println(Helper.Folder, "Thumbnail: " + Thumbnail);
                }

                // Url
                Url = "http://youtube.com" + result[ctr].Groups[3].Value;

                if (Log.getMode())
                {
                    Log.println(Helper.Folder, "Url: " + Url);
                }

                // Add item to list
                items.Add(new ChannelSearchComponents(Id, Utilities.HtmlDecode(Title),
                    Utilities.HtmlDecode(Description), VideoCount, SubscriberCount, Url, Thumbnail));
            }

            return items;
        }
    }
}