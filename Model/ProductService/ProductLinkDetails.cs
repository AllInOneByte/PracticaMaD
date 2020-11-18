using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductService
{
    public class ProductLinkDetails
    {
        #region Properties Region

        public long ProductId { get; private set; }

        public String ProductName { get; private set; }

        public String CategoryName { get; private set; }

        public System.DateTime ProductDate { get; private set; }

        public decimal ProductPrice { get; private set; }

        public int RemainingQuantity { get; private set; }

        public List<string> SpecificName { get; private set; }

        public List<string> SpecificValue { get; private set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductLinkDetails"/>
        /// class.
        /// </summary>
        /// <param name="productId">The product's ID.</param>
        /// <param name="productName">The products's name.</param>
        /// <param name="categoryName">The product's category name.</param>
        /// <param name="productDate">The product's date.</param>
        /// <param name="productPrice">The product's price.</param>
        /// <param name="remaningQuantity">The product's remaning quantity.</param>
        /// <param name="specificName">The product's specific propertry name.</param>
        /// <param name="specificValue">The product's specific propertry value.</param>
        public ProductLinkDetails(long productId, string productName, string categoryName, 
                    DateTime productDate, decimal productPrice, int remainingQuantity, List<string> specificName, List<string> specificValue)
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.CategoryName = categoryName;
            this.ProductDate = productDate;
            this.ProductPrice = productPrice;
            this.RemainingQuantity = remainingQuantity;
            this.SpecificName = specificName;
            this.SpecificValue = specificValue;
        }

        public override bool Equals(object obj)
        {
            var details = obj as ProductLinkDetails;
            return details != null &&
                   ProductId == details.ProductId &&
                   ProductName == details.ProductName &&
                   CategoryName == details.CategoryName &&
                   ProductDate == details.ProductDate &&
                   ProductPrice == details.ProductPrice &&
                   RemainingQuantity == details.RemainingQuantity &&
                   EqualityComparer<List<string>>.Default.Equals(SpecificName, details.SpecificName) &&
                   EqualityComparer<List<string>>.Default.Equals(SpecificValue, details.SpecificValue);
        }

        public override int GetHashCode()
        {
            var hashCode = 728645204;
            hashCode = hashCode * -1521134295 + ProductId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ProductName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CategoryName);
            hashCode = hashCode * -1521134295 + ProductDate.GetHashCode();
            hashCode = hashCode * -1521134295 + ProductPrice.GetHashCode();
            hashCode = hashCode * -1521134295 + RemainingQuantity.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<string>>.Default.GetHashCode(SpecificName);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<string>>.Default.GetHashCode(SpecificValue);
            return hashCode;
        }
    }
}
