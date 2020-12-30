using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductService.Exceptions
{
    /// <summary>
    /// This exception is raised if the product quantity to be extracted is greater than
    /// the remaining stock.
    /// </summary>
    [Serializable]
    public class NegativeStockException : Exception
    {
        private long ProductIdentifier { get; }
        private int CurrentStock { get; }
        private int RequestedQuantity { get; }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="NegativeStockException"/> class.
        /// </summary>
        /// <param name="productIdentifier"> The product identifier. </param>
        /// <param name="currentStock"> The current stock. </param>
        /// <param name="requestedQuantity"> The requested quantity. </param>
        public NegativeStockException(long productIdentifier, int currentStock, int requestedQuantity)
            : base("Negative stock exception => " +
            "ProductIdentifier = " + productIdentifier + " | " +
            "CurrentStock = " + currentStock + " | " +
            "RequestedQuantity = " + requestedQuantity)
        {
            ProductIdentifier = productIdentifier;
            CurrentStock = currentStock;
            RequestedQuantity = requestedQuantity;
        }
    }
}
