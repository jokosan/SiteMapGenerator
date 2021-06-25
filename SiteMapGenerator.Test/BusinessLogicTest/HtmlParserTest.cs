﻿using Moq;
using SiteMapGenerator.Bll.BusinessLogic;
using System.Collections.Generic;
using Xunit;

namespace SiteMapGenerator.Bll.Test.BusinessLogicTest
{
    public class HtmlParserTest
    {
        private Mock<HtmlParser> mockHtmlParser = new Mock<HtmlParser>();

        [Fact]
        public void Parser_LinkParser_ReturnAbsoluteLinkList()
        {
            // Arrange
            var htmlParser = new HtmlParser();
            var url = "https://www.example.com/";

            var list = new List<string>() { "https://www.example.com/" };

            // Act
            mockHtmlParser.SetupSequence(s => s.GetAllPageLinks(url, url)).Returns(list);
            var result = htmlParser.GetAllPageLinks(url, url);

            // Assert
            Assert.Equal(list[0], "https://www.example.com/");
        }
    }
}
