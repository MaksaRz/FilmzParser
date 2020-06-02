
using AngleSharp.Html.Dom;

namespace FilmzParser
{
    public interface IParser<T> where T:class
    {
        T Parse(IHtmlDocument document);
        
    }
}