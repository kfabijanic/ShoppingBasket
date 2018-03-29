namespace ShoppingBasket.Shared
{
    public interface IDiscountContentDalObject : IDiscountParameterDalObject
    {
        decimal PercentageOff { get; set; }
    }
}
