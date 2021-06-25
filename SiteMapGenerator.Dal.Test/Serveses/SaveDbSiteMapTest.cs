using Moq;
using SiteMapGenerator.Bll.Models;
using SiteMapGenerator.Dal.Models.Dal;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Xunit;

namespace SiteMapGenerator.Dal.Test.Serveses
{

    public class SaveDbSiteMapTest
    {
        //Mock<IRepository<ArchiveOfRequest>> mockArchiveOfRequest = new Mock<IRepository<ArchiveOfRequest>>();
        //Mock<IRepository<PageInfo>> mockPageInfo = new Mock<IRepository<PageInfo>>();
        //Mock<IRepository<UrlSiteMap>> mockUrlSiteMap = new Mock<IRepository<UrlSiteMap>>();
        //Mock<SaveDbSiteMap> mockSaveDbSiteMap = new Mock<SaveDbSiteMap>();

        //[Fact]
        //public void Save_()
        //{
        //    // Arrange
        //    var imputListUrl = new List<UrlResult>() {
        //        new UrlResult { NameSite = "https://example.com/" },
        //        new UrlResult { NameSite = "https://example.com/1/"}
        //    };

        //    var urlSiteMaps = new List<UrlSiteMap>(){
        //        new UrlSiteMap { IdSitemap = 1, NameSite = "https://docs.microsoft.com/", ArchiveOfRequestsId = 1 },
        //        new UrlSiteMap { IdSitemap = 2, NameSite = "https://example.com/", ArchiveOfRequestsId = 2 }
        //    };

        //    var urlSiteMap = new UrlSiteMap();
        //    urlSiteMap.NameSite = "https://example.com/";

        //    SaveDbSiteMap saveDbSiteMap = new SaveDbSiteMap(mockArchiveOfRequest.Object, mockPageInfo.Object, mockUrlSiteMap.Object);

        //    // Act
        //    saveDbSiteMap.Save(imputListUrl.AsEnumerable(), urlSiteMaps, 1);

        //    // Assert
        //    mockPageInfo.Verify(p => p.Add(It.Is<PageInfo>(f => f.SitemapId == 2)));

        //}

        //[Fact]
        //public void SaveUserRequest()
        //{
        //    SaveDbSiteMap saveDbSiteMap = new SaveDbSiteMap(mockArchiveOfRequest.Object, mockPageInfo.Object, mockUrlSiteMap.Object);
        //    mockSaveDbSiteMap.SetupSequence(x => x.SaveUserRequest(It.IsAny<string>())).Returns(2);
        //    var resultInt = saveDbSiteMap.SaveUserRequest("https://example.com/");

        //    mockArchiveOfRequest.SetupSequence(x => x.Add(It.Is<ArchiveOfRequest>(f => f.IdArchiveOfRequests == 1)));

        //    Assert.Equal(resultInt, 0);
        //}
    }
}
