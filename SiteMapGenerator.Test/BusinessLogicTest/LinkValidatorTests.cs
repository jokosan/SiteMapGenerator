using Moq;
using SiteMapGenerator.Bll.BusinessLogic;
using Xunit;

namespace SiteMapGenerator.Test.SiteMapGenerators.Bll.BusinessLogicTest
{
    public class LinkValidatorTests
    {
        private Mock<LinkValidator> LinkCheckUrlMock = new Mock<LinkValidator>();

        [Fact]
        public void UrlValidation_CorrectLink_ReturnTrue()
        {
            // Arrange      
            string url = "http://microsoft.com/";
            var resultUrlValidation = true;
            var linkCheck = new LinkValidator();

            //  Act
            LinkCheckUrlMock.SetupSequence(p => p.CheckURLValid(url))
                .Returns(resultUrlValidation);

            var urlResult = linkCheck.CheckURLValid(url);

            // Assert
            Assert.Equal(resultUrlValidation, urlResult);
        }

        [Fact]
        public void UrlValidation_InvalidUrl_ReturnFalse()
        {
            // Arrange      
            string url = "microsoft.com";
            var resultUrlValidation = false;
            var linkCheck = new LinkValidator();

            //  Act
            LinkCheckUrlMock.SetupSequence(p => p.CheckURLValid(url))
                .Returns(resultUrlValidation);

            var urlResult = linkCheck.CheckURLValid(url);

            // Assert
            Assert.Equal(resultUrlValidation, urlResult);
        }

        [Fact]
        public void AddressHostValidator_CorrectLink_ReturnStringUrl()
        {
            // Arrange
            string url = "http://microsoft.com/";
            var linkCheck = new LinkValidator();

            // Act
            LinkCheckUrlMock.SetupSequence(p => p.GetHost(url));
            var urlResult = linkCheck.GetHost(url);

            // Assert
            Assert.Equal("http://microsoft.com", urlResult);
        }

        [Fact]
        public void StatusHost_StatusLink_returTrue()
        {
            // Arrange
            string url = "http://microsoft.com/";
            var linkValidator = new LinkValidator();
            LinkCheckUrlMock.SetupSequence(p => p.StatusHost(It.IsAny<string>())).Returns(true);

            // Act
            var resuilt = linkValidator.StatusHost(url);

            //  Assert
            Assert.Equal(resuilt, true);
        }

        [Fact]
        public void StatusHost_StatusLink_returnFalse()
        {
            // Arrange
            string url = "http://test.com";
            var linkValidator = new LinkValidator();
            LinkCheckUrlMock.SetupSequence(p => p.StatusHost(It.IsAny<string>())).Returns(false);

            // Act
            var resuilt = linkValidator.StatusHost(url);

            //  Assert
            Assert.Equal(resuilt, false);
        }
    }
}
