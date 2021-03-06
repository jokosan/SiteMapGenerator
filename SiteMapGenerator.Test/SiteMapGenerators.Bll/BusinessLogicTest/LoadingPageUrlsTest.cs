using Moq;
using SiteMapGenerator.Bll.BusinessLogic;
using SiteMapGenerator.Bll.Services.Contract;
using System.Collections.Generic;
using Xunit;

namespace SiteMapGenerator.Test.SiteMapGenerators.Bll.BusinessLogicTest
{
    public class LoadingPageUrlsTest
    {
        private Mock<ILoadingPageUrls> mockLoadingPage = new Mock<ILoadingPageUrls>();

        [Fact]
        public void ExtractHref_UrlValidation_ReturnParsedLinks()
        {
            // Arrange
            var loadingPageUrls = new LoadingPageUrls(new LinkCheck());
            var url = "https://www.example.com/";

            var list = new List<string>() { "https://www.example.com/" };

            // Act
            mockLoadingPage.SetupSequence(s => s.ExtractHref(url, 2)).Returns(list);
            var result = loadingPageUrls.ExtractHref(url, 1);

            // Assert
            Assert.Equal(list, result);
        }
    }
}
