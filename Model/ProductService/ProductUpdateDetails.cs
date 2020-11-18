using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductService
{
    public class ProductUpdateDetails
    {
        #region Properties Region

        public string ProductName { get; private set; }

        public decimal ProductPrice { get; private set; }

        public int ProductQuantity { get; private set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductUpdateDetails"/>
        /// class.
        /// </summary>
        /// <param name="productName">The products's name.</param>
        /// <param name="productPrice">The product's price.</param>
        /// <param name="productQuantity">The product's remaning quantity.</param>
        public ProductUpdateDetails(string productName, decimal productPrice, int productQuantity)
        {
            this.ProductName = productName;
            this.ProductPrice = productPrice;
            this.ProductQuantity = productQuantity;
        }

        public override bool Equals(object obj)
        {
            var details = obj as ProductUpdateDetails;
            return details != null &&
                   ProductName == details.ProductName &&
                   ProductPrice == details.ProductPrice &&
                   ProductQuantity == details.ProductQuantity;
        }

        public override int GetHashCode()
        {
            var hashCode = 64093865;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ProductName);
            hashCode = hashCode * -1521134295 + ProductPrice.GetHashCode();
            hashCode = hashCode * -1521134295 + ProductQuantity.GetHashCode();
            return hashCode;
        }
    }
}
