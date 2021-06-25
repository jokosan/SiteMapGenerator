using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml;

namespace SiteMapGenerator.Bll.BusinessLogic
{
    public class SitemapParser
    {
        public virtual IEnumerable<string> XMLSiteMap(string xmlLink)
        {
            var resultXmlSiteMap = new List<string>();

            /*Create a new instance of the System.Net Webclient*/
            WebClient wc = new WebClient();

            /*Set the Encodeing on the Web Client*/
            wc.Encoding = System.Text.Encoding.UTF8;

            /* Download the document as a string*/
            string sitemapString = wc.DownloadString(xmlLink);

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

        public string ReturnUrlSitemap(string robots)
        {
            if (robots.Contains("Sitemap"))
            {
                var indexStart = robots.LastIndexOf("Sitemap:") + "Sitemap:".Length;
                var substringLength = robots.LastIndexOf("xml") + "xml".Length;

                return robots.Substring(indexStart, substringLength - indexStart).Trim(' ');
            }

            return string.Empty;
        }
                 
    }
}
