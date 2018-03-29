using System.ComponentModel.Composition;

namespace ShoppingBasket.Bll.Dal
{
    [Export(typeof(IDbContextProvider))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public sealed class MockContextProvider : IDbContextProvider
    {
        public IDbContext GetDbContext()
        {
            return MockDbContext.Instance;
        }
    }
}
