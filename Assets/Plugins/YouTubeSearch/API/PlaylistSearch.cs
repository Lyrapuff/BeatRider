using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YouTubeSearch
{
    public class PlaylistSearch
    {
        private static List<PlaylistSearchComponents> items;

        private static string Id;
        private static string Title;
        private static string Author;
        private static string VideoCount;
        private static string Thumbnail;
        private static string Url;

        public async Task<List<PlaylistSearchComponents>> GetPlaylists(string querystring, int querypages)
        {
            items = new List<PlaylistSearchComponents>();

            // Do search
            for (var i = 1; i <= querypages; i++)
            {
                // Search address
                var content = await Web.getContentFromUrlWithProperty(
                    "https://www.youtube.com/results?search_query=" + querystring.Replace(" ", "+") +
                    "&sp=EgIQAw%253D%253D&page=" + i);

                // Search string
                var pattern =
                    "playlistRenderer\":\\{\"playlistId\":\"(?<ID>.*?)\",\"title\":\\{\"simpleText\":\"(?<TITLE>.*?)\"},\"thumbnails\":\\[\\{\"thumbnails\":\\[\\{\"url\":\"(?<THUMBNAIL>.*?)\".*?videoCount\":\"(?<VIDEOCOUNT>.*?)\".*?\\{\"webCommandMetadata\":\\{\"url\":\"(?<URL>.*?)\".*?\"shortBylineText\":\\{\"runs\":\\[\\{\"text\":\"(?<AUTHOR>.*?)\"";
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

                    // Author
                    Author = result[ctr].Groups[6].Value;

                    if (Log.getMode())
                    {
                        Log.println(Helper.Folder, "Author: " + Author);
                    }

                    // VideoCount
                    VideoCount = result[ctr].Groups[4].Value;

                    if (Log.getMode())
                    {
                        Log.println(Helper.Folder, "VideoCount: " + VideoCount);
                    }

                    // Thumbnail
                    Thumbnail = result[ctr].Groups[3].Value;

                    if (Log.getMode())
                    {
                        Log.println(Helper.Folder, "Thumbnail: " + Thumbnail);
                    }

                    // Url
                    Url = "http://youtube.com" + result[ctr].Groups[5].Value.Replace(@"\u0026", "&");

                    if (Log.getMode())
                    {
                        Log.println(Helper.Folder, "Url: " + Url);
                    }

                    // Add item to list
                    items.Add(new PlaylistSearchComponents(Id, Utilities.HtmlDecode(Title),
                        Utilities.HtmlDecode(Author), VideoCount, Thumbnail, Url));
                }
            }

            return items;
        }

        public async Task<List<PlaylistSearchComponents>> GetPlaylistsPaged(string querystring, int querypagenum)
        {
            items = new List<PlaylistSearchComponents>();

            // Do search
            // Search address
            var content = await Web.getContentFromUrlWithProperty(
                "https://www.youtube.com/results?search_query=" + querystring.Replace(" ", "+") +
                "&sp=EgIQAw%253D%253D&page=" + querypagenum);

            // Search string
            var pattern =
                "playlistRenderer\":\\{\"playlistId\":\"(?<ID>.*?)\",\"title\":\\{\"simpleText\":\"(?<TITLE>.*?)\"},\"thumbnails\":\\[\\{\"thumbnails\":\\[\\{\"url\":\"(?<THUMBNAIL>.*?)\".*?videoCount\":\"(?<VIDEOCOUNT>.*?)\".*?\\{\"webCommandMetadata\":\\{\"url\":\"(?<URL>.*?)\".*?\"shortBylineText\":\\{\"runs\":\\[\\{\"text\":\"(?<AUTHOR>.*?)\"";
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

                // Author
                Author = result[ctr].Groups[6].Value;

                if (Log.getMode())
                {
                    Log.println(Helper.Folder, "Author: " + Author);
                }

                // VideoCount
                VideoCount = result[ctr].Groups[4].Value;

                if (Log.getMode())
                {
                    Log.println(Helper.Folder, "VideoCount: " + VideoCount);
                }

                // Thumbnail
                Thumbnail = result[ctr].Groups[3].Value;

                if (Log.getMode())
                {
                    Log.println(Helper.Folder, "Thumbnail: " + Thumbnail);
                }

                // Url
                Url = "http://youtube.com" + result[ctr].Groups[5].Value.Replace(@"\u0026", "&");

                if (Log.getMode())
                {
                    Log.println(Helper.Folder, "Url: " + Url);
                }

                // Add item to list
                items.Add(new PlaylistSearchComponents(Id, Utilities.HtmlDecode(Title),
                    Utilities.HtmlDecode(Author), VideoCount, Thumbnail, Url));
            }

            return items;
        }
    }
}