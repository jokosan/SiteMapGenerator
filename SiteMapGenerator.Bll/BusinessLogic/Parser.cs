using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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
                                    .Where(u => !String.IsNullOrEmpty(u)).Distinct();
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

        public IEnumerable<string> XMLSiteMap(string xml)
        {
            var resultXmlSiteMap = new List<string>();

            /*Create a new instance of the System.Net Webclient*/
            WebClient wc = new WebClient();

            /*Set the Encodeing on the Web Client*/
            wc.Encoding = System.Text.Encoding.UTF8;

            /* Download the document as a string*/
            string sitemapString = wc.DownloadString(xml);

            /*Create a new xml document*/
            XmlDocument urldoc = new XmlDocument();

            /*Load the downloaded string as XML*/
            urldoc.LoadXml(sitemapString);

            /*Create an list of XML nodes from the url nodes in the sitemap*/
            XmlNodeList xmlSitemapList = urldoc.GetElementsByTagName("url");

            /*Loops through the node list and prints the values of each node*/
            foreach (XmlNode node in xmlSitemapList)
            {
                if (node["loc"] != null)
                {
                    resultXmlSiteMap.Add(node["loc"].InnerText);
                }
            }

            return resultXmlSiteMap.AsEnumerable();
        }

        public bool CheckForSitemapAvailability(string robotsTxt)
           => robotsTxt.Contains("Sitemap");

        public string ResultUrlSiteMAp(string[] robotsTxt)
        {
            foreach (var item in robotsTxt)
            {
                if (item.StartsWith("Sitemap"))
                {
                    int start = item.IndexOf(":") + 1;
                    return item.Substring(start).Trim(' ');
                }
            }

            return string.Empty;
        }
    }
}
