using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductService
{
    interface IProductService
    {
        
        [Inject]
        IProductDao productDao { set; }

        [Inject]
        ICommentDao commentDao { set; }

        [Inject]
        ITagDao tagDao { set; }

        /// <summary>
        /// Updates the product details.
        /// </summary>
        /// <param name="userId"> The user id. </param>
        /// <param name="productId"> The product id. </param>
        /// <param name="productDetails"> The podruct details. </param>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        void UpdateProductDetails(long productId, ProductLinkDetails productDetails);

        /// <summary>
        /// Find all products
        /// </summary>
        /// <returns>A list of Products</returns>
        List<ProductDetails> FindAllProducts();

        /// <summary>
        /// Find all creditCards that meet the search conditions
        /// </summary>
        /// <param name="keyword">The keyword</param>
        /// <optional param name="categoryId">The category ID</param>
        /// <returns>A list of Products</returns>
        List<ProductDetails> FindAllProductsByKeyword(string keyword, long categoryId);

        /// <summary>
        /// Find a product
        /// </summary>
        /// <param productId="productId">productId</param>
        /// <returns>The product to look for</returns>
        /// <exception cref="InstanceNotFoundException"/>
        ProductLinkDetails FindProduct(long productId);

        /// <summary>
        /// Checks if the specified ID corresponds to a valid user.
        /// </summary>
        /// <param name="userId"> User ID. </param>
        /// <returns> Boolean to indicate if the ID exists </returns>
        bool UserExists(long userId);
    }
}
