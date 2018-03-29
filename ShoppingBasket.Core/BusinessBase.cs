using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Practices.ServiceLocation;
using ShoppingBasket.Shared;

namespace ShoppingBasket.Core
{
    public abstract class BusinessBase
    {
        #region Constructors & Init
        protected BusinessBase()
        {
            Id = Guid.NewGuid();
            _ValidationErrors = new Dictionary<PropertyInfo, List<string>>();
        }
        #endregion

        #region Properties
        public Guid Id { get; private set; }
        #endregion

        #region Validation Errors
        public bool IsValid
        {
            get { return _ValidationErrors.Count == 0; }
        }

        private Dictionary<PropertyInfo, List<string>> _ValidationErrors;

        protected void AddValidationError(PropertyInfo property, string error)
        {
            if (!_ValidationErrors.ContainsKey(property))
            {
                _ValidationErrors.Add(property, new List<string>());
            }

            if (!_ValidationErrors[property].Contains(error))
            {
                _ValidationErrors[property].Add(error);
            }
        }

        protected void ClearPropertyValidationErrors(PropertyInfo property)
        {
            if (_ValidationErrors.ContainsKey(property))
            {
                _ValidationErrors.Remove(property);
            }
        }

        protected void ClearValidationErrors()
        {
            _ValidationErrors.Clear();
        }
        #endregion

        #region SetPropertyValue
        protected void SetPropertyValue<T>(ref T propValue, T newValue)
        {
            SetPropertyValue(ref propValue, newValue, null, null);
        }

        protected void SetPropertyValue<T>(ref T propValue, T newValue, Action<T> propertyValidationRule)
        {
            SetPropertyValue(ref propValue, newValue, propertyValidationRule, null);
        }

        protected void SetPropertyValue<T>(ref T propValue, T newValue, Action<T, T> propertyChangedCallback)
        {
            SetPropertyValue(ref propValue, newValue, null, propertyChangedCallback);
        }

        protected void SetPropertyValue<T>(ref T propValue, T newValue, Action<T> propertyValidationRule, Action<T, T> propertyChangedCallback)
        {
            T valueToSet = newValue;
            T oldValue = propValue;
            if ((typeof(T).IsValueType && !Equals(propValue, valueToSet) || !typeof(T).IsValueType && !ReferenceEquals(propValue, valueToSet)))
            {
                propValue = valueToSet;
                propertyValidationRule?.Invoke(propValue);
                propertyChangedCallback?.Invoke(oldValue, valueToSet);
            }
        }
        #endregion

        #region Logger
        private static readonly ILogger _Logger = ServiceLocator.Current.GetInstance<ILogger>();
        #endregion
    }
}
