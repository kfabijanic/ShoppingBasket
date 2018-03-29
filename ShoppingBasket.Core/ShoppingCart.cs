using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;
using ShoppingBasket.Shared;

namespace ShoppingBasket.Core
{
    public class ShoppingCart
    {
        #region Constructors & Init
        public ShoppingCart()
        {
            _Items = new List<ShoppingCartItem>();
        }
        #endregion

        #region Fields
        private List<ShoppingCartItem> _Items;
        private static readonly ILogger _Logger = ServiceLocator.Current.GetInstance<ILogger>();
        #endregion

        #region Properties
        public IEnumerable<ShoppingCartItem> Items
        {
            get
            {
                foreach (var item in _Items)
                {
                    yield return item;
                }
            }
        }
        #endregion

        #region Calculated Properties
        public decimal TotalPrice
        {
            get
            {
                return CalculateTotalPrice(DiscountInfo.FetchApplicableDiscounts(_Items.Select(x => new ProductQuantity(x.ProductId, x.Quantity)).ToArray()).ToArray());
            }
        }
        #endregion

        #region Methods
        public void AddToCart(int productId)
        {
            var item = _Items.SingleOrDefault(x => x.ProductId == productId);
            if (item == null)
            {
                item = new ShoppingCartItem(productId);
                _Items.Add(item);
            }
            else
            {
                item.Quantity += 1;
            }
        }

        public void AddToCart(int productId, uint quantity)
        {
            var item = _Items.SingleOrDefault(x => x.ProductId == productId);
            if (item == null)
            {
                item = new ShoppingCartItem(productId);
                _Items.Add(item);
            }
            item.Quantity = quantity;
        }

        public void RemoveFromCart(int productId)
        {
            var item = _Items.SingleOrDefault(x => x.ProductId == productId);
            if (item != null)
            {
                _Items.Remove(item);
            }
        }

        public decimal CalculateTotalPrice(DiscountInfo[] applicableDiscounts)
        {
            decimal totalPrice = 0m;
            List<DiscountContentInfo> usedDiscounts = new List<DiscountContentInfo>();
            if (applicableDiscounts == null || applicableDiscounts.Length == 0)
            {
                totalPrice = _Items.Sum(x => x.Quantity * x.Product.UnitPrice);
            }
            else
            {
                var remainingCartItems = _Items.ToList();
                foreach (var discount in applicableDiscounts.OrderBy(x => x.ContentValue).ThenBy(x => x.Criteria.Sum(xx => xx.Quantity)))
                {
                    while (discount.Criteria.Count == 0
                        || discount.Criteria.All(x => remainingCartItems.Where(xx => xx.ProductId == x.ProductId).Sum(xx => xx.Quantity) >= x.Quantity))
                    {
                        
                        foreach (var dCriteriaItem in discount.Criteria)
                        {
                            totalPrice += dCriteriaItem.Value;
                            var cartItem = remainingCartItems.Single(x => x.ProductId == dCriteriaItem.ProductId);
                            if (cartItem.Quantity > dCriteriaItem.Quantity)
                            {
                                cartItem.Quantity -= dCriteriaItem.Quantity;
                            }
                            else
                            {
                                remainingCartItems.Remove(cartItem);
                            }
                        }
                        foreach (var dContentItem in discount.Content)
                        {
                            var cartItem = remainingCartItems.SingleOrDefault(x => x.ProductId == dContentItem.ProductId);
                            if (cartItem != null)
                            {
                                totalPrice += dContentItem.Value;

                                if (cartItem.Quantity > dContentItem.Quantity)
                                {
                                    cartItem.Quantity -= dContentItem.Quantity;
                                }
                                else
                                {
                                    remainingCartItems.Remove(cartItem);
                                }

                            }
                            usedDiscounts.Add(dContentItem);
                        }
                    }
                }

                totalPrice += remainingCartItems.Sum(x => x.Value);
            }
            _Logger?.Log(GetCartInfo(usedDiscounts.ToArray(), totalPrice));
            return totalPrice;
        }

        private string GetCartInfo(DiscountContentInfo[] applicableDiscounts, decimal totalPrice)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Product\tQuantity\tUnitPrice($)");
            foreach (var item in _Items)
            {
                sb.AppendFormat("{0}\t{1}\t{2:F2}", item.Product.Name, item.Quantity, item.Product.UnitPrice);
                sb.AppendLine();
            }
            sb.AppendLine("----------------------");
            if (applicableDiscounts != null && applicableDiscounts.Length > 0)
            {
                sb.AppendLine("DISCOUNTS - Product\tQuantity\tOff (%)");
                foreach (var item in applicableDiscounts)
                {
                    sb.AppendFormat("            {0}\t{1}\t{2:F2}", item.Product.Name, item.Quantity, item.PercentageOff);
                    sb.AppendLine();
                }
                sb.AppendLine("----------------------");
                sb.AppendFormat("TOTAL: {0:F2}", totalPrice);
            }

            return sb.ToString();
        }
        #endregion
    }
}
