using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;
using SportStore.Controllers;
using SportStore.Models;
using System.Collections.Generic;
using System.Linq;

namespace SportStoreTests
{
    [TestFixture]
    public class AdminControllerTests
    {
        [Test]
        public void AdminController_IndexContains_AllProducts()
        {
            //Arrange

            var repo = new Mock<IProductRepository>();
            repo.Setup(m => m.Products).Returns(new Product[]{
                new Product {ProductId =1, Name = "P1"},
                new Product {ProductId =2, Name = "P2"},
                new Product {ProductId =3, Name = "P3"},
            }.AsQueryable<Product>());

            var controller = new AdminController(repo.Object);

            //Act

            var result = GetViewModel<IEnumerable<Product>>(controller.Index())?.ToArray();

            //Assert
            Assert.AreEqual(3, result.Length);
            Assert.IsTrue("P1".Equals(result[0].Name));
            Assert.IsTrue("P2".Equals(result[1].Name));
            Assert.IsTrue("P3".Equals(result[2].Name));
        }

        [Test]
        public void AdminController_EditProduct_CanEdit()
        {
            var repo = new Mock<IProductRepository>();
            repo.Setup(m => m.Products).Returns(new Product[]{
                new Product {ProductId =1, Name = "P1"},
                new Product {ProductId =2, Name = "P2"},
                new Product {ProductId =3, Name = "P3"},
            }.AsQueryable<Product>());

            var controller = new AdminController(repo.Object);

            //Act
            var p1 = GetViewModel<Product>(controller.Edit(1));
            var p2 = GetViewModel<Product>(controller.Edit(2));
            var p3 = GetViewModel<Product>(controller.Edit(3));

            //Assert
            Assert.AreEqual(1, p1.ProductId);
            Assert.AreEqual(2, p2.ProductId);
            Assert.AreEqual(3, p3.ProductId);
        }

        [Test]
        public void AdminController_EditUnexistedProduct_CantEdit()
        {
            var repo = new Mock<IProductRepository>();
            repo.Setup(m => m.Products).Returns(new Product[]{
                new Product {ProductId =1, Name = "P1"},
                new Product {ProductId =2, Name = "P2"},
                new Product {ProductId =3, Name = "P3"},
            }.AsQueryable<Product>());

            var controller = new AdminController(repo.Object);

            //Act
            var p = GetViewModel<Product>(controller.Edit(4));

            //Assert
            Assert.IsNull(p);
        }

        T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }

        [Test]
        public void AdminCOntroller_SaveValidChange_Save()
        {
            //Arrange
            var repo = new Mock<IProductRepository>();
            var tempData = new Mock<ITempDataDictionary>();
            var controller = new AdminController(repo.Object)
            {
                TempData = tempData.Object
            };
            var product = new Product { Name = "Test" };

            //Act
            var result = controller.Edit(product) as RedirectToActionResult;

            //Assert
            repo.Verify(r => r.SaveProduct(product));
            Assert.IsNotNull(result);
            Assert.IsTrue("Index".Equals(result.ActionName));
        }

        [Test]
        public void AdminCOntroller_SaveInValidChange_CanNotSave()
        {
            //Arrange
            var repo = new Mock<IProductRepository>();
            var tempData = new Mock<ITempDataDictionary>();
            var controller = new AdminController(repo.Object)
            {
                TempData = tempData.Object
            };
            var product = new Product { Name = "Test" };
            controller.ModelState.AddModelError("error", "error");

            //Act
            var result = controller.Edit(product);

            //Assert
            repo.Verify(r => r.SaveProduct(It.IsAny<Product>()), Times.Never());
            Assert.IsTrue(result is ViewResult);
        }

        [Test]
        public void AdminController_Delete_CanDelete()
        {
            //Arrange

            var product = new Product { ProductId = 2, Name = "P2" };

            var repo = new Mock<IProductRepository>();
            repo.Setup(m => m.Products).Returns(new Product[]{
                new Product {ProductId =1, Name = "P1"},
                product,
                new Product {ProductId =3, Name = "P3"},
            }.AsQueryable<Product>());

            var controller = new AdminController(repo.Object);

            //Act
            controller.Delete(2);

            //Assert
            repo.Verify(r => r.DeleteProduct(product.ProductId));
        }

    }
}
