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
using Microsoft.AspNet.Identity;
using AdBoard.Domain.Concrete;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Routing;
using System.Security.Claims;

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
                    new Ad { Id = 1, Name = "A1"},
                    new Ad { Id = 2, Name = "A2"},
                    new Ad { Id = 3, Name = "A3"},
                    new Ad { Id = 4, Name = "A4"},
                    new Ad { Id = 5, Name = "A5"}
                });
            AdController controller = new AdController(mock.Object);
            controller.pageSize = 3;

            //act
            AdListViewModel result = (AdListViewModel)controller.List(null,2).Model;

            //assert
            List<Ad> ads = result.Ads.ToList();
            Assert.IsTrue(ads.Count == 2);
            Assert.AreEqual(ads[0].Name, "A4");
            Assert.AreEqual(ads[1].Name, "A5");
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
                new Ad { Id = 1, Name = "A1"},
                    new Ad { Id = 2, Name = "A2"},
                    new Ad { Id = 3, Name = "A3"},
                    new Ad { Id = 4, Name = "A4"},
                    new Ad { Id = 5, Name = "A5"}
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
                new Ad { Id = 1, Name = "Ad1", Category = "C1"},
                new Ad { Id = 2, Name = "Ad2", Category = "C2"},
                new Ad { Id = 3, Name = "Ad3", Category = "C1"},
                new Ad { Id = 4, Name = "Ad4", Category = "C2"},
                new Ad { Id = 5, Name = "Ad5", Category = "C1"}
            });
            AdController controller = new AdController(mock.Object);
            controller.pageSize = 3;

            //act
            List<Ad> result = ((AdListViewModel)controller.List("C2", 1).Model).Ads.ToList();

            //assert
            Assert.AreEqual(result.Count, 2);
            Assert.IsTrue(result[0].Name == "Ad2" && result[0].Category == "C2");
            Assert.IsTrue(result[1].Name == "Ad4" && result[1].Category == "C2");
        }

        [TestMethod]
        public void Can_Generate_List_of_Categories()
        {
            Mock<IAdRepository> mock = new Mock<IAdRepository>();
            mock.Setup(m => m.Ads).Returns(new List<Ad>
                {
                    new Ad { Id = 1, Category = "C1"},
                    new Ad { Id = 2, Category = "C3"},
                    new Ad { Id = 3, Category = "C1"},
                    new Ad { Id = 4, Category = "C2"},
                    new Ad { Id = 5, Category = "C3"},
                });
            NavController controller = new NavController(mock.Object);

            //act
            List<string> result = ((IEnumerable<string>)controller.Menu(null).Model).ToList();

            //assert
            Assert.IsTrue(result.Count == 3);
            Assert.AreEqual(result[0], "C1");
            Assert.AreEqual(result[1], "C2");
            Assert.AreEqual(result[2], "C3");
        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            //arrange 
            Mock<IAdRepository> mock = new Mock<IAdRepository>();
            mock.Setup(m => m.Ads).Returns(new List<Ad>
            {
                new Ad { Id = 1, Category = "C1"},
                new Ad { Id = 2, Category = "C2"},
                new Ad { Id = 3, Category = "C1"}
            });
            NavController controller = new NavController(mock.Object);

            string categoryToSelect = "C1";

            //act
            string result = controller.Menu(categoryToSelect).ViewBag.SelectedCategory;

            //assert
            Assert.AreEqual(categoryToSelect, result);
        }

        [TestMethod]
        public void Generate_Category_Specific_Count()
        {
            //arrange 
            Mock<IAdRepository> mock = new Mock<IAdRepository>();
            mock.Setup(m => m.Ads).Returns(new List<Ad>
            {
                new Ad { Id = 1, Category = "C1"},
                new Ad { Id = 2, Category = "C2"},
                new Ad { Id = 3, Category = "C1"},
                new Ad { Id = 4, Category = "C2"},
                new Ad { Id = 5, Category = "C3"}
            });
            AdController controller = new AdController(mock.Object);
            controller.pageSize = 3;

            //act
            int res1 = ((AdListViewModel)controller.List("C1").Model).PagingInfo.TotalItems;
            int res2 = ((AdListViewModel)controller.List("C2").Model).PagingInfo.TotalItems;
            int res3 = ((AdListViewModel)controller.List("C3").Model).PagingInfo.TotalItems;
            int res4 = ((AdListViewModel)controller.List(null).Model).PagingInfo.TotalItems;

            //assert
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(res4, 5);
        }

        [TestMethod]
        public void Can_Edit_Ad()
        {
            Mock<IAdRepository> mock = new Mock<IAdRepository>();
            mock.Setup(m => m.Ads).Returns(new List<Ad>
                {
                    new Ad { Id = 1, Name = "A1"},
                    new Ad { Id = 2, Name = "A2"},
                    new Ad { Id = 3, Name ="A3"},
                });

            AdController controller = new AdController(mock.Object);

            Ad ad1 = controller.Edit(1).ViewData.Model as Ad;
            Ad ad2 = controller.Edit(2).ViewData.Model as Ad;
            Ad ad3 = controller.Edit(3).ViewData.Model as Ad;

            Assert.AreEqual(1, ad1.Id);
            Assert.AreEqual(2, ad2.Id);
            Assert.AreEqual(3, ad3.Id);

        }

        [TestMethod]
        public void Can_Create_Ad()
        {
            var context = new Mock<HttpContextBase>();
            var mockIdentity = new Mock<IIdentity>();
            context.SetupGet(x => x.User.Identity).Returns(mockIdentity.Object);
            mockIdentity.Setup(x => x.Name).Returns("test_name");
            Mock<IAdRepository> mock = new Mock<IAdRepository>();
            mock.Setup(m => m.Ads).Returns(new List<Ad>
                {
                    new Ad { Id = 0, Name="A1", UserId = "id"}
                });

            var identity = new GenericIdentity("dominik.ernst@xyz123.de");
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "id"));
            var principal = new GenericPrincipal(identity, new[] { "user" });
            context.Setup(s => s.User).Returns(principal);

            AdController controller = new AdController(mock.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var result = controller.Create().Model as Ad;
            
            Assert.AreEqual(result.UserId, "id");
        }

        [TestMethod]
        public void Can_Delete_Ad()
        {
            Ad ad = new Ad { Id = 1, Name = "A2" };

            Mock<IAdRepository> mock = new Mock<IAdRepository>();
            mock.Setup(m => m.Ads).Returns(new List<Ad>
                {
                    new Ad { Id = 0, Name="A1"},
                    new Ad { Id = 1, Name = "A2"},
                    new Ad { Id = 2, Name = "A3"}
                });

            AdController controller = new AdController(mock.Object);

            controller.Delete(1);
            

            mock.Verify(m => m.DeleteAd(ad.Id));
        }
    }
}
