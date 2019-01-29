using Microsoft.VisualStudio.TestTools.UnitTesting;
using _09_11_2018__Sport_Store_.Controllers.NavController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Domain.Abstract;
using Domain.Entities;

namespace _09_11_2018__Sport_Store_.Controllers.NavController.Tests
{
    [TestClass()]
    public class NavControllerTests
    {
        [TestMethod()]
        public void MenuTest1()
        {
            //Arrange
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product{ ProductID = 1, Name = "p1", Category = "1"},
                new Product{ ProductID = 2, Name = "p2", Category = "1"},
                new Product{ ProductID = 3, Name = "p3", Category = "3"},
                new Product{ ProductID = 4, Name = "p4", Category = "2"},
            }.AsQueryable());

            NavController target = new NavController(mock.Object);

            //Act
            string[] results = ((IEnumerable<string>)target.Menu().Model).ToArray();

            //Assert
            Assert.AreEqual(3, results.Length);
            Assert.AreEqual(results[0], "1");
            Assert.AreEqual(results[1], "2");
            Assert.AreEqual(results[2], "3");
        }
    }
}