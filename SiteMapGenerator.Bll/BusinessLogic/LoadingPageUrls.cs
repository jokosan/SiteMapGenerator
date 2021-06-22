using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SiteMapGenerator.Bll.BusinessLogic
{
    public class LoadingPageUrls
    {
        private readonly LinkValidator _linkValidator;
        private readonly Parser _parser;

        public LoadingPageUrls(
            LinkValidator linkValidator,
            Parser parser)
        {
            _linkValidator = linkValidator;
            _parser = parser;
        }

        public virtual List<string> ExtractHref(string URL, int countLink)
        {
            var linksResult = AnalyzeUrl(URL, _parser.HtmlParser(URL), URL);
            int i = 0;

            while (countLink >= linksResult.Count())
            {
                int indexList = 1 + i;

                if (indexList >= countLink)
                    break;

                string selectUriList = linksResult[indexList];

                if (_linkValidator.CheckURLValid(selectUriList))
                {
                    var resultHtmlParser = _parser.HtmlParser(selectUriList);
                    linksResult.AddRange(AnalyzeUrl(selectUriList, resultHtmlParser, URL));
                    linksResult.Distinct();
                }
                else
                {
                    linksResult.RemoveAt(indexList);
                }
            }

            return linksResult.OrderBy(x => x.Length).ToList();
        }

        private List<string> AnalyzeUrl(string url, IEnumerable<string> linkedPages, string urlConst)
        {
            var urlList = new List<string> { url };

            foreach (var item in linkedPages)
            {
                string absolut = _parser.GetAbsoluteUrlString(urlConst, item);

                if (!urlList.Any(x => x.Contains(absolut)) && absolut.Contains(urlConst))
                {
                    urlList.Add(absolut);
                }
            }

            return urlList.OrderBy(x => x.Length).ToList();
        }
    }
}
