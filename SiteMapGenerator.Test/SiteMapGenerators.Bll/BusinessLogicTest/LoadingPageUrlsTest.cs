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
           
        }
    }
}
