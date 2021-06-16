using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SiteMapGenerator.Bll.BusinessLogic
{
    public class LoadingPageUrls
    {
        private readonly LinkValidator _linkCheck;

        public LoadingPageUrls(
            LinkValidator linkCheck)
        {
            _linkCheck = linkCheck;
        }

        public virtual List<string> ExtractHref(string URL, int countLink)
        {
            var linksResult = SearchForLinks(URL, HtmlParser(URL));
            int i = 0;

            do
            {
                int indexList = 1 + i;

                if (indexList >= countLink)
                    break;

                string selectUriList = linksResult[indexList];

                if (_linkCheck.UrlValidation(selectUriList))
                {
                    var resultHtmlParser = HtmlParser(selectUriList);
                    linksResult.AddRange(SearchForLinks(selectUriList, resultHtmlParser));
                }
                else
                {
                    linksResult.RemoveAt(indexList);
                }

            } while (countLink >= linksResult.Count());

            var result = linksResult.Distinct().ToList();

            return linksResult.OrderBy(x => x.Length).ToList();
        }

        private IEnumerable<string> HtmlParser(string urlName)
        {
            var doc = new HtmlWeb().Load(urlName);
            var linkTags = doc.DocumentNode.Descendants("link");

            return doc.DocumentNode.Descendants("a")
                                    .Select(a => a.GetAttributeValue("href", null))
                                    .Where(u => !String.IsNullOrEmpty(u));
        }

        private List<string> SearchForLinks(string url, IEnumerable<string> linkedPages)
        {
            var urlList = new List<string> { url };

            string http = SelectHttp(url);

            string urls = url.Replace(http, string.Empty);
            string result = urls.Trim(new char[] { '/', ':' });

            foreach (var item in linkedPages)
            {
                if (urlList.Any(x => x.Contains(item)) == false)
                {
                    if (item.Contains(url))
                    {
                        urlList.Add(item);
                    }
                    else if (item.Contains(result) && item.Contains("//"))
                    {
                        urlList.Add($"{http}{item.Replace("//", string.Empty)}");
                    }
                    else if (item.StartsWith("http://") == false && item.StartsWith("https://") == false)
                    {
                        urlList.Add(url + item.TrimStart(new char[] { '/' }));
                    }
                }
            }

            return urlList.OrderBy(x => x.Length).ToList();
        }

        private string SelectHttp(string url) => url.Contains("http://") ? "http://" : "https://";
    }
}
