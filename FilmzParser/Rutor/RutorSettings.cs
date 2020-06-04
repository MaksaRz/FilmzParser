using System;
using System.Collections.Generic;
using FilmzParser.Common;

namespace FilmzParser.Rutor
{
    public class RutorSettings:IParserSettings
    {
        public string SiteUrl { get; set; } = "http://top.new-rutor.org/";
        public string[] PathForParsing { get; set; } = new string[]{"kino", "nashe_kino"};
        public Dictionary<string, Func<string[], IFilmInfo, bool>> Selectors { get; set; }
        public SelectionRule Selection { get; set; }
    }
}