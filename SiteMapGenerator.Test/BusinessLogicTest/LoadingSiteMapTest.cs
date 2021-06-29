using Moq;
using SiteMapGenerator.Bll.BusinessLogic;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SiteMapGenerator.Bll.Test.BusinessLogicTest
{
    public class LoadingSiteMapTest
    {
        private Mock<LoadingSiteMap> mockLoadingSiteMap = new Mock<LoadingSiteMap>();
        private Mock<SitemapParser> mockSitemapParser = new Mock<SitemapParser>();
        private Mock<LinkValidator> mockLinkValidator = new Mock<LinkValidator>();

        [Fact]
        public void SearchSitemap_SitemapDefaultUrl_ReturnListlink()
        {
            // Arrange
            var LoadingSiteMap = new LoadingSiteMap(mockSitemapParser.Object, mockLinkValidator.Object);
            string url = "https://www.ukad-group.com/";

            var listSitmap = new List<string>() { "https://www.ukad-group.com/", "https://www.ukad-group.com/blog" };

            // Act
            mockLinkValidator.Setup(x => x.StatusHost("https://www.ukad-group.com/sitemap.xml")).Returns(true);            
            mockSitemapParser.Setup(x => x.XMLSiteMap("https://www.ukad-group.com/sitemap.xml")).Returns(listSitmap);

            var result = LoadingSiteMap.SearchSitemap(url);
            
            // Assert
            Assert.Equal(result.ToList()[0], "https://www.ukad-group.com/");
        }

        [Fact]
        public void SearchSitemap_GetUrlSitemapThroughRobotsTxt_ReturnListlink()
        {
            // Arrange
            var LoadingSiteMap = new LoadingSiteMap(mockSitemapParser.Object, mockLinkValidator.Object);
            string url = "https://www.ukad-group.com/";

            var listSitmap = new List<string>() { "https://www.ukad-group.com/", "https://www.ukad-group.com/blog" };

            // Act
            mockLinkValidator.Setup(x => x.StatusHost("https://www.ukad-group.com/sitemap.xml")).Returns(false);
            mockLinkValidator.Setup(x => x.StatusHost("https://www.ukad-group.com/robots.txt")).Returns(true);

            mockSitemapParser.Setup(x => x.XMLSiteMap("https://www.ukad-group.com/sitemap.xml")).Returns(listSitmap);

            var result = LoadingSiteMap.SearchSitemap(url);

            // Assert
            Assert.Equal(result.ToList()[0], "https://www.ukad-group.com/");
        }

        [Fact]
        public void SearchSitemap_SitemapWasNotFound_RetornListMessage()
        {
            // Arrange
            var LoadingSiteMap = new LoadingSiteMap(mockSitemapParser.Object, mockLinkValidator.Object);
            string url = "https://www.ukad-group.com/";

            // Act
            mockLinkValidator.Setup(x => x.StatusHost("https://www.ukad-group.com/sitemap.xml")).Returns(false);
            mockLinkValidator.Setup(x => x.StatusHost("https://www.ukad-group.com/robots.txt")).Returns(false);


            var result = LoadingSiteMap.SearchSitemap(url);

            // Assert
            Assert.Equal(result.ToList()[0], "Sitemap not found or invalid robots.txt");
        }
    }
}
