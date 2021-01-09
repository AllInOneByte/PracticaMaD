using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService.Exceptions
{
    public class StockEmptyException : Exception
    {
        private long ProductIdentifier { get; }
        private string ProductName { get; }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="StockEmptyException"/> class.
        /// </summary>
        /// <param name="productIdentifier"> The product identifier. </param>
        /// <param name="productName"> The product name. </param>
        public StockEmptyException(long productIdentifier, string productName) 
            : base("StockEmptyException => " +
                  "ProductIdentifier = " + productIdentifier + " | "
                  + "ProductName = " + productName)
        {
            ProductIdentifier = productIdentifier;
            ProductName = productName;
        }
    }
}
