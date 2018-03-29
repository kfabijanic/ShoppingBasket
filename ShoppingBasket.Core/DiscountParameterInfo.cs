using ShoppingBasket.Shared;

namespace ShoppingBasket.Core
{
    public abstract class DiscountParameterInfo<TBll, TDal> : BusinessBaseInfo<TBll, TDal>
        where TBll : DiscountParameterInfo<TBll, TDal>
        where TDal : IDiscountParameterDalObject
    {
        #region Properties
        public int DiscountId { get; private set; }
        public int ProductId { get; private set; }
        public uint Quantity { get; private set; }
        #endregion

        #region Calculated Properties
        public abstract decimal Value { get; }

        #region Discount
        private DiscountInfo _Discount;
        public DiscountInfo Discount
        {
            get
            {
                if (_Discount == null || _Discount.Id != DiscountId)
                {
                    _Discount = DiscountInfo.Fetch(DiscountId);
                }
                return _Discount;
            }
        }
        #endregion

        #region Product
        private ProductInfo _Product;
        public ProductInfo Product
        {
            get
            {
                if (_Product == null || _Product.Id != ProductId)
                {
                    _Product = ProductInfo.Fetch(ProductId);
                }
                return _Product;
            }
        }
        #endregion
        #endregion

        #region Methods
        protected override void LoadFromDal(TDal dalObject)
        {
            base.LoadFromDal(dalObject);
            DiscountId = dalObject.DiscountId;
            ProductId = dalObject.ProductId;
            Quantity = dalObject.Quantity;
        }
        #endregion
    }
}
