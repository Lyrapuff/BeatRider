using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YouTubeSearch
{
    public class PlaylistItemsSearch
    {
        private static List<PlaylistItemsSearchComponents> items;
        private string Author;
        private string Duration;
        private string Thumbnail;

        private string Title;
        private string Url;

        public async Task<List<PlaylistItemsSearchComponents>> GetPlaylistItems(string Playlisturl)
        {
            items = new List<PlaylistItemsSearchComponents>();

            // Do search
            // Search address
            var content = await Web.getContentFromUrlWithProperty(Playlisturl);

            // Search string
            var pattern =
                "playlistPanelVideoRenderer\":\\{\"title\":\\{\"simpleText\":\"(?<TITLE>.*?)\".*?runs\":\\[\\{\"text\":\"(?<AUTHOR>.*?)\".*?\":\\{\"thumbnails\":\\[\\{\"url\":\"(?<THUMBNAIL>.*?)\".*?\"}},\"simpleText\":\"(?<DURATION>.*?)\".*?videoId\":\"(?<URL>.*?)\"";
            MatchCollection result = Regex.Matches(content, pattern, RegexOptions.Singleline);

            for (var ctr = 0; ctr <= result.Count - 1; ctr++)
            {
                if (Log.getMode())
                {
                    Log.println(Helper.Folder, "Match: " + result[ctr].Value);
                }

                // Title
                Title = result[ctr].Groups[1].Value;

                if (Log.getMode())
                {
                    Log.println(Helper.Folder, "Title: " + Title);
                }

                // Author
                Author = result[ctr].Groups[2].Value;

                if (Log.getMode())
                {
                    Log.println(Helper.Folder, "Author: " + Author);
                }

                // Duration
                Duration = result[ctr].Groups[4].Value;

                if (Log.getMode())
                {
                    Log.println(Helper.Folder, "Duration: " + Duration);
                }

                // Thumbnail
                Thumbnail = result[ctr].Groups[3].Value;

                if (Log.getMode())
                {
                    Log.println(Helper.Folder, "Thumbnail: " + Thumbnail);
                }

                // Url
                Url = "http://youtube.com/watch?v=" + result[ctr].Groups[5].Value;

                if (Log.getMode())
                {
                    Log.println(Helper.Folder, "Url: " + Url);
                }

                // Add item to list
                items.Add(new PlaylistItemsSearchComponents(Utilities.HtmlDecode(Title),
                    Utilities.HtmlDecode(Author), Duration, Url, Thumbnail));
            }

            return items;
        }
    }
}