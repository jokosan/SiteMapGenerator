using Moq;
using Xunit;
using SiteMapGenerator.Bll.Services.Contract;
using SiteMapGenerator.Bll.BusinessLogic;
using System.Collections.Generic;

namespace SiteMapGenerator.Test.SiteMapGenerators.Bll.BusinessLogicTest
{
    public class WebsiteLoadingSpeedTest
    {
        private Mock<IWebsiteLoadingSpeed> mockWebsiteLoadingSpeed = new Mock<IWebsiteLoadingSpeed>();

        [Fact]
        public void SpeedPageUploads_()
        {
            // Arrange
            var websiteLoadingSpeed = new WebsiteLoadingSpeed(new LinkCheck());
            var sitMapList = new List<string>() {"https://www.example.com/"};

            // Act


        }
    }
}
