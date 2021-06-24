using Xunit;
using Moq;
using SiteMapGenerator.Bll.BusinessLogic;
using System.Collections.Generic;

namespace SiteMapGenerator.Bll.Test.BusinessLogicTest
{
    public class SitemapParserTest
    {
        private Mock<SitemapParser> mockHtmlParser = new Mock<SitemapParser>();

        [Fact]
        public void XMLSiteMap_() // ???
        {
            // Arrange
            var xmlLink = new SitemapParser();
            var linkSitemap = "https://www.ukad-group.com/";

            var list = new List<string>() { "https://www.ukad-group.com/" };

            // Act
            mockHtmlParser.SetupSequence(s => s.XMLSiteMap(It.IsAny<string>())).Returns(list);
            //var result = xmlLink.XMLSiteMap(linkSitemap);

            // Assert
        }

        [Fact]
        public void CheckForSitemapAvailability_()
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
        public void ReturnUrlSitemap_()
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
