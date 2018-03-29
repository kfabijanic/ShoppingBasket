using System.Collections.Generic;

namespace ShoppingBasket.Bll.Dal
{
    public interface IDiscountDalObject : IDalObject
    {
        string Description { get; set; }
        ICollection<IDiscountCriteriaDalObject> DiscountCriteria { get; }
        ICollection<IDiscountContentDalObject> DiscountContent { get; }
    }
}
