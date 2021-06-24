using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SiteMapGenerator.Bll.BusinessLogic
{
    public class HtmlParser
    {
        public virtual IEnumerable<string> Parser(string urlName, string UserLink)
        {
            var doc = new HtmlWeb().Load(urlName);
            var linkTags = doc.DocumentNode.Descendants("link");

            var resultLink = doc.DocumentNode.Descendants("a")
                                    .Select(a => a.GetAttributeValue("href", null))
                                    .Where(u => !String.IsNullOrEmpty(u)).Distinct();

            return AnalyzeUrl(resultLink, UserLink);
        }

        private IEnumerable<string> AnalyzeUrl(IEnumerable<string> linkedPages, string UserLink)
        {
            var urlList = new List<string>();

            foreach (var item in linkedPages)
            {
                string absolut = GetAbsoluteUrl(UserLink, item);

                if (!urlList.Any(x => x.Contains(absolut)) && absolut.Contains(UserLink))
                {
                    if (!absolut.Contains("#"))
                    {
                        urlList.Add(absolut);
                    }
                }
            }

            return urlList.OrderBy(x => x.Length).Distinct().ToList();
        }

        public virtual string GetAbsoluteUrl(string baseUrl, string url)
        {
            var uri = new Uri(url, UriKind.RelativeOrAbsolute);
            if (!uri.IsAbsoluteUri)
            {
                uri = new Uri(new Uri(baseUrl), uri);
            }

            return uri.ToString();
        }
    }
}
