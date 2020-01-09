using NUnit.Framework;
using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportStoreTests
{
    [TextFixture]
    public class CartTests
    {
        [Test]
        public void Cart_AddNewLines_Success()
        {
            //Arrange 

            var product1 = new Product() { ProductId = 0, Name = "p1" };
            var product2 = new Product() { ProductId = 1, Name = "p2" };

            var cart = new Cart();

            //Act

            cart.AddItem(product1, 1);
            cart.AddItem(product2, 1);

            var lines = cart.Lines.ToArray();

            //Assert
            Assert.AreEqual(2, lines.Length);
            Assert.AreEqual(product1, lines[0].Product);
            Assert.AreEqual(product2, lines[1].Product);
        }

        [Test]
        public void Cart_AddSameProducts_Success()
        {
            //Arrange 

            var product1 = new Product() { ProductId = 0, Name = "p1" };
            var product2 = new Product() { ProductId = 1, Name = "p2" };

            var cart = new Cart();

            //Act

            cart.AddItem(product1, 1);
            cart.AddItem(product2, 1);
            cart.AddItem(product1, 10);

            var lines = cart.Lines.ToArray();

            //Assert
            Assert.AreEqual(2, lines.Length);
            Assert.AreEqual(11, lines[0].Quantity);
            Assert.AreEqual(1, lines[1].Quantity);
        }

        [Test]
        public void Cart_RemoveLine_Success()
        {
            //Arrange 

            var product1 = new Product() { ProductId = 0, Name = "p1" };
            var product2 = new Product() { ProductId = 1, Name = "p2" };
            var product3 = new Product() { ProductId = 2, Name = "p3" };

            var cart = new Cart();

            cart.AddItem(product1, 1);
            cart.AddItem(product2, 3);
            cart.AddItem(product3, 5);
            cart.AddItem(product2, 1);

            //Act
            cart.RemoveLine(product2);

            //Assert
            Assert.AreEqual(0, cart.Lines.Count(l => l.Product == product2));
            Assert.AreEqual(2, cart.Lines.Count());
        }

        [Test]
        public void Cart_TotalCalculate_Success()
        {
            //Arrange 

            var product1 = new Product() { ProductId = 0, Name = "p1", Price = 100 };
            var product2 = new Product() { ProductId = 1, Name = "p2", Price = 50 };

            var cart = new Cart();

            //Act

            cart.AddItem(product1, 1);
            cart.AddItem(product2, 1);
            cart.AddItem(product1, 1);

            var total = cart.TotalValue;

            //Assert
            Assert.AreEqual(250, total);
        }


        [Test]
        public void Cart_ClearCart_Success()
        {
            //Arrange 

            var product1 = new Product() { ProductId = 0, Name = "p1", Price = 100 };
            var product2 = new Product() { ProductId = 1, Name = "p2", Price = 50 };

            var cart = new Cart();

            //Act

            cart.AddItem(product1, 1);
            cart.AddItem(product2, 1);
            cart.AddItem(product1, 1);

            cart.Clear();

            //Assert
            Assert.AreEqual(0, cart.Lines.Count());
        }
    }
}
