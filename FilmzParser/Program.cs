using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace FilmzParser
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var config = Configuration.Default;

            //Create a new context for evaluating webpages with the given config
            var context = BrowsingContext.New(config);

            //Parse the document from the content of a response to a virtual request
            IDocument document = await context.OpenAsync(req => req.Content(new StreamReader("..\\..\\..\\..\\TestParser\\filmPage.html").BaseStream));
            IHtmlTableElement filmInfoTable = (IHtmlTableElement)document.All.First(t => t.LocalName == "table" && t.Id == "details");
            
            var htmlTableCellElement = filmInfoTable.Rows[0].Cells[1];
            var boldElements = htmlTableCellElement.ChildNodes.Where(t => t is IHtmlElement && (t as IHtmlElement).NodeName == "B").ToArray();
            INode boldElName     = boldElements.FirstOrDefault(t => t.TextContent.Contains("Название"));
            INode boldElYear     = boldElements.FirstOrDefault(t => t.TextContent.Contains("Год"));
            INode boldElCountry  = boldElements.FirstOrDefault(t => t.TextContent.Contains("Страна"));
            INode boldElGenres   = boldElements.FirstOrDefault(t => t.TextContent.Contains("Жанр"));
            INode boldElDuration = boldElements.FirstOrDefault(t => t.TextContent.Contains("Продолжительность"));
            string filmTitle    = boldElName?.NextSibling.TextContent;
            string filmYear     = boldElYear?.NextSibling.TextContent;
            string filmCountry  = boldElCountry?.NextSibling.TextContent;
            string filmGenres   = boldElGenres?.NextSibling.TextContent;
            string filmDuration = boldElDuration?.NextSibling.TextContent;

            var AnchorElements = htmlTableCellElement.ChildNodes.Where(t => t is IHtmlAnchorElement && (t as IHtmlAnchorElement).NodeName == "A").ToArray();
            IHtmlAnchorElement anchorElKinopoisk = AnchorElements.FirstOrDefault(t => (t as IHtmlAnchorElement).Origin.Contains(("kinopoisk"))) as IHtmlAnchorElement;
            IHtmlAnchorElement anchorElIMDB      = AnchorElements.FirstOrDefault(t => (t as IHtmlAnchorElement).Origin.Contains("imdb")) as IHtmlAnchorElement;
            (string kpVotes, string kpRaiting, string imdbVotes, string imdbRaiting)  = await GetRaitings(anchorElKinopoisk,
                anchorElIMDB);
            

         
        }
        
        private static async Task<(string kpVotes, string kpRaiting, string imdbVotes, string imdbRaiting)> GetRaitings(IHtmlAnchorElement elKp, IHtmlAnchorElement elImdb)
        {
            //https://rating.kinopoisk.ru/4169.xml
            string kpVotes = "", kpRaiting = "", imdbVotes = "", imdbRaiting = "";
            if (elKp != null)
            {
                string filmIdKp = elKp.PathName.Replace("film", "").Replace("/", "");
                string responseBody = "";
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync($"https://rating.kinopoisk.ru/{filmIdKp}.xml").ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();
                    responseBody = await response.Content.ReadAsStringAsync();
                }
                
               XDocument xd = XDocument.Parse(responseBody);
               var kp   = xd.XPathSelectElement("rating/kp_rating ");
               var imdb = xd.XPathSelectElement("rating/imdb_rating ");
               kpRaiting     = kp?.Value ?? "";
               kpVotes       = kp?.Attribute("num_vote")?.Value ?? "";
               imdbRaiting   = imdb?.Value ?? "";
               imdbVotes     = imdb?.Attribute("num_vote")?.Value ?? "";

            }
            else if (elImdb != null)
            {
                kpVotes= "";
                kpRaiting= "";
                imdbVotes= "";
                imdbRaiting = "";  
            }

            return (kpVotes, kpRaiting, imdbVotes, imdbRaiting);
        }
    }


}