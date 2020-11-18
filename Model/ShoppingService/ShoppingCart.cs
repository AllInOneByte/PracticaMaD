using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService
{
    /// <summary>
    /// VO Class which contains the shopping cart data
    /// </summary>
    [Serializable()]
    public class ShoppingCart
    {
        #region Properties Region

        public long DeliveryId { get; set; }

        public DateTime DeliveryDate { get; private set; }

        public decimal DeliveryPrice { get; private set; }

        public string DeliveryAddress { get; private set; }

        public string Description { get; private set; }

        public long UserId { get; private set; }

        public long CardId { get; private set; }

        #endregion Properties Region

        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingCart"/> class
        /// </summary>
        /// <param name="deliveryDate"> The date the delivery was created. </param>
        /// <param name="deliveryPrice"> The total price of the delivery. </param>
        /// <param name="deliveryAddress"> The address for the delivery. </param>
        /// <param name="description"> The descriptive short name. </param>
        /// <param name="userId"> The user profile id. </param>
        /// <param name="cardId"> The credit card id. </param>
        public ShoppingCart(DateTime deliveryDate, decimal deliveryPrice, 
            string deliveryAddress, string description, long userId, long cardId)
        {
            DeliveryDate = deliveryDate;
            DeliveryPrice = deliveryPrice;
            DeliveryAddress = deliveryAddress;
            Description = description;
            UserId = userId;
            CardId = cardId;
        }

        public override bool Equals(object obj)
        {
            var cart = obj as ShoppingCart;
            return cart != null &&
                   DeliveryDate == cart.DeliveryDate &&
                   DeliveryPrice == cart.DeliveryPrice &&
                   DeliveryAddress == cart.DeliveryAddress &&
                   Description == cart.Description &&
                   UserId == cart.UserId &&
                   CardId == cart.CardId;
        }

        public override int GetHashCode()
        {
            var hashCode = 1406498744;
            hashCode = hashCode * -1521134295 + DeliveryDate.GetHashCode();
            hashCode = hashCode * -1521134295 + DeliveryPrice.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DeliveryAddress);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + UserId.GetHashCode();
            hashCode = hashCode * -1521134295 + CardId.GetHashCode();
            return hashCode;
        }
    }
}