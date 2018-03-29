using System;
using Microsoft.Practices.ServiceLocation;
using ShoppingBasket.Shared;

namespace ShoppingBasket.Core
{
    public abstract class BusinessBaseInfo
    {
        #region DbContextProvider
        private static IDbContextProvider _DbContextProvider;
        public static IDbContextProvider DbContextProvider
        {
            get
            {
                if (_DbContextProvider == null)
                {
                    _DbContextProvider = ServiceLocator.Current.GetInstance<IDbContextProvider>();
                }
                return _DbContextProvider;
            }
        }
        #endregion
    }

    public abstract class BusinessBaseInfo<TBll, TDal> : BusinessBaseInfo
        where TBll : BusinessBaseInfo<TBll, TDal>
        where TDal : IDalObject
    {
        #region Properties
        public int Id { get; private set; }
        #endregion

        #region Methods
        protected virtual void LoadFromDal(TDal dalObject)
        {
            if (dalObject == null)
                throw new ArgumentNullException("dalObject");

            Id = dalObject.Id;
        }
        #endregion

        #region Static & Factory Methods
        protected internal static TBll CreateInternal(TDal dalObject)
        {
            TBll item = (TBll)Activator.CreateInstance(typeof(TBll), true);
            item.LoadFromDal(dalObject);
            return item;
        }
        #endregion
    }
}
