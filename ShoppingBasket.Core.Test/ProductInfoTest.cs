using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingBasket.Core.Test.MockObjects;

namespace ShoppingBasket.Core.Test
{
    [TestClass()]
    public class ProductInfoTest
    {
        [ClassInitialize]
        public static void TestInit(TestContext context)
        {
            MockDataInitialization.Initialize();
        }

        [TestMethod()]
        public void FetchTest()
        {
            int id = 1;
            var item = ProductInfo.Fetch(id);
            Assert.IsTrue(item != null);
            Assert.IsTrue(item.Id == id);

            id = 0;
            item = ProductInfo.Fetch(id);
            Assert.IsTrue(item == null);
        }
    }
}