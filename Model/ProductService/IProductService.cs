using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
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

        [Inject]
        ICategoryDao CategoryDao { set; }


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
        ProductBlock FindAllProducts(int startIndex = 0, int count = 20);

        /// <summary>
        /// Find all products that contains the tag id in the comments.
        /// </summary>
        /// <param name="TagId"> The tag ID. </param>
        /// <param name="startIndex"> The index at which the products list must start </param>
        /// <param name="count"> The maximum number of products that must return the function. </param>
        /// <returns> A list of products. </returns>
        ProductBlock FindAllProductsByTag(long tagId, int startIndex = 0, int count = 20);

        /// <summary>
        /// Find all creditCards that meet the search conditions.
        /// </summary>
        /// <param name="keyword"> The keyword that must be contained within the name of the product </param>
        /// <param name="categoryId"> The category ID. </param>
        /// <param name="startIndex"> The index at which the products list must start </param>
        /// <param name="count"> The maximum number of products that must return the function. </param>
        /// <returns> A list of products. </returns>
        ProductBlock FindAllProductsByKeyword(string keyword, long categoryId,
            int startIndex = 0, int count = 20);

        /// <summary>
        /// Find all creditCards that meet the search conditions.
        /// </summary>
        /// <param name="keyword"> The keyword that must be contained within the name of the product </param>
        /// <param name="startIndex"> The index at which the products list must start </param>
        /// <param name="count"> The maximum number of products that must return the function. </param>
        /// <returns> A list of products. </returns>
        ProductBlock FindAllProductsByKeyword(string keyword, int startIndex = 0, int count = 20);


        /// <summary>
        /// Find a product.
        /// </summary>
        /// <param productId="productId"> The ID of the product to be found. </param>
        /// <returns> The found product. </returns>
        /// <exception cref="InstanceNotFoundException"/>
        Product FindProduct(long productId);

        /// <summary>
        /// Find a comment.
        /// </summary>
        /// <param commentId="commentId"> The ID of the comment to be found. </param>
        /// <returns> The found comment. </returns>
        /// <exception cref="InstanceNotFoundException"/>
        Comment FindCommentById(long commentId);

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
        /// Find all comments of a product.
        /// </summary>
        /// <param name="productId"> The product's ID </param>
        /// <param name="startIndex"> The index at which the comments list must start </param>
        /// <param name="count"> The maximum number of comments that must return the function. </param>
        /// <returns> A list of comments. </returns>
        CommentBlock FindAllProductComments(long productId, int startIndex = 0, int count = 20);

        /// <summary>
        /// Find a comment of a product made by a specific user.
        /// </summary>
        /// <param name="productId"> The product's ID </param>
        /// <param name="userId"> The user ID. </param>
        /// <returns> A comment made by the indicated user for the indicated product. </returns>
        /// <exception cref="InstanceNotFoundException" />
        Comment FindCommentByProductAndUser(long productId, long userId);

        /// <summary>
        /// Add a new tag.
        /// </summary>
        /// <param name="tagName"> The tag's name. </param>
        /// <exception cref="DuplicateInstanceException"/>
        long AddTag(string tagName);

        /// <summary>
        /// Find a tag by its name.
        /// </summary>
        /// <param name="tagName"> The tag's name. </param>
        /// <exception cref="InstanceNotFoundException"/>
        Tag FindTagByName(string tagName);

        /// <summary>
        /// Find all tags.
        /// </summary>
        /// <param name="startIndex"> The index at which the tags list must start </param>
        /// <param name="count"> The maximum number of tags that must return the function. </param>
        /// <returns> A list of tags. </returns>
        TagBlock FindAllTags(int startIndex = 0, int count = 20);

        /// <summary>
        /// Find all categories.
        /// </summary>
        /// <returns> A list of categories. </returns>
        List<Category> FindAllCategories();

    }
}
