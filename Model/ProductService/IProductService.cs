using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Ninject;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductService
{
    public interface IProductService
    {
        
        [Inject]
        IProductDao ProductDao { set; }

        [Inject]
        ICommentDao CommentDao { set; }

        [Inject]
        ITagDao TagDao { set; }

        /// <summary>
        /// Updates the product details.
        /// </summary>
        /// <param name="userId"> The user id. </param>
        /// <param name="productId"> The product id. </param>
        /// <param name="productDetails"> The podruct details. </param>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        void UpdateProductDetails(long productId, ProductUpdateDetails productDetails);

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
        /// Add a new comment.
        /// </summary>
        /// <param name="productId"> The product's id. </param>
        /// <param name="userId"> The user's id. </param>
        /// <param name="commentBody"> The comment's body. </param>
        /// <param name="productDetails"> The podruct details. </param>
        void AddComment(long productId, long userId, string commentBody);

        /// <summary>
        /// Delete the comment.
        /// </summary>
        /// <param name="commentId"> The comment's id. </param>
        /// <exception cref="InstanceNotFoundException"/>
        void DeleteComment(long commentId);

        /// <summary>
        /// Updates the comment.
        /// </summary>
        /// <param name="commentId"> The comment's id. </param>
        /// <param name="commentBody"> The comment's body. </param>
        /// <exception cref="InstanceNotFoundException"/>
        void UpdateComment(long commentId, string commentBody);

        /// <summary>
        /// Find all comments of a product
        /// </summary>
        /// <param name="productId">The product's ID</param>
        /// <returns>A list of Comments</returns>
        List<CommentDetails> FindAllProductComments(long productId);

        /// <summary>
        /// Add a new tag.
        /// </summary>
        /// <param name="tagName"> The tag's name. </param>
        /// <exception cref="DuplicateInstanceException"/>
        void addTag(string tagName);

        /// <summary>
        /// Find all tags
        /// </summary>
        /// <returns>A list of Tags</returns>
        List<Tag> FindAllTags();
    }
}
