using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using FilmzParser.Rutor;

namespace FilmzParser
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Test();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            RutorSettings  settings = new RutorSettings();
            var result = await new RutorParser().Parse(settings);
            
         
        }

        static async Task Test()
        {
            var context = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
            Func<int, string> handler = t => @"http://top.new-rutor.org/search/0/1/000/0/avatar";

            int pageIndex = 1;
            
            var doc1 = await context.OpenAsync(handler(pageIndex));
            var doc2 = context.Active;
            var rez = doc2.QuerySelectorAll("#index tbody td:nth-child(2) a:nth-child(3)");
            var rez2 = rez.Select(l => l as IHtmlAnchorElement);

           
        }
     }


}