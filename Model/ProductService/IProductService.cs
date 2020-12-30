using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService.Exceptions;
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
        /// Decrease the product's quantity.
        /// </summary>
        /// <param name="productId"> The product's id. </param>
        /// <param name="quantity"> The product's quantity to be extracted from stock. </param>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="NegativeStockException"/>
        [Transactional]
        void DecreaseProductStock(long productId, int quantity);

        /// <summary>
        /// Update the product details.
        /// </summary>
        /// <param name="productId"> The product's id. </param>
        /// <param name="productName"> The product's name. </param>
        /// <param name="productPrice"> The product's price. </param>
        /// <param name="productQuantity"> The product's quantity. </param>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        void UpdateProductDetails(long productId, string productName, decimal productPrice, int productQuantity);

        /// <summary>
        /// Find all products.
        /// </summary>
        /// <param name="startIndex"> The index at which the products list must start. </param>
        /// <param name="count"> The maximum number of products that must return the function. </param>
        /// <returns> A list of products. </returns>
        ProductBlock FindAllProducts(int startIndex, int count);

        /// <summary>
        /// Find all creditCards that meet the search conditions.
        /// </summary>
        /// <param name="keyword"> The keyword that must be contained within the name of the product </param>
        /// <param name="categoryId"> The category ID. </param>
        /// <param name="startIndex"> The index at which the products list must start </param>
        /// <param name="count"> The maximum number of products that must return the function. </param>
        /// <returns> A list of products. </returns>
        ProductBlock FindAllProductsByKeyword(string keyword, long categoryId = -1, 
            int startIndex = 0, int count = 20);

        /// <summary>
        /// Find a product.
        /// </summary>
        /// <param productId="productId"> The ID of the product to be found. </param>
        /// <returns> The found product. </returns>
        /// <exception cref="InstanceNotFoundException"/>
        Product FindProduct(long productId);

        /// <summary>
        /// Add a new comment.
        /// </summary>
        /// <param name="productId"> The product's id. </param>
        /// <param name="userId"> The user's id. </param>
        /// <param name="commentBody"> The comment's body. </param>
        long AddComment(long productId, long userId, string commentBody);

        /// <summary>
        /// Add a new comment.
        /// </summary>
        /// <param name="productId"> The product's id. </param>
        /// <param name="userId"> The user's id. </param>
        /// <param name="commentBody"> The comment's body. </param>
        /// <param name="tags"> The comment's tags. </param>
        long AddComment(long productId, long userId, string commentBody, List<long> tags);

        /// <summary>
        /// Delete the comment.
        /// </summary>
        /// <param name="commentId"> The comment's id. </param>
        /// <exception cref="InstanceNotFoundException"/>
        void DeleteComment(long commentId);

        /// <summary>
        /// Update the comment.
        /// </summary>
        /// <param name="commentId"> The comment's id. </param>
        /// <param name="commentBody"> The comment's body. </param>
        /// <exception cref="InstanceNotFoundException"/>
        void UpdateComment(long commentId, string commentBody);

        /// <summary>
        /// Update the comment.
        /// </summary>
        /// <param name="commentId"> The comment's id. </param>
        /// <param name="commentBody"> The comment's body. </param>
        /// <param name="newTags"> The comment's new tags. </param>
        /// <exception cref="InstanceNotFoundException"/>
        void UpdateComment(long commentId, string commentBody, List<long> newTags);

        /// <summary>
        /// Update the comment.
        /// </summary>
        /// <param name="commentId"> The comment's id. </param>
        /// <param name="commentBody"> The comment's body. </param>
        /// <param name="newTags"> The comment's new tags. </param>
        /// <param name="removeTags"> The remove tags of the commennt. </param>
        /// <exception cref="InstanceNotFoundException"/>
        void UpdateComment(long commentId, string commentBody, List<long> newTags, List<long> removeTags);

        /// <summary>
        /// Find all comments of a product.
        /// </summary>
        /// <param name="productId"> The product's ID </param>
        /// <param name="startIndex"> The index at which the comments list must start </param>
        /// <param name="count"> The maximum number of comments that must return the function. </param>
        /// <returns> A list of comments. </returns>
        CommentBlock FindAllProductComments(long productId, int startIndex = 0, int count = 20);

        /// <summary>
        /// Add a new tag.
        /// </summary>
        /// <param name="tagName"> The tag's name. </param>
        /// <exception cref="DuplicateInstanceException"/>
        long addTag(string tagName);

        /// <summary>
        /// Find all tags.
        /// </summary>
        /// <param name="startIndex"> The index at which the tags list must start </param>
        /// <param name="count"> The maximum number of tags that must return the function. </param>
        /// <returns> A list of tags. </returns>
        TagBlock FindAllTags(int startIndex = 0, int count = 20);
    }
}
