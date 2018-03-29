using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingBasket.Core.Test.MockObjects;

namespace ShoppingBasket.Core.Test
{
    [TestClass()]
    public class DiscountInfoTest
    {
        [ClassInitialize]
        public static void TestInit(TestContext context)
        {
            MockDataInitialization.Initialize();
        }

        [TestMethod()]
        public void FetchApplicableDiscountsTest()
        {
            ProductQuantity[] productQuantites = new ProductQuantity[0];
            var applicableDiscounts = DiscountInfo.FetchApplicableDiscounts(productQuantites).ToArray();
            Assert.IsTrue(applicableDiscounts.Length == 0);


            productQuantites = new ProductQuantity[] { new ProductQuantity(1, 2), new ProductQuantity(3, 1) };
            applicableDiscounts = DiscountInfo.FetchApplicableDiscounts(productQuantites).ToArray();
            Assert.IsTrue(applicableDiscounts.Length == 1);
            Assert.IsTrue(applicableDiscounts[0].Id == 1);

            productQuantites = new ProductQuantity[] { new ProductQuantity(1, 2), new ProductQuantity(2, 3) };
            applicableDiscounts = DiscountInfo.FetchApplicableDiscounts(productQuantites).ToArray();
            Assert.IsTrue(applicableDiscounts.Length == 2);

            productQuantites = new ProductQuantity[] { new ProductQuantity(3, 1), new ProductQuantity(2, 3) };
            applicableDiscounts = DiscountInfo.FetchApplicableDiscounts(productQuantites).ToArray();
            Assert.IsTrue(applicableDiscounts.Length == 1);
            Assert.IsTrue(applicableDiscounts[0].Id == 2);

            productQuantites = new ProductQuantity[] { new ProductQuantity(3, 1) };
            applicableDiscounts = DiscountInfo.FetchApplicableDiscounts(productQuantites).ToArray();
            Assert.IsTrue(applicableDiscounts.Length == 0);
        }
    }
}