
using System.Threading.Tasks;
using AngleSharp.Html.Dom;

namespace FilmzParser
{
    public interface IParser<T> where T:class
    {
        Task<string[]> Parse(IParserSettings settings);
        
    }
}