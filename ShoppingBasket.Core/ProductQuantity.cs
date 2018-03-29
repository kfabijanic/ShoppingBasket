namespace ShoppingBasket.Core
{
    public sealed class ProductQuantity
    {
        #region Constructors & Init
        public ProductQuantity(int productId)
        {
            ProductId = productId;
        }

        public ProductQuantity(int productId, uint quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
        #endregion

        #region Properties
        public int ProductId { get; set; }
        public uint Quantity { get; set; }
        #endregion
    }
}
