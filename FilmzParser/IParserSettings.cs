using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using FilmzParser.Common;

namespace FilmzParser
{
    public interface IParserSettings
    {
        string SiteUrl { get; set; }
        string[] PathForParsing { get; set; }
        Dictionary<string, Func<string[], IFilmInfo, bool>> Selectors { get; set; }
        SelectionRule Selection { get; set; }
        
    }
}