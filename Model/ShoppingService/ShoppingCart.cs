using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService
{
    /// <summary>
    /// VO Class which contains the shopping cart details
    /// </summary>
    [Serializable()]
    public class ShoppingCart
    {
        #region Properties Region

        public int Amount { get; set; }

        public Product Product { get; set; }

        public bool Gift { get; set; }
        
        #endregion Properties Region

        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingCart"/> class
        /// </summary>
        /// <param name="deliveryLineAmount"> The amount of items of a product. </param>
        /// <param name="deliveryLinePrice"> The price of the product. </param>
        /// <param name="productId"> The product id. </param>
        public ShoppingCart(int amount, Product product, bool gift)
        {
            Amount = amount;
            Product = product;
            Gift = gift;
        }

        public override bool Equals(object obj)
        {
            var cart = obj as ShoppingCart;
            return cart != null &&
                   Amount == cart.Amount &&
                   EqualityComparer<Product>.Default.Equals(Product, cart.Product) &&
                   Gift == cart.Gift;
        }

        public override int GetHashCode()
        {
            var hashCode = 1059833502;
            hashCode = hashCode * -1521134295 + Amount.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Product>.Default.GetHashCode(Product);
            hashCode = hashCode * -1521134295 + Gift.GetHashCode();
            return hashCode;
        }
    }
}
