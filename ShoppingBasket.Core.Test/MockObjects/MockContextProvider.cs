using System.ComponentModel.Composition;
using ShoppingBasket.Shared;

namespace ShoppingBasket.Core.Test.MockObjects
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
