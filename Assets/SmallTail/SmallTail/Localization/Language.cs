using System;
using System.Collections.Generic;

namespace SmallTail.Localization
{
    [Serializable]
    public class Language
    {
        public string Key { get; set; }
        public List<string> Words { get; set; } = new List<string>();
    }
}