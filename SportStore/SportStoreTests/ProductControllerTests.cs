using Moq;
using NUnit.Framework;
using SportStore.Controllers;
using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportStoreTests
{
    [TestFixture]
    public class ProductControllerTests
    {
        [Test]
        public void Controller_CanPaginate_True()
        {
            //Arrange
            var repo = new Mock<IProductRepository>();
            repo.Setup(r => r.Products).Returns((
                new Product[]
                {
                    new Product {ProductId = 0, Name="p0"},
                    new Product {ProductId = 1, Name="p1"},
                    new Product {ProductId = 2, Name="p2"},
                    new Product {ProductId = 3, Name="p3"},
                    new Product {ProductId = 4, Name="p4"},
                }).AsQueryable<Product>());

            var controller = new ProductController(repo.Object);
            controller.PageSize = 3;

            //Act
            var result = controller.List(2).ViewData.Model as IEnumerable<Product>;

            //Assert
            var prodArray = result.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.True("p3".Equals(prodArray[0].Name));
            Assert.True("p4".Equals(prodArray[1].Name));
        }
    }
}
