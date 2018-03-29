namespace ShoppingBasket.Shared
{
    public interface IDiscountParameterDalObject : IDalObject
    {
        int DiscountId { get; set; }
        int ProductId { get; set; }
        uint Quantity { get; set; }
    }
}
