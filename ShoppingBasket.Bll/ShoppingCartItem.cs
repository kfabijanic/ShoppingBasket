using System;

namespace ShoppingBasket.Bll
{
    public class ShoppingCartItem
    {
        #region Constructors & Init
        public ShoppingCartItem(int productId)
        {
            ProductId = productId;
            Quantity = 1;
        }

        public ShoppingCartItem(int productId, uint quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
        #endregion

        #region Properties
        public int ProductId { get; private set; }

        #region Quantity
        private uint _Quantity;
        public uint Quantity
        {
            get { return _Quantity; }
            set
            {
                _Quantity = value != 0 ? value : 1;
            }
        }
        #endregion
        #endregion

        #region Calculated Properties
        public decimal Value
        {
            get
            {
                return Product.UnitPrice * Quantity;
            }
        }

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

    }
}
