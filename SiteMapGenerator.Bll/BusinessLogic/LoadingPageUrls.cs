using System.Collections.Generic;
using System.Linq;

namespace SiteMapGenerator.Bll.BusinessLogic
{
    public class LoadingPageUrls
    {
        private readonly HtmlParser _htmlParser;

        public LoadingPageUrls(
            HtmlParser htmlParser)
        {
            _htmlParser = htmlParser;
        }

        public virtual List<string> ExtractHref(string URL)
        {
            var listurlResult = new List<string>() { URL };
            var searchLinks = new List<string>() { URL };
            var resultParsrer = new List<string>();

            while (searchLinks.Count() != 0)
            {
                foreach (var item in searchLinks)
                {
                    resultParsrer.AddRange(_htmlParser.Parser(item, URL));
                }

                searchLinks = resultParsrer.Except(listurlResult).ToList();
                listurlResult.AddRange(searchLinks);
            }

            return listurlResult.OrderBy(x => x.Length).Distinct().ToList();
        }              
    }
}