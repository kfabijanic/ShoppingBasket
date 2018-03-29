using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Microsoft.Practices.ServiceLocation;
using ShoppingBasket.Shared.Mef;

namespace ShoppingBasket.Core.Test.MockObjects
{
    public class MockDataInitialization
    {
        static CompositionContainer Container { get; set; }
        static bool IsInitialized = false;

        internal static void Initialize()
        {
            if (IsInitialized) return;

            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory));
            Container = new CompositionContainer(catalog);
            Container.ComposeParts();

            Container.ComposeExportedValue<IServiceLocator>(new MefServiceLocator(Container));

            IServiceLocator serviceLocator = Container.GetExportedValue<IServiceLocator>();
            ServiceLocator.SetLocatorProvider(() => serviceLocator);

            MockDbContext.Instance.Product.Add(new MockProductDal() { Id = 1, Name = "Butter", UnitPrice = 0.8m });
            MockDbContext.Instance.Product.Add(new MockProductDal() { Id = 2, Name = "Milk", UnitPrice = 1.15m });
            MockDbContext.Instance.Product.Add(new MockProductDal() { Id = 3, Name = "Bread", UnitPrice = 1.0m });

            MockDbContext.Instance.Discount.Add(new MockDiscountDal() { Id = 1, Description = "Buy 2 butters and get one bread at 50% off"});
            MockDbContext.Instance.DiscountCriteria.Add(new MockDiscountCriteriaDal() { Id = 1, DiscountId = 1, ProductId = 1, Quantity = 2 });
            MockDbContext.Instance.DiscountContent.Add(new MockDiscountContentDal() { Id = 2, DiscountId = 1, ProductId = 3, Quantity = 1, PercentageOff = 0.5m });

            MockDbContext.Instance.Discount.Add(new MockDiscountDal() { Id = 2, Description = "Buy 3 milks and get one 4th milk for free" });
            MockDbContext.Instance.DiscountCriteria.Add(new MockDiscountCriteriaDal() { Id = 3, DiscountId = 2, ProductId = 2, Quantity = 3 });
            MockDbContext.Instance.DiscountContent.Add(new MockDiscountContentDal() { Id = 4, DiscountId = 2, ProductId = 2, Quantity = 1, PercentageOff = 1.0m });

            IsInitialized = true;
        }
    }
}
