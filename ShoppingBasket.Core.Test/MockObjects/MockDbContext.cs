using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingBasket.Shared;

namespace ShoppingBasket.Core.Test.MockObjects
{
    public sealed class MockDbContext : IDbContext
    {
        #region Constructors & Init
        private MockDbContext()
        {
            Product = new List<IProductDalObject>();
            Discount = new List<IDiscountDalObject>();
            DiscountCriteria = new List<IDiscountCriteriaDalObject>();
            DiscountContent = new List<IDiscountContentDalObject>();
        }
        #endregion

        #region Properties
        public ICollection<IProductDalObject> Product { get; set; }
        public ICollection<IDiscountDalObject> Discount { get; set; }
        public ICollection<IDiscountCriteriaDalObject> DiscountCriteria { get; set; }
        public ICollection<IDiscountContentDalObject> DiscountContent { get; set; }
        #endregion

        #region Instance
        private static MockDbContext _Instance = null;
        public static MockDbContext Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MockDbContext();
                }
                return _Instance;
            }
        }
        #endregion

        #region IDisposable
        void IDisposable.Dispose()
        {

        }
        #endregion

    }

    #region Dal Classes
    public sealed class MockProductDal : IProductDalObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public sealed class MockDiscountDal : IDiscountDalObject
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public ICollection<IDiscountCriteriaDalObject> DiscountCriteria { get { return MockDbContext.Instance.DiscountCriteria.Where(x => x.DiscountId == Id).ToList(); } }
        public ICollection<IDiscountContentDalObject> DiscountContent { get { return MockDbContext.Instance.DiscountContent.Where(x => x.DiscountId == Id).ToList(); } }
    }

    public sealed class MockDiscountCriteriaDal : IDiscountCriteriaDalObject
    {
        public int Id { get; set; }
        public int DiscountId { get; set; }
        public int ProductId { get; set; }
        public uint Quantity { get; set; }
    }

    public sealed class MockDiscountContentDal : IDiscountContentDalObject
    {
        public int Id { get; set; }
        public int DiscountId { get; set; }
        public int ProductId { get; set; }
        public uint Quantity { get; set; }
        public decimal PercentageOff { get; set; }
    }
    #endregion
}
