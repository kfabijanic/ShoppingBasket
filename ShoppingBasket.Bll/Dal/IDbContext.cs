using System;
using System.Collections.Generic;

namespace ShoppingBasket.Bll.Dal
{
    public interface IDbContext : IDisposable
    {
        ICollection<IProductDalObject> Product { get; }
        ICollection<IDiscountDalObject> Discount { get; }
        ICollection<IDiscountCriteriaDalObject> DiscountCriteria { get; }
        ICollection<IDiscountContentDalObject> DiscountContent { get; }
    }
}
