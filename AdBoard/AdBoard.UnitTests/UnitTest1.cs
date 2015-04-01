using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AdBoard.Domain.Abstract;
using AdBoard.Domain.Entities;
using System.Collections.Generic;
using AdBoard.WebUI.Controllers;
using System.Linq;

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
            IEnumerable<Ad> result = (IEnumerable<Ad>)controller.List(2).Model;

            //assert
            List<Ad> ads = result.ToList();
            Assert.IsTrue(ads.Count == 2);
            Assert.AreEqual(ads[0].Title, "A4");
            Assert.AreEqual(ads[1].Title, "A5");
        }
    }
}
