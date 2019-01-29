using Microsoft.VisualStudio.TestTools.UnitTesting;
using _09_11_2018__Sport_Store_.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Domain.Abstract;
using Domain.Entities;
using System.Web.Mvc;

namespace _09_11_2018__Sport_Store_.Controllers.Tests
{
    [TestClass()]
    public class AdminTests
    {

        [TestMethod()]
        public void Index_Contains_All_Products()
        {
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                      new Product {ProductID = 1, Name = "P1"},
                      new Product {ProductID = 2, Name = "P2"},
                      new Product {ProductID = 3, Name = "P3"}}.AsQueryable());
            AdminController controller = new AdminController(mock.Object);
            Product[] result =
                ((IEnumerable<Product>)controller.Index().ViewData.Model).ToArray();
            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual("P1", result[0].Name);
            Assert.AreEqual("P2", result[1].Name);
        }

        [TestMethod()]
        public void Can_Edit_Product()
        {
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                      new Product {ProductID = 1, Name = "P1"},
                      new Product {ProductID = 2, Name = "P2"},
                      new Product {ProductID = 3, Name = "P3"}}.AsQueryable());
            AdminController controller = new AdminController(mock.Object);
            Product p1 = controller.Edit(1).ViewData.Model as Product;
            Product p2 = controller.Edit(2).ViewData.Model as Product;
            Product p3 = controller.Edit(3).ViewData.Model as Product;
            Assert.AreEqual(1, p1.ProductID);
            Assert.AreEqual(2, p2.ProductID);
            Assert.AreEqual(3, p3.ProductID);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Product()
        {
            // Arrange - create the mock repository
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                      new Product {ProductID = 1, Name = "P1"},
                      new Product {ProductID = 2, Name = "P2"},
                      new Product {ProductID = 3, Name = "P3"}}.AsQueryable());
            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object);
            // Act
            Product result = (Product)target.Edit(4).ViewData.Model;
            // Assert
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void Can_Save_Valid_Changes()
        {
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            AdminController controller = new AdminController(mock.Object);
            Product product = new Product { Name = "Test" };
            ActionResult result = controller.Edit(product, null);
            mock.Verify(m => m.SaveProduct(product));
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            // Arrange - create mock repository
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object);
            // Arrange - create a product
            Product product = new Product { Name = "Test" };
            // Arrange - add an error to the model state
            target.ModelState.AddModelError("error", "error");
            // Act - try to save the product
            ActionResult result = target.Edit(product, null);
            // Assert - check that the repository was not called
            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never());
            // Assert - check the method result type
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod()]
        public void Can_Delete_Product()
        {
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            Product prod = new Product { ProductID = 2, Name = "Test Product" };
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product { ProductID = 1, Name = "P1" }, prod,
                new Product { ProductID = 3, Name = "P3" } }.AsQueryable());
            AdminController controller = new AdminController(mock.Object);
            controller.Delete(prod.ProductID);
            mock.Verify(m => m.DeleteProduct(prod.ProductID));
        }
    }
}