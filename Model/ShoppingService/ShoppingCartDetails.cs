using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService
{
    /// <summary>
    /// VO Class which contains the shopping cart details
    /// </summary>
    [Serializable()]
    public class ShoppingCartDetails
    {
        #region Properties Region

        public int DeliveryLineAmount { get; set; }

        public decimal DeliveryLinePrice { get; set; }

        public long ProductId { get; set; }
        
        #endregion Properties Region

        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingCartDetails"/> class
        /// </summary>
        /// <param name="deliveryLineAmount"> The amount of items of a product. </param>
        /// <param name="deliveryLinePrice"> The price of the product. </param>
        /// <param name="productId"> The product id. </param>
        public ShoppingCartDetails(int deliveryLineAmount, 
            decimal deliveryLinePrice, long productId)
        {
            DeliveryLineAmount = deliveryLineAmount;
            DeliveryLinePrice = deliveryLinePrice;
            ProductId = productId;
        }

        public override bool Equals(object obj)
        {
            var details = obj as ShoppingCartDetails;
            return details != null &&
                   DeliveryLineAmount == details.DeliveryLineAmount &&
                   DeliveryLinePrice == details.DeliveryLinePrice &&
                   ProductId == details.ProductId;
        }

        public override int GetHashCode()
        {
            var hashCode = 531276092;
            hashCode = hashCode * -1521134295 + DeliveryLineAmount.GetHashCode();
            hashCode = hashCode * -1521134295 + DeliveryLinePrice.GetHashCode();
            hashCode = hashCode * -1521134295 + ProductId.GetHashCode();
            return hashCode;
        }
    }
}
