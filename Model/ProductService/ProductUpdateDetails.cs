using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductService
{
    class ProductUpdateDetails
    {
        #region Properties Region

        public String ProductName { get; private set; }

        public decimal ProductPrice { get; private set; }

        public int ProductQuantity { get; private set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductUpdateDetails"/>
        /// class.
        /// </summary>
        /// <param name="productName">The products's name.</param>
        /// <param name="productPrice">The product's price.</param>
        /// <param name="remaningQuantity">The product's remaning quantity.</param>
        public ProductsLinkDetails(string productName, decimal productPrice, int remaningQuantity)
        {
            this.ProductName = productName;
            this.ProductPrice = productPrice;
            this.RemaningQuantity = remaningQuantity;
        }
    }
}
