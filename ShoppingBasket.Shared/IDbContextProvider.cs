namespace ShoppingBasket.Shared
{
    public interface IDbContextProvider
    {
        IDbContext GetDbContext();
    }
}
