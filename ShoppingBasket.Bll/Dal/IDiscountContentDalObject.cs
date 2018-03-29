namespace ShoppingBasket.Bll.Dal
{
    public interface IDiscountContentDalObject : IDiscountParameterDalObject
    {
        decimal PercentageOff { get; set; }
    }
}
