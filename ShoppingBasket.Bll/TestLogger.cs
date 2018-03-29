using System;
using System.Diagnostics;

namespace ShoppingBasket.Bll
{
    public class TestLogger : ILogger
    {
        public void Log(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException("input");

            Debug.WriteLine(input);
        }
    }
}
