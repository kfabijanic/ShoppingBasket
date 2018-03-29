namespace ShoppingBasket.Shared
{
    public interface IProductDalObject : IDalObject
    {
        string Name { get; set; }
        decimal UnitPrice { get; set; }
    }
}
