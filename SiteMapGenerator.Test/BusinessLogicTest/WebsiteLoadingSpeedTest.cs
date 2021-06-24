using Moq;
using SiteMapGenerator.Bll.BusinessLogic;
using SiteMapGenerator.Bll.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace SiteMapGenerator.Test.SiteMapGenerators.Bll.BusinessLogicTest
{
    public class WebsiteLoadingSpeedTest
    {
        private Mock<WebsiteLoadingSpeed> mockWebsiteLoadingSpeed = new Mock<WebsiteLoadingSpeed>();

        [Fact]
        public void SpeedPageUploads_()
        {
            // Arrange
            var websiteLoadingSpeed = new WebsiteLoadingSpeed(new LinkValidator());
            var sitMapList = new List<string>() { "https://www.example.com/", "https://www.example.com/1/", "https://www.example.com/2/" };

            var listResult = new List<UrlResult>()
            {
                new UrlResult { NameSite = sitMapList[0], StatusCode = 200,  PageTestDate = DateTime.Now,  Elapsed = 12541}
            };

            mockWebsiteLoadingSpeed.SetupSequence(x => x.SpeedPageUploads(It.IsAny<List<string>>()))
                .Returns(listResult);

            // Act
            var result = websiteLoadingSpeed.SpeedPageUploads(sitMapList);

            // Asert
            Assert.Equal(result[0].NameSite, "https://www.example.com/");
        }
    }
}
