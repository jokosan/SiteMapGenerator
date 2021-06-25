using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SiteMapGenerator.Bll.BusinessLogic
{
    public class HtmlParser
    {
        private readonly LinkValidator _linkValidator;

        public HtmlParser(LinkValidator linkValidator)
        {
            _linkValidator = linkValidator;
        }

        public virtual IEnumerable<string> GetAllPageLinks(string urlName)
        {
            var doc = new HtmlWeb().Load(urlName);
            var linkTags = doc.DocumentNode.Descendants("link");

            var resultLink = doc.DocumentNode.Descendants("a")
                                    .Select(a => a.GetAttributeValue("href", null))
                                    .Where(u => !String.IsNullOrEmpty(u)).Distinct();

            return AnalyzeUrl(resultLink, urlName);
        }

        private IEnumerable<string> AnalyzeUrl(IEnumerable<string> linkedPages, string userLink)
        {
            var urlList = new List<string>();
            userLink = _linkValidator.GetHost(userLink) + "/";

            foreach (var item in linkedPages)
            {
                string absolut = GetAbsoluteUrl(userLink, item);

                if (!urlList.Any(x => x.Contains(absolut)) && absolut.Contains(userLink))
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
