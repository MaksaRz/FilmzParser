using System;
using System.Collections.Generic;

namespace FilmzParser.Rutor
{
    public class RutorSettings:IParserSettings
    {
        public string SiteUrl { get; set; } = "http://1.xrutor.org/";
        public string[] PathForParsing { get; set; } = new string[]{"kino", "nashe_kino"};
        public Dictionary<string, Func<string[], IFilmInfo, bool>> SelectionRules { get; set; }
        public Dictionary<string, string> Selection { get; set; }
    }
}