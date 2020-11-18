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

        public int DeliveryLineAmount { get; private set; }

        public decimal DeliveryLinePrice { get; private set; }

        public long DeliveryId { get; private set; }

        public long ProductId { get; private set; }
        
        #endregion Properties Region

        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingCartDetails"/> class
        /// </summary>
        /// <param name="deliveryLineAmount"> The amount of items of a product. </param>
        /// <param name="deliveryLinePrice"> The price of the product. </param>
        /// <param name="deliveryId"> The delivery id. </param>
        /// <param name="productId"> The product id. </param>
        public ShoppingCartDetails(int deliveryLineAmount, 
            decimal deliveryLinePrice, long deliveryId, long productId)
        {
            DeliveryLineAmount = deliveryLineAmount;
            DeliveryLinePrice = deliveryLinePrice;
            DeliveryId = deliveryId;
            ProductId = productId;
        }

        public override bool Equals(object obj)
        {
            var details = obj as ShoppingCartDetails;
            return details != null &&
                   DeliveryLineAmount == details.DeliveryLineAmount &&
                   DeliveryLinePrice == details.DeliveryLinePrice &&
                   DeliveryId == details.DeliveryId &&
                   ProductId == details.ProductId;
        }

        public override int GetHashCode()
        {
            var hashCode = 531276092;
            hashCode = hashCode * -1521134295 + DeliveryLineAmount.GetHashCode();
            hashCode = hashCode * -1521134295 + DeliveryLinePrice.GetHashCode();
            hashCode = hashCode * -1521134295 + DeliveryId.GetHashCode();
            hashCode = hashCode * -1521134295 + ProductId.GetHashCode();
            return hashCode;
        }
    }
}
