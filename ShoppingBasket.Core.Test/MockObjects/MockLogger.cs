using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using ShoppingBasket.Shared;

namespace ShoppingBasket.Core.Test.MockObjects
{
    [Export(typeof(ILogger))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class MockLogger  : ILogger
    {
        public void Log(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException("input");

            Debug.WriteLine(input);
        }
    }
}
