using System.Collections.Generic;
using System.Data;
using System.Linq;
using Moq;
using SiteMapGenerator.Dal.Models.Dal;
using SiteMapGenerator.Dal.Serveses;
using Xunit;

namespace SiteMapGenerator.Dal.Test.Serveses
{
    public class TablePageInfoTest
    {
        Mock<IRepository<PageInfo>> mockPageInfo = new Mock<IRepository<PageInfo>>();

        List<PageInfo> resulPageInfo = new List<PageInfo>() {
                new PageInfo { IdPageInfo = 1, SitemapId = 1, WebsiteLoadingSpeed = 3725757, StatusCode = 200, Elapsed = 12 },
                new PageInfo { IdPageInfo = 2, SitemapId = 2, WebsiteLoadingSpeed = 3325757, StatusCode = 200, Elapsed = 12  },
                new PageInfo { IdPageInfo = 3, SitemapId = 1, WebsiteLoadingSpeed = 3325757, StatusCode = 200, Elapsed = 12  }
            };

        [Fact]
        public void GetPageInfos_GetPageInfoTableFromatabase_ReturnTable()
        {
            // Arrange
            var serveses = new TablePageInfo(mockPageInfo.Object);

            mockPageInfo.Setup(x => x.GetAll()).Returns(resulPageInfo.AsQueryable);

            // Act
            var result = serveses.GetPageInfo();

            // Assert
            Assert.Equal(resulPageInfo, result);
        }

        [Fact]
        public void SaveSitemap_WritingDataToTheDatabase_Save()
        {
            // Arrange
            var serveses = new TablePageInfo(mockPageInfo.Object);
            mockPageInfo.Setup(_ => _.SaveChanges());

            // Act
            serveses.SavePageInfo(resulPageInfo[0]);

            // Assert
            mockPageInfo.Verify(_ => _.Add(resulPageInfo[0]), Times.AtLeastOnce());
        }
    }
}
