using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductService
{
    public class ProductDetails
    {
        
        #region Properties Region

        public long ProductId { get; private set; }

        public String ProductName { get; private set; }

        public String CategoryName { get; private set; }

        public System.DateTime ProductDate { get; private set; }

        public decimal ProductPrice { get; private set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDetails"/>
        /// class.
        /// </summary>
        /// <param name="productId">The product's ID.</param>
        /// <param name="productName">The products's name.</param>
        /// <param name="categoryName">The product's category name.</param>
        /// <param name="productDate">The product's date.</param>
        /// <param name="productPrice">The product's price.</param>
        public ProductDetails(long productId, string productName, 
                                    string categoryName, DateTime productDate, decimal productPrice)
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.CategoryName = categoryName;
            this.ProductDate = productDate;
            this.ProductPrice = productPrice;
        }

        public override bool Equals(object obj)
        {
            var details = obj as ProductDetails;
            return details != null &&
                   ProductId == details.ProductId &&
                   ProductName == details.ProductName &&
                   CategoryName == details.CategoryName &&
                   ProductDate == details.ProductDate &&
                   ProductPrice == details.ProductPrice;
        }

        public override int GetHashCode()
        {
            var hashCode = -885774565;
            hashCode = hashCode * -1521134295 + ProductId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ProductName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CategoryName);
            hashCode = hashCode * -1521134295 + ProductDate.GetHashCode();
            hashCode = hashCode * -1521134295 + ProductPrice.GetHashCode();
            return hashCode;
        }

    }
}
