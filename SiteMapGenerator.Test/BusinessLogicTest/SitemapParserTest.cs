using Moq;
using SiteMapGenerator.Bll.BusinessLogic;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml;
using Xunit;

namespace SiteMapGenerator.Bll.Test.BusinessLogicTest
{
    public class SitemapParserTest
    {
        private Mock<SitemapParser> mockSitemapParser = new();
        private Mock<WebClient> mockWebClient = new();
        private Mock<XmlDocument> mockXmlDocument = new();


        [Fact]
        public void XMLSiteMap_ParserXmlSiteMAp_ReturnList() // ???
        {
            // Arrange
            var xmlLink = new SitemapParser();
            var linkSitemap = "https://www.ukad-group.com/";
         
            //Act
            var result = xmlLink.XMLSiteMap("https://www.ukad-group.com/sitemap.xml");

            //Assert
            Assert.Equal(result.ToList()[0], linkSitemap );
        }

        [Fact]
        public void CheckForSitemapAvailability_SearchSitemapInRobotstxt_returnTrue()
        {
            // Arrange
            var sitemapParser = new SitemapParser();
            var stringRobots = "# robots.txt for Umbraco\r\nUser-agent: *\r\nDisallow: /aspnet_client/\r\nDisallow:" +
                               " /bin/\r\nDisallow: /config/\r\nDisallow: /css/\r\nDisallow: /data/\r\nDisallow: " +
                               "/install/\r\nDisallow: /masterpages/\r\nDisallow: /python/\r\nDisallow: /scripts/\r\nDisallow:" +
                               " /umbraco/\r\nDisallow: /umbraco_client/\r\nDisallow: /usercontrols/\r\nDisallow: " +
                               "/xslt/\r\nSitemap: https://www.ukad-group.com/sitemap.xml\r\n";

            // Act
            var result = sitemapParser.CheckForSitemapAvailability(stringRobots);

            // Assert
            Assert.Equal(result, true);
        }

        [Fact]
        public void ReturnUrlSitemap_GettingLinkToSitemap_ReturnStringUrlSiteMap()
        {
            // Arrange
            var sitemapParser = new SitemapParser();
            var stringRobots = "# robots.txt for Umbraco\r\nUser-agent: *\r\nDisallow: /aspnet_client/\r\nDisallow:" +
                               " /bin/\r\nDisallow: /config/\r\nDisallow: /css/\r\nDisallow: /data/\r\nDisallow: " +
                               "/install/\r\nDisallow: /masterpages/\r\nDisallow: /python/\r\nDisallow: /scripts/\r\nDisallow:" +
                               " /umbraco/\r\nDisallow: /umbraco_client/\r\nDisallow: /usercontrols/\r\nDisallow: " +
                               "/xslt/\r\nSitemap: https://www.ukad-group.com/sitemap.xml\r\n";

            // Act
            var result = sitemapParser.ReturnUrlSitemap(stringRobots);

            // Assert
            Assert.Equal(result, "https://www.ukad-group.com/sitemap.xml");
        }
    }
}
