using Moq;
using SiteMapGenerator.Dal.Models.Dal;
using SiteMapGenerator.Dal.Serveses;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Xunit;


namespace SiteMapGenerator.Dal.Test.Serveses
{
    public class TableArchiveOfRequestTest
    {
        Mock<IRepository<ArchiveOfRequest>> mockRepositoryArchiveOfRequest = new Mock<IRepository<ArchiveOfRequest>>();
        Mock<TableArchiveOfRequest> moqTableArchiveOfRequest = new Mock<TableArchiveOfRequest>();

        [Fact]
        public void GetArchiveOfRequest_GetArchiveOfRequestTableFromatabase_ReturnTable()
        {
            // Arrange
            var serveses = new TableArchiveOfRequest(mockRepositoryArchiveOfRequest.Object);

            var resultList = new List<ArchiveOfRequest>() {
                new ArchiveOfRequest{IdArchiveOfRequests = 1, NameUrl = "http://test1.com/" },
                new ArchiveOfRequest{ IdArchiveOfRequests = 2, NameUrl = "http://test2.com/"}
            }.AsQueryable();

            mockRepositoryArchiveOfRequest.Setup(x => x.GetAll()).Returns(resultList);

            // Act
            var result = serveses.GetArchiveOfRequest();

            //Assert
            Assert.Equal(resultList, result);
        }

        [Fact]
        public void SaveUserRequest_WritingDataToTheDatabase_Save()
        {
            var serveses = new TableArchiveOfRequest(mockRepositoryArchiveOfRequest.Object);
            moqTableArchiveOfRequest.SetupSequence(x => x.SaveUserRequest(It.IsAny<string>())).Returns(2);
            var resultInt = serveses.SaveUserRequest("https://example.com/");

            mockRepositoryArchiveOfRequest.SetupSequence(x => x.Add(It.Is<ArchiveOfRequest>(f => f.IdArchiveOfRequests == 1)));

            Assert.Equal(resultInt, 0);
        }
    }
}
