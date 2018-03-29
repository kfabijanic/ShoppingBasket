using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using Microsoft.Practices.ServiceLocation;

namespace ShoppingBasket.Shared.Mef
{
    public sealed class MefServiceLocator : ServiceLocatorImplBase
    {
        #region CompositionContainer
        private readonly CompositionContainer _CompositionContainer;
        #endregion

        #region Constructor
        public MefServiceLocator(CompositionContainer compositionContainer)
        {
            _CompositionContainer = compositionContainer;
        }
        #endregion

        #region Overrides
        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            List<object> instances = new List<object>();
            IEnumerable<Lazy<object, object>> exports = _CompositionContainer.GetExports(serviceType, null, null);
            if (exports != null)
            {
                instances.AddRange(exports.Select(export => export.Value));
            }
            return instances;
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            IEnumerable<Lazy<object, object>> exports = _CompositionContainer.GetExports(serviceType, null, key);
            if ((exports != null) && (exports.Count() > 0))
            {
                return exports.Single().Value;
            }
            else
            {
                throw new ActivationException(FormatActivationExceptionMessage(new CompositionException("Export not found"), serviceType, key));
            }
        }
        #endregion
    }
}
