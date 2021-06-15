using Moq;
using SiteMapGenerator.Bll.BusinessLogic;
using SiteMapGenerator.Bll.BusinessLogic.Contract;
using Xunit;

namespace SiteMapGenerator.Test.SiteMapGenerators.Bll.BusinessLogicTest
{
    public class LinkCheckTests
    {
        private Mock<ILinkCheck> LinkCheckUrlMock = new Mock<ILinkCheck>();

        [Fact]
        public void UrlValidation_CheckingUserUrl_ResultTrue()
        {
            // Arrange      
            string url = "http://microsoft.com/";
            var resultUrlValidation = true;
            var linkCheck = new LinkCheck();

            //  Act
            LinkCheckUrlMock.SetupSequence(p => p.UrlValidation(url))
                .Returns(resultUrlValidation);

            var urlResult = linkCheck.UrlValidation(url);

            // Assert
            Assert.Equal(resultUrlValidation, urlResult);
        }

        [Fact]
        public void UrlValidation_CheckingUserUrl_ResultFalse()
        {
            // Arrange      
            string url = "microsoft.com";
            var resultUrlValidation = false;
            var linkCheck = new LinkCheck();

            //  Act
            LinkCheckUrlMock.SetupSequence(p => p.UrlValidation(url))
                .Returns(resultUrlValidation);

            var urlResult = linkCheck.UrlValidation(url);

            // Assert
            Assert.Equal(resultUrlValidation, urlResult);
        }

        [Fact]
        public void AddressHost_CheckingHostAvailability_ReturnStringUrl()
        {
            // Arrange
            string url = "http://microsoft.com/";
            var linkCheck = new LinkCheck();

            // Act
            LinkCheckUrlMock.SetupSequence(p => p.AddressHost(url));
            var urlResult = linkCheck.AddressHost(url);

            // Assert
            Assert.Equal("http://microsoft.com", urlResult);
        }
    }
}
