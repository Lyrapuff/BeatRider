using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YouTubeSearch
{
    public class VideoSearch
    {
        private static List<VideoSearchComponents> items;

        private static string title;
        private static string author;
        private static string description;
        private static string duration;
        private static string url;
        private static string thumbnail;
        private static string viewcount;

        /// <summary>
        ///     Search videos
        /// </summary>
        /// <param name="querystring"></param>
        /// <param name="querypages"></param>
        /// <returns></returns>
        public async Task<List<VideoSearchComponents>> GetVideos(string querystring, int querypages)
        {
            items = new List<VideoSearchComponents>();

            // Do search
            for (var i = 1; i <= querypages; i++)
            {
                // Search address
                var content =
                    await Web.getContentFromUrl("https://www.youtube.com/results?search_query=" + querystring +
                                                "&page=" + i);

                // Search string
                var pattern = "<div class=\"yt-lockup-content\">.*?title=\"(?<NAME>.*?)\".*?</div></div></div></li>";
                MatchCollection result = Regex.Matches(content, pattern, RegexOptions.Singleline);

                for (var ctr = 0; ctr <= result.Count - 1; ctr++)
                {
                    if (Log.getMode())
                    {
                        Log.println(Helper.Folder, "Match: " + result[ctr].Value);
                    }

                    // Title
                    title = result[ctr].Groups[1].Value;

                    if (Log.getMode())
                    {
                        Log.println(Helper.Folder, "Title: " + title);
                    }

                    // Author
                    author = Helper.ExtractValue(result[ctr].Value, "/user/", "class").Replace('"', ' ').TrimStart()
                        .TrimEnd();

                    if (string.IsNullOrEmpty(author))
                    {
                        author = Helper.ExtractValue(result[ctr].Value, " >", "</a>");
                    }

                    if (Log.getMode())
                    {
                        Log.println(Helper.Folder, "Author: " + author);
                    }

                    // Description
                    description = Helper.ExtractValue(result[ctr].Value, "dir=\"ltr\" class=\"yt-uix-redirect-link\">",
                        "</div>");

                    if (string.IsNullOrEmpty(description.Trim()))
                    {
                        description = Helper.ExtractValue(result[ctr].Value,
                            "<div class=\"yt-lockup-description yt-ui-ellipsis yt-ui-ellipsis-2\" dir=\"ltr\">",
                            "</div>");
                    }

                    if (Log.getMode())
                    {
                        Log.println(Helper.Folder, "Description: " + description);
                    }

                    // Duration
                    duration = Helper
                        .ExtractValue(Helper.ExtractValue(result[ctr].Value, "id=\"description-id-", "span"), ": ", "<")
                        .Replace(".", "");

                    if (Log.getMode())
                    {
                        Log.println(Helper.Folder, "Duration: " + duration);
                    }

                    // Url
                    url = string.Concat("http://www.youtube.com/watch?v=",
                        Helper.ExtractValue(result[ctr].Value, "watch?v=", "\""));

                    if (Log.getMode())
                    {
                        Log.println(Helper.Folder, "Url: " + url);
                    }

                    // Thumbnail
                    thumbnail = "https://i.ytimg.com/vi/" + Helper.ExtractValue(result[ctr].Value, "watch?v=", "\"") +
                                "/mqdefault.jpg";

                    if (Log.getMode())
                    {
                        Log.println(Helper.Folder, "Thumbnail: " + thumbnail);
                    }

                    // View count
                    {
                        var strView = Helper.ExtractValue(result[ctr].Value, "</li><li>", "</li></ul></div>");
                        if (!string.IsNullOrEmpty(strView) && !string.IsNullOrWhiteSpace(strView))
                        {
                            var strParsedArr =
                                strView.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);

                            var parsedText = strParsedArr[0];
                            parsedText = parsedText.Trim().Replace(",", ".");

                            viewcount = parsedText;
                        }
                    }

                    if (Log.getMode())
                    {
                        Log.println(Helper.Folder, "Viewcount: " + viewcount);
                    }

                    // Remove playlists
                    if (title != "__title__" && title != " ")
                    {
                        if (duration != "" && duration != " ")
                        {
                            // Add item to list
                            items.Add(new VideoSearchComponents(Utilities.HtmlDecode(title),
                                Utilities.HtmlDecode(author), Utilities.HtmlDecode(description), duration, url,
                                thumbnail, viewcount));
                        }
                    }
                }
            }

            return items;
        }

        /// <summary>
        ///     Search videos paged
        /// </summary>
        /// <param name="querystring"></param>
        /// <param name="querypage"></param>
        /// <returns></returns>
        public async Task<List<VideoSearchComponents>> GetVideosPaged(string querystring, int querypagenum)
        {
            items = new List<VideoSearchComponents>();

            // Do search
            // Search address
            var content = await Web.getContentFromUrl("https://www.youtube.com/results?search_query=" + querystring +
                                                      "&page=" + querypagenum);

            // Search string
            var pattern = "<div class=\"yt-lockup-content\">.*?title=\"(?<NAME>.*?)\".*?</div></div></div></li>";
            MatchCollection result = Regex.Matches(content, pattern, RegexOptions.Singleline);

            for (var ctr = 0; ctr <= result.Count - 1; ctr++)
            {
                if (Log.getMode())
                {
                    Log.println(Helper.Folder, "Match: " + result[ctr].Value);
                }

                // Title
                title = result[ctr].Groups[1].Value;

                if (Log.getMode())
                {
                    Log.println(Helper.Folder, "Title: " + title);
                }

                // Author
                author = Helper.ExtractValue(result[ctr].Value, "/user/", "class").Replace('"', ' ').TrimStart()
                    .TrimEnd();

                if (string.IsNullOrEmpty(author))
                {
                    author = Helper.ExtractValue(result[ctr].Value, " >", "</a>");
                }

                if (Log.getMode())
                {
                    Log.println(Helper.Folder, "Author: " + author);
                }

                // Description
                description = Helper.ExtractValue(result[ctr].Value, "dir=\"ltr\" class=\"yt-uix-redirect-link\">",
                    "</div>");

                if (string.IsNullOrEmpty(description.Trim()))
                {
                    description = Helper.ExtractValue(result[ctr].Value,
                        "<div class=\"yt-lockup-description yt-ui-ellipsis yt-ui-ellipsis-2\" dir=\"ltr\">", "</div>");
                }

                if (Log.getMode())
                {
                    Log.println(Helper.Folder, "Description: " + description);
                }

                // Duration
                duration = Helper
                    .ExtractValue(Helper.ExtractValue(result[ctr].Value, "id=\"description-id-", "span"), ": ", "<")
                    .Replace(".", "");

                if (Log.getMode())
                {
                    Log.println(Helper.Folder, "Duration: " + duration);
                }

                // Url
                url = string.Concat("http://www.youtube.com/watch?v=",
                    Helper.ExtractValue(result[ctr].Value, "watch?v=", "\""));

                if (Log.getMode())
                {
                    Log.println(Helper.Folder, "Url: " + url);
                }

                // Thumbnail
                thumbnail = "https://i.ytimg.com/vi/" + Helper.ExtractValue(result[ctr].Value, "watch?v=", "\"") +
                            "/mqdefault.jpg";

                if (Log.getMode())
                {
                    Log.println(Helper.Folder, "Thumbnail: " + thumbnail);
                }

                // View count
                {
                    var strView = Helper.ExtractValue(result[ctr].Value, "</li><li>", "</li></ul></div>");
                    if (!string.IsNullOrEmpty(strView) && !string.IsNullOrWhiteSpace(strView))
                    {
                        var strParsedArr =
                            strView.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);

                        var parsedText = strParsedArr[0];
                        parsedText = parsedText.Trim().Replace(",", ".");

                        viewcount = parsedText;
                    }
                }

                if (Log.getMode())
                {
                    Log.println(Helper.Folder, "Viewcount: " + viewcount);
                }

                // Remove playlists
                if (title != "__title__" && title != " ")
                {
                    if (duration != "" && duration != " ")
                    {
                        // Add item to list
                        items.Add(new VideoSearchComponents(Utilities.HtmlDecode(title),
                            Utilities.HtmlDecode(author), Utilities.HtmlDecode(description), duration, url, thumbnail,
                            viewcount));
                    }
                }
            }

            return items;
        }
    }
}