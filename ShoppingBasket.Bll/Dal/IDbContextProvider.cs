namespace ShoppingBasket.Bll.Dal
{
    public interface IDbContextProvider
    {
        IDbContext GetDbContext();
    }
}
