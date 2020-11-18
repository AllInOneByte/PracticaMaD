using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductService
{
    class ProductLinkDeatils
    {
        #region Properties Region

        public long ProductId { get; private set; }

        public String ProductName { get; private set; }

        public String CategoryName { get; private set; }

        public System.DateTime ProductDate { get; private set; }

        public decimal ProductPrice { get; private set; }

        public int RemaningQuantity { get; private set; }

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
        public ProductsLinkDetails(long productId, string productName, string categoryName, 
                    System.Data productDate, decimal productPrice, int remaningQuantity, List<string> specificName, List<string> specificValue)
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.CategoryName = categoryName;
            this.ProductDate = productDate;
            this.ProductPrice = productPrice;
            this.RemaningQuantity = remaningQuantity;
            this.SpecificName = specificName;
            this.SpecificValue = specificValue;
        }
    }
}
