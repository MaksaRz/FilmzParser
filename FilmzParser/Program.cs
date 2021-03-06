﻿using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
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
            var filmInfoTables = document.All.Where(t => t.LocalName == "table" && t.Id == "details");
            var filmInfoTable2 = document.QuerySelector<IHtmlTableElement>("table#details");
            if (filmInfoTables.Count() == 1)
            {
                IHtmlTableElement filmInfoTable = (IHtmlTableElement)filmInfoTables.ElementAt(0);
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

                //Год выпуска: 1999
                //Страна: США
                //Жанр: детектив, криминал, драма, триллер
                //Продолжительность: 01:28:45
            }

         
        }
    }


}