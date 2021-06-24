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

        public virtual List<string> ExtractHref(string URL)
        {
            var listurlResult = new List<string>() { URL };
            var searchLinks = new List<string>() { URL };
            var resultParsrer = new List<string>();

            while (searchLinks.Count() != 0)
            {
                foreach (var item in searchLinks)
                {
                    resultParsrer.AddRange(AnalyzeUrl(_parser.HtmlParser(item), URL));
                }

                searchLinks = resultParsrer.Except(listurlResult).ToList();
                listurlResult.AddRange(searchLinks);
            }

            return listurlResult.OrderBy(x => x.Length).Distinct().ToList();
        }

        private List<string> AnalyzeUrl(IEnumerable<string> linkedPages, string urlConst)
        {
            var urlList = new List<string>();

            foreach (var item in linkedPages)
            {
                string absolut = _parser.GetAbsoluteUrlString(urlConst, item);

                if (!urlList.Any(x => x.Contains(absolut)) && absolut.Contains(urlConst))
                {
                    if (!absolut.Contains("#"))
                    {
                        urlList.Add(absolut);
                    }
                }
            }

            return urlList.OrderBy(x => x.Length).Distinct().ToList();
        }
    }
}
