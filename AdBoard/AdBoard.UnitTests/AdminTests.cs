using AdBoard.Domain.Abstract;
using AdBoard.Domain.Entities;
using AdBoard.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdBoard.UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Can_Edit_Admin_Ad()
        {
            Mock<IAdRepository> mock = new Mock<IAdRepository>();
            mock.Setup(m => m.Ads).Returns(new List<Ad>
                {
                    new Ad { Id = 1, Name = "A1"},
                    new Ad { Id = 2, Name = "A2"},
                    new Ad { Id = 3, Name ="A3"},
                });

            AdminController controller = new AdminController(mock.Object);

            Ad ad1 = controller.Edit(1).ViewData.Model as Ad;
            Ad ad2 = controller.Edit(2).ViewData.Model as Ad;
            Ad ad3 = controller.Edit(3).ViewData.Model as Ad;

            Assert.AreEqual(1, ad1.Id);
            Assert.AreEqual(2, ad2.Id);
            Assert.AreEqual(3, ad3.Id);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Ad()
        {
            Mock<IAdRepository> mock = new Mock<IAdRepository>();
            mock.Setup(m => m.Ads).Returns(new List<Ad>
                {
                    new Ad { Id = 1, Name = "Ad1"},
                    new Ad { Id = 2, Name = "Ad2"},
                    new Ad { Id = 3, Name = "Ad3"},
                    new Ad { Id = 4, Name = "Ad4"},
                    new Ad { Id = 5, Name = "Ad5"}
                });

            AdminController controller = new AdminController(mock.Object);

            Ad ad = controller.Edit(6).ViewData.Model as Ad;            
        }

        [TestMethod]
        public void Index_Contains_All_Ads()
        {
            Mock<IAdRepository> mock = new Mock<IAdRepository>();
            mock.Setup(m => m.Ads).Returns(new List<Ad>
            {
                new Ad { Id = 1, Name = "Ad1"},
                new Ad { Id = 2, Name = "Ad2"},
                new Ad { Id = 3, Name = "Ad3"},
                new Ad { Id = 4, Name = "Ad4"},
                new Ad { Id = 5, Name = "Ad5"}
            });

            AdminController controller = new AdminController(mock.Object);

            List<Ad> ads = ((IEnumerable<Ad>)controller.Index().ViewData.Model).ToList();

            Assert.AreEqual(ads.Count, 5);
            Assert.AreEqual(ads[0].Name, "Ad1");
            Assert.AreEqual(ads[4].Name, "Ad5");
        }
    }
}
