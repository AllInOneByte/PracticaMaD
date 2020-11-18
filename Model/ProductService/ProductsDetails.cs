using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductService
{
    class ProductsDetails
    {
        
        #region Properties Region

        public long ProductId { get; private set; }

        public String ProductName { get; private set; }

        public String CategoryName { get; private set; }

        public System.DateTime ProductDate { get; private set; }

        public decimal ProductPrice { get; private set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsDetails"/>
        /// class.
        /// </summary>
        /// <param name="productId">The product's ID.</param>
        /// <param name="productName">The products's name.</param>
        /// <param name="categoryName">The product's category name.</param>
        /// <param name="productDate">The product's date.</param>
        /// <param name="productPrice">The product's price.</param>
        public ProductsDetails(long productId, string productName, 
                                    string categoryName, System.Data productDate, decimal productPrice)
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.CategoryName = categoryName;
            this.ProductDate = productDate;
            this.ProductPrice = productPrice;
        }

    }
}
