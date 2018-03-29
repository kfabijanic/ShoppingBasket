using System.Collections.Generic;
using System.Linq;
using ShoppingBasket.Shared;

namespace ShoppingBasket.Core
{
    /// <summary>
    /// Readonly klasa za vrijednost popusta
    /// </summary>
    public sealed class DiscountContentInfo : DiscountParameterInfo<DiscountContentInfo, IDiscountContentDalObject>
    {
        #region Constructors & Init
        private DiscountContentInfo()
        {

        }
        #endregion

        #region Properties
        public decimal PercentageOff { get; private set; }
        #endregion

        #region Calculated Properties
        public override decimal Value
        {
            get
            {
                return Product.UnitPrice * Quantity * (1m - PercentageOff);
            }
        }
        #endregion

        #region Methods
        protected override void LoadFromDal(IDiscountContentDalObject dalObject)
        {
            base.LoadFromDal(dalObject);
            PercentageOff = dalObject.PercentageOff;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1} ({2}%)", Product.Name, Quantity, PercentageOff);
        }
        #endregion

        #region Static Methods
        public static IEnumerable<DiscountContentInfo> FetchByDiscountId(int discountId)
        {
            using (var db = DbContextProvider.GetDbContext())
            {
                foreach (var entity in db.DiscountContent.Where(x => x.DiscountId == discountId))
                {
                    yield return CreateInternal(entity);
                }
            }
        }
        #endregion
    }
}
