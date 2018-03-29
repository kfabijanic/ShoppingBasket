namespace ShoppingBasket.Bll.Dal
{
    public interface IDiscountParameterDalObject : IDalObject
    {
        int DiscountId { get; set; }
        int ProductId { get; set; }
        uint Quantity { get; set; }
    }
}
