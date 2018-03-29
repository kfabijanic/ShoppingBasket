using System.Collections.Generic;
using System.Linq;
using ShoppingBasket.Bll.Dal;

namespace ShoppingBasket.Bll
{
    public sealed class DiscountCriteriaInfo : DiscountParameterInfo<DiscountCriteriaInfo, IDiscountCriteriaDalObject>
    {
        #region Constructors & Init
        private DiscountCriteriaInfo()
        {
            
        }
        #endregion

        #region Calculated Properties
        public override decimal Value
        {
            get
            {
                return Product.UnitPrice * Quantity;
            }
        }
        #endregion

        #region Methods
        protected override void LoadFromDal(IDiscountCriteriaDalObject dalObject)
        {
            base.LoadFromDal(dalObject);
        }
        #endregion

        #region Static Methods
        public static IEnumerable<DiscountCriteriaInfo> FetchByDiscountId(int discountId)
        {
            using (var db = DbContextProvider.GetDbContext())
            {
                foreach(var entity in db.DiscountCriteria.Where(x => x.DiscountId == discountId))
                {
                    yield return CreateInternal(entity);
                }
            }
        }
        #endregion
    }
}
