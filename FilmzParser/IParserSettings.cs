using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace FilmzParser
{
    public interface IParserSettings
    {
        string SiteUrl { get; set; }
        string[] PathForParsing { get; set; }
        Dictionary<string, Func<string[], IFilmInfo, bool>> SelectionRules { get; set; }
        Dictionary<string,string> Selection { get; set; }
    }
}