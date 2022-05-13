using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace SerpScraper
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Welcome To Google SERP Scraper");

            var results = ScrapeSerp("what+is+programming", 3);

            foreach (var result in results)
            {
                Console.WriteLine(result.Title);
                Console.WriteLine(result.Url);

            }


            Console.ReadLine();

        }



        public static List<serpResult> ScrapeSerp(string query, int n_pages)
        {
            var serpResults = new List<serpResult>();

            for (var i = 1; i <= n_pages; i++)
            {
                var url = "http://www.google.com/search?q=" + query + " &num=100&start=" + ((i - 1) * 10).ToString();
                HtmlWeb web = new HtmlWeb();
                web.UserAgent = "user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36";
                
                var htmldoc = web.Load(url);

                HtmlNodeCollection Nodes = htmldoc.DocumentNode.SelectNodes("//div[@class='yuRUbf']");


                foreach (var tag in Nodes)
                {
                    var result = new serpResult();

                    result.Url = tag.Descendants("a").FirstOrDefault().Attributes["href"].Value;
                    result.Title = tag.Descendants("h3").FirstOrDefault().InnerText;


                    serpResults.Add(result);

                }
                

            }
            

            return serpResults;
        }

        public class serpResult
        {
            public string Url { get; set; }
            public string Title { get; set; }
        }


    }
}
