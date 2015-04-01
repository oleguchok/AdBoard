using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AdBoard.Domain.Abstract;
using AdBoard.Domain.Entities;
using System.Collections.Generic;
using AdBoard.WebUI.Controllers;
using System.Linq;
using System.Web.Mvc;
using AdBoard.WebUI.Models;
using AdBoard.WebUI.HtmlHelpers;

namespace AdBoard.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CanPaginate()
        {
            //arrange
            Mock<IAdRepository> mock = new Mock<IAdRepository>();
            mock.Setup(m => m.Ads).Returns(new List<Ad>
                {
                    new Ad { Id = 1, Title = "A1"},
                    new Ad { Id = 2, Title = "A2"},
                    new Ad { Id = 3, Title = "A3"},
                    new Ad { Id = 4, Title = "A4"},
                    new Ad { Id = 5, Title = "A5"}
                });
            AdController controller = new AdController(mock.Object);
            controller.pageSize = 3;

            //act
            AdListViewModel result = (AdListViewModel)controller.List(2).Model;

            //assert
            List<Ad> ads = result.Ads.ToList();
            Assert.IsTrue(ads.Count == 2);
            Assert.AreEqual(ads[0].Title, "A4");
            Assert.AreEqual(ads[1].Title, "A5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            //arrange
            HtmlHelper helper = null;

            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                ItemsPerPage = 10,
                TotalItems = 28
            };

            Func<int, string> pageUrlDelegate = i => "Page" + i;

            //act
            MvcHtmlString result = helper.PageLinks(pagingInfo, pageUrlDelegate);

            //asset
            Assert.AreEqual(result.ToString() ,@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>");
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            //arrange
            Mock<IAdRepository> mock = new Mock<IAdRepository>();
            mock.Setup(m => m.Ads).Returns(new List<Ad>
            {
                new Ad { Id = 1, Title = "A1"},
                new Ad { Id = 2, Title = "A2"},
                new Ad { Id = 3, Title = "A3"},
                new Ad { Id = 4, Title = "A4"},
                new Ad { Id = 5, Title = "A5"}
            });
            AdController controller = new AdController(mock.Object);
            controller.pageSize = 3;

            //act
            AdListViewModel result = (AdListViewModel)controller.List(2).Model;

            //assert
            PagingInfo pagingInfo = result.PagingInfo;
            Assert.AreEqual(pagingInfo.CurrentPage, 2);
            Assert.AreEqual(pagingInfo.TotalItems, 5);
            Assert.AreEqual(pagingInfo.TotalPages, 2);
            Assert.AreEqual(pagingInfo.ItemsPerPage, 3);
        }
    }
}
