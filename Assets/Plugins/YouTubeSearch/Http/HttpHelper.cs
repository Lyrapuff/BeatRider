using System;
using System.Collections.Generic;
using System.Linq;
using MonoHttp;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine.Networking;

namespace YouTubeSearch
{
    internal static class HttpHelper
    {
        public static string DownloadString(string url)
        {
            using (UnityWebRequest client = UnityWebRequest.Get(url))
            {
                UnityWebRequestAsyncOperation req = client.SendWebRequest();

                while (!req.isDone)
                {
                    Thread.Sleep(100);
                }
                
                string text = client.downloadHandler.text;
                return text;
            }
        }

        public static string HtmlDecode(string value)
        {
            return HttpUtility.HtmlDecode(value);
        }

        public static IDictionary<string, string> ParseQueryString(string s)
        {
            // remove anything other than query string from url
            if (s.Contains("?"))
            {
                s = s.Substring(s.IndexOf('?') + 1);
            }

            var dictionary = new Dictionary<string, string>();

            foreach (var vp in Regex.Split(s, "&"))
            {
                var strings = Regex.Split(vp, "=");

                var key = strings[0];
                var value = string.Empty;

                if (strings.Length == 2)
                {
                    value = strings[1];
                }
                else if (strings.Length > 2)
                {
                    value = string.Join("=", strings.Skip(1).Take(strings.Length).ToArray());
                }

                dictionary.Add(key, value);
            }

            return dictionary;
        }

        public static string ReplaceQueryStringParameter(string currentPageUrl, string paramToReplace, string newValue)
        {
            var query = ParseQueryString(currentPageUrl);

            query[paramToReplace] = newValue;

            StringBuilder resultQuery = new StringBuilder();
            var isFirst = true;

            foreach (var pair in query)
            {
                if (!isFirst)
                {
                    resultQuery.Append("&");
                }

                resultQuery.Append(pair.Key);
                resultQuery.Append("=");
                resultQuery.Append(pair.Value);

                isFirst = false;
            }

            UriBuilder uriBuilder = new UriBuilder(currentPageUrl)
            {
                Query = resultQuery.ToString()
            };

            return uriBuilder.ToString();
        }

        public static string UrlDecode(string url)
        {
            return HttpUtility.UrlDecode(url);
        }
    }
}