using Moq;
using SiteMapGenerator.Bll.BusinessLogic;
using SiteMapGenerator.Bll.BusinessLogic.Contract;
using Xunit;

namespace SiteMapGenerator.Test.SiteMapGenerators.Bll.BusinessLogicTest
{
    public class LinkValidatorTests
    {
        private Mock<ILinkValidator> LinkCheckUrlMock = new Mock<ILinkValidator>();

        [Fact]
        public void UrlValidation_CheckingUserUrl_ResultTrue()
        {
            // Arrange      
            string url = "http://microsoft.com/";
            var resultUrlValidation = true;
            var linkCheck = new LinkValidator();

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
            var linkCheck = new LinkValidator();

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
            var linkCheck = new LinkValidator();

            // Act
            LinkCheckUrlMock.SetupSequence(p => p.AddressHostValidator(url));
            var urlResult = linkCheck.AddressHostValidator(url);

            // Assert
            Assert.Equal("http://microsoft.com", urlResult);
        }
    }
}
