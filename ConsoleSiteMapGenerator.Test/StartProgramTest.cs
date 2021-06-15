using System;
using Xunit;
using Moq;
using ConsoleSiteMapGenerator.Infrastructure.Contract;
using SiteMapGenerator.Bll.BusinessLogic.Contract;
using ConsoleSiteMapGenerator.Infrastructure.Constants;
using System.Collections.Generic;
using SiteMapGenerator.Bll.Models.Bll;
using System.Linq;

namespace ConsoleSiteMapGenerator.Test
{
    public class StartProgramTest
    {
        Mock<IUserInteraction> mockUserInteraction = new Mock<IUserInteraction>();
        Mock<IGeneratingSitemap> mockGeneratingSitemap = new Mock<IGeneratingSitemap>();

        [Fact]
        public void Start_StartMessage_Message()
        {
            // Arrange
            var startConsole = new StartProgram(mockUserInteraction.Object, mockGeneratingSitemap.Object);


            mockUserInteraction.Setup(x => x.UserValueInput()).Returns("");
            mockGeneratingSitemap.Setup(x => x.ValidationAddresses("")).Returns(false);

            // Act
            startConsole.Start();

            // Asert
            mockUserInteraction.Verify(x => x.Info(MessageUsers.MessageStart));
        }

        [Fact]
        public void Start_IncorrectUrl_MessageIncorrectlyUrl()
        {
            // Arrange
            var urlTest = "docs.microsoft.com";
            var startConsole = new StartProgram(mockUserInteraction.Object, mockGeneratingSitemap.Object);

            mockUserInteraction.Setup(x => x.UserValueInput()).Returns(urlTest);
            mockGeneratingSitemap.Setup(x => x.ValidationAddresses(urlTest)).Returns(false);

            // Act 
            startConsole.Start();

            // Asert
            mockUserInteraction.Verify(x => x.Info(MessageUsers.MessageIncorrectUrl));
        }

        [Fact]
        public void Start_CreateSitemap_ReturnIEnumerableUrlResult()
        {
            // Arrang
            var urlTest = "https://docs.microsoft.com";

            var listResult = new List<UrlResult>();
            listResult.Add(new UrlResult
            {
                NameSite = urlTest,
                StatusCode = 200,
                PageTestDate = DateTime.Now,
                Elapsed = 12541
            });

            var startConsole = new StartProgram(mockUserInteraction.Object, mockGeneratingSitemap.Object);

            mockUserInteraction.SetupSequence(x => x.UserValueInput()).Returns(urlTest);
            mockGeneratingSitemap.SetupSequence(x => x.ValidationAddresses(urlTest)).Returns(true);
            mockGeneratingSitemap.SetupSequence(x => x.Loading(It.IsAny<string>(), It.IsAny<int>())).Returns(listResult);

            // Act
            startConsole.Start();

            // Asert\
            
            Assert.True(listResult.Any(a => a.NameSite == urlTest));
        }
    }
}
