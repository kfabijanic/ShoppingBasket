using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ShoppingBasket.Bll.Dal;

namespace ShoppingBasket.Bll
{
    /// <summary>
    /// Readonly klasa za popuste
    /// </summary>
    public sealed class DiscountInfo : BusinessBaseInfo<DiscountInfo, IDiscountDalObject>
    {
        #region Constructors & Init
        private DiscountInfo()
        {
            
        }
        #endregion

        #region Properties
        public string Description { get; set; }
        public ReadOnlyCollection<DiscountCriteriaInfo> Criteria { get; private set; }
        public ReadOnlyCollection<DiscountContentInfo> Content { get; private set; }
        #endregion

        #region Calculated Properties
        public decimal DiscountValue
        {
            get
            {
                return Content.Sum(x => x.Value);
            }
        }
        #endregion

        #region Methods
        protected override void LoadFromDal(IDiscountDalObject dalObject)
        {
            base.LoadFromDal(dalObject);
            Description = dalObject.Description;
            Criteria = new ReadOnlyCollection<DiscountCriteriaInfo>(dalObject.DiscountCriteria.Select(x => DiscountCriteriaInfo.CreateInternal(x)).ToList());
            Content = new ReadOnlyCollection<DiscountContentInfo>(dalObject.DiscountContent.Select(x => DiscountContentInfo.CreateInternal(x)).ToList());

        }
        #endregion

        #region Static & Factory Methods
        public static DiscountInfo Fetch(int id)
        {
            DiscountInfo item = null;
            using (var db = DbContextProvider.GetDbContext())
            {
                var entity = db.Discount.SingleOrDefault(x => x.Id == id);
                if(entity != null)
                {
                    item = CreateInternal(entity);
                }
            }
            return item;
        }

        /// <summary>
        /// Funkcija vraća popuste koji zadovoljavaju kriterije ovisno o vrsti i količini proizvoda
        /// </summary>
        /// <param name="productQuantites"></param>
        /// <returns></returns>
        public static IEnumerable<DiscountInfo> FetchApplicableDiscounts(ProductQuantity[] productQuantites)
        {
            if (productQuantites == null)
                throw new ArgumentNullException("productQuantites");

            using (var db = DbContextProvider.GetDbContext())
            {
                foreach(var item in db.Discount.Where(x => x.DiscountCriteria.All(xx => productQuantites.Where(xxx => xxx.ProductId == xx.ProductId).Count() >= xx.Quantity)))
                {
                    yield return CreateInternal(item);
                }
            }
        }
        #endregion
    }
}
