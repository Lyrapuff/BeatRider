using System;
using System.Collections.Generic;

namespace SmallTail.Localization
{
    [Serializable]
    public class Locale
    {
        public List<string> Keys { get; set; } = new List<string>();
        public List<Language> Languages { get; set; } = new List<Language>();
    }
}