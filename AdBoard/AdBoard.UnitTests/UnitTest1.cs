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
            AdListViewModel result = (AdListViewModel)controller.List(null,2).Model;

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
            AdListViewModel result = (AdListViewModel)controller.List(null,2).Model;

            //assert
            PagingInfo pagingInfo = result.PagingInfo;
            Assert.AreEqual(pagingInfo.CurrentPage, 2);
            Assert.AreEqual(pagingInfo.TotalItems, 5);
            Assert.AreEqual(pagingInfo.TotalPages, 2);
            Assert.AreEqual(pagingInfo.ItemsPerPage, 3);
        }

        [TestMethod]
        public void Can_Filter_Ads()
        {
            //arrange
            Mock<IAdRepository> mock = new Mock<IAdRepository>();
            mock.Setup(m => m.Ads).Returns(new List<Ad>
            {
                new Ad { Id = 1, Title = "Ad1", Category = "C1"},
                new Ad { Id = 2, Title = "Ad2", Category = "C2"},
                new Ad { Id = 3, Title = "Ad3", Category = "C1"},
                new Ad { Id = 4, Title = "Ad4", Category = "C2"},
                new Ad { Id = 5, Title = "Ad5", Category = "C1"}
            });
            AdController controller = new AdController(mock.Object);
            controller.pageSize = 3;

            //act
            List<Ad> result = ((AdListViewModel)controller.List("C2", 1).Model).Ads.ToList();

            //assert
            Assert.AreEqual(result.Count, 2);
            Assert.IsTrue(result[0].Title == "Ad2" && result[0].Category == "C2");
            Assert.IsTrue(result[1].Title == "Ad4" && result[1].Category == "C2");
        }
    }
}
