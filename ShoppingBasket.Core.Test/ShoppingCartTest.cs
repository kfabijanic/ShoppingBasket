using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingBasket.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingBasket.Core.Test.MockObjects;

namespace ShoppingBasket.Core.Test
{
    [TestClass()]
    public class ShoppingCartTest
    {
        [ClassInitialize]
        public static void TestInit(TestContext context)
        {
            MockDataInitialization.Initialize();
        }

        [TestMethod()]
        public void CalculateTotalPriceTest()
        {
            decimal targetedPrice = 2.95m;
            var applicableDiscounts = new DiscountInfo[0];
            var shoppingCart = new ShoppingCart();
            shoppingCart.AddToCart(3, 1);
            shoppingCart.AddToCart(1, 1);
            shoppingCart.AddToCart(2, 1);
            var cartPrice = shoppingCart.CalculateTotalPrice(applicableDiscounts);
            Assert.IsTrue(cartPrice == targetedPrice);

            targetedPrice = 3.10m;
            applicableDiscounts = new DiscountInfo[1] { DiscountInfo.Fetch(1) };
            shoppingCart = new ShoppingCart();
            shoppingCart.AddToCart(1, 2);
            shoppingCart.AddToCart(3, 2);
            cartPrice = shoppingCart.CalculateTotalPrice(applicableDiscounts);
            Assert.IsTrue(cartPrice == targetedPrice);

            targetedPrice = 3.45m;
            applicableDiscounts = new DiscountInfo[1] { DiscountInfo.Fetch(2) };
            shoppingCart = new ShoppingCart();
            shoppingCart.AddToCart(2, 4);
            cartPrice = shoppingCart.CalculateTotalPrice(applicableDiscounts);
            Assert.IsTrue(cartPrice == targetedPrice);

            targetedPrice = 9.0m;
            applicableDiscounts = new DiscountInfo[2] { DiscountInfo.Fetch(1), DiscountInfo.Fetch(2) };
            shoppingCart = new ShoppingCart();
            shoppingCart.AddToCart(1, 2);
            shoppingCart.AddToCart(3, 1);
            shoppingCart.AddToCart(2, 8);
            cartPrice = shoppingCart.CalculateTotalPrice(applicableDiscounts);
            Assert.IsTrue(cartPrice == targetedPrice);
        }

        [TestMethod()]
        public void AddToCartTest()
        {
            var shoppingCart = new ShoppingCart();
            shoppingCart.AddToCart(1);
            Assert.IsTrue(shoppingCart.Items.Count() == 1);
            Assert.IsTrue(shoppingCart.Items.Single().ProductId == 1);
            Assert.IsTrue(shoppingCart.Items.Single().Quantity == 1);

            shoppingCart.AddToCart(1);
            Assert.IsTrue(shoppingCart.Items.Count() == 1);
            Assert.IsTrue(shoppingCart.Items.Single().ProductId == 1);
            Assert.IsTrue(shoppingCart.Items.Single().Quantity == 2);

            shoppingCart.AddToCart(2);
            Assert.IsTrue(shoppingCart.Items.Count() == 2);
            Assert.IsTrue(shoppingCart.Items.Count(x => x.ProductId == 2) == 1);
            Assert.IsTrue(shoppingCart.Items.Sum(x => x.Quantity) == 3);
        }

        [TestMethod()]
        public void AddToCartTest1()
        {
            var shoppingCart = new ShoppingCart();
            shoppingCart.AddToCart(1, 2);
            Assert.IsTrue(shoppingCart.Items.Count() == 1);
            Assert.IsTrue(shoppingCart.Items.Sum(x => x.Quantity) == 2);

            shoppingCart.AddToCart(1, 1);
            Assert.IsTrue(shoppingCart.Items.Count() == 1);
            Assert.IsTrue(shoppingCart.Items.Sum(x => x.Quantity) == 1);

            shoppingCart.AddToCart(2, 1);
            Assert.IsTrue(shoppingCart.Items.Count() == 2);
            Assert.IsTrue(shoppingCart.Items.Sum(x => x.Quantity) == 2);
        }

        [TestMethod()]
        public void RemoveFromCartTest()
        {
            var shoppingCart = new ShoppingCart();
            shoppingCart.AddToCart(1);
            shoppingCart.AddToCart(2, 1);

            shoppingCart.RemoveFromCart(1);
            Assert.IsTrue(shoppingCart.Items.Count() == 1);
            Assert.IsTrue(shoppingCart.Items.Single().ProductId == 2);

            shoppingCart.RemoveFromCart(3);
            Assert.IsTrue(shoppingCart.Items.Count() == 1);
            Assert.IsTrue(shoppingCart.Items.Single().ProductId == 2);

            shoppingCart.RemoveFromCart(2);
            Assert.IsTrue(shoppingCart.Items.Count() == 0);
        }
    }
}