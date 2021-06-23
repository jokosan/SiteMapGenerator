using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteMapGenerator.Bll.BusinessLogic
{
    public class Parser
    {
        public IEnumerable<string> HtmlParser(string urlName)
        {
            var doc = new HtmlWeb().Load(urlName);
            var linkTags = doc.DocumentNode.Descendants("link");

            return doc.DocumentNode.Descendants("a")
                                    .Select(a => a.GetAttributeValue("href", null))
                                    .Where(u => !String.IsNullOrEmpty(u));
        }

        public virtual string GetAbsoluteUrlString(string baseUrl, string url)
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
