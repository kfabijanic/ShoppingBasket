using System.Linq;
using ShoppingBasket.Shared;

namespace ShoppingBasket.Core
{
    /// <summary>
    /// Readonly klasa za proizvod
    /// </summary>
    public sealed class ProductInfo : BusinessBaseInfo<ProductInfo, IProductDalObject>
    {
        #region Constructors & Init
        private ProductInfo()
        {

        }
        #endregion

        #region Properties
        public string Name { get; private set; }
        public decimal UnitPrice { get; private set; }
        #endregion

        #region Methods
        protected override void LoadFromDal(IProductDalObject dalObject)
        {
            base.LoadFromDal(dalObject);
            Name = dalObject.Name;
            UnitPrice = dalObject.UnitPrice;
        }

        public override string ToString()
        {
            return Name;
        }
        #endregion

        #region Static & Factory Methods
        public static ProductInfo Fetch(int id)
        {
            ProductInfo item = null;
            using (var db = DbContextProvider.GetDbContext())
            {
                var entity = db.Product.SingleOrDefault(x => x.Id == id);
                if (entity != null)
                {
                    item = CreateInternal(entity);
                }
            }
            return item;
        }
        #endregion
    }
}
