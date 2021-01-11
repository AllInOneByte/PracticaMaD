using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Ninject;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductService
{
    public class ProductService : IProductService
    {
        [Inject]
        public IProductDao ProductDao { private get; set; }

        [Inject]
        public ICommentDao CommentDao { private get; set; }

        [Inject]
        public ITagDao TagDao { private get; set; }

        [Inject]
        public ICategoryDao CategoryDao { private get; set; }

        #region IProductService Members

        #region Product Members

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void UpdateProductDetails(long productId, string productName,
            decimal productPrice, int productQuantity)
        {
            Product product = ProductDao.Find(productId);

            product.productName = productName;
            product.productPrice = productPrice;
            product.productQuantity = productQuantity;

            ProductDao.Update(product);
        }

        public ProductBlock FindAllProducts(int startIndex = 0, int count = 20)
        {
            List<Product> products = ProductDao.FindAll(startIndex, count + 1);

            bool existMoreProducts = (products.Count == count + 1);

            if (existMoreProducts) products.RemoveAt(count);

            return new ProductBlock(products, existMoreProducts);
        }

        public ProductBlock FindAllProductsByTag(long tagId, int startIndex = 0, int count = 20)
        {
            List<Product> products = ProductDao.FindByTagId(tagId);

            bool existMoreProducts = (products.Count == count + 1);

            if (existMoreProducts) products.RemoveAt(count);

            return new ProductBlock(products, existMoreProducts);
        }

        public ProductBlock FindAllProductsByKeyword(string keyword, long categoryId,
            int startIndex = 0, int count = 20)
        {
            List<Product> products = ProductDao.FindByKeywords(keyword, categoryId,
                startIndex, count + 1);

            bool existMoreProducts = (products.Count == count + 1);

            if (existMoreProducts) products.RemoveAt(count);

            return new ProductBlock(products, existMoreProducts);
        }

        public ProductBlock FindAllProductsByKeyword(string keyword, int startIndex = 0, int count = 20)
        {
            List<Product> products = ProductDao.FindByKeywords(keyword, startIndex, count + 1);

            bool existMoreProducts = (products.Count == count + 1);

            if (existMoreProducts) products.RemoveAt(count);

            return new ProductBlock(products, existMoreProducts);
        }

        /// <exception cref="InstanceNotFoundException"/>
        public Product FindProduct(long productId)
        {
            return ProductDao.Find(productId);
        }

        #endregion Product Members

        #region Comment Members

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public Comment FindCommentById(long commentId)
        {
            return CommentDao.Find(commentId);
        }

        /// <exception cref="InstanceNotFoundException"/>
        public long AddComment(long productId, long userId, string commentBody)
        {
            Comment comment = new Comment
            {
                comment1 = commentBody,
                commentDate = System.DateTime.Now,
                userId = userId,
                productId = productId
            };

            CommentDao.Create(comment);

            return comment.commentId;
        }

        public long AddComment(long productId, long userId, string commentBody, List<long> tags)
        {
            Comment comment = new Comment
            {
                comment1 = commentBody,
                commentDate = System.DateTime.Now,
                userId = userId,
                productId = productId
            };

            List<Tag> tagList = new List<Tag>();

            foreach (var tagId in tags)
            {
                tagList.Add(TagDao.Find(tagId));
            }
            comment.Tags = tagList;

            CommentDao.Create(comment);

            return comment.commentId;
        }

        /// <exception cref="InstanceNotFoundException"/>
        public void DeleteComment(long commentId)
        {
            var comment = CommentDao.Find(commentId);
            CommentDao.Remove(comment.commentId);
        }

        /// <exception cref="InstanceNotFoundException"/>
        public void UpdateComment(long commentId, string commentBody)
        {
            Comment comment = CommentDao.Find(commentId);

            comment.comment1 = commentBody;

            CommentDao.Update(comment);
        }

        /// <exception cref="InstanceNotFoundException"/>
        public void UpdateComment(long commentId, string commentBody, List<long> newTags)
        {
            Comment comment = CommentDao.Find(commentId);

            ICollection<Tag> tags = new List<Tag>();

            comment.comment1 = commentBody;
            comment.Tags.Clear();
            foreach (var tagId in newTags)
            {
                tags.Add(TagDao.Find(tagId));
            }

            comment.Tags = tags;

            CommentDao.Update(comment);
        }
        
        public CommentBlock FindAllProductComments(long productId, int startIndex = 0, int count = 20)
        {
            List<Comment> comments = CommentDao.FindByProductIdOrderByDeliveryDate(productId, startIndex, count + 1);

            bool existMoreComments = (comments.Count == count + 1);

            if (existMoreComments) comments.RemoveAt(count);

            return new CommentBlock(comments, existMoreComments);
        }

        /// <exception cref="InstanceNotFoundException" />
        public Comment FindCommentByProductAndUser(long productId, long userId)
        {
            return CommentDao.FindByProductIdAndUserId(productId, userId);
        }

        #endregion Comment Members

        #region Tag Members

        /// <exception cref="DuplicateInstanceException"/>
        public long AddTag(string tagName)
        {
            Tag tag = new Tag();
            try
            {
                TagDao.FindByName(tagName);

                throw new DuplicateInstanceException(tagName,
                    typeof(Tag).FullName);
            }
            catch (InstanceNotFoundException)
            {
                tag.tagName = tagName;

                TagDao.Create(tag);
            }
            return tag.tagId;
        }

        /// <exception cref="InstanceNotFoundException"/>
        public Tag FindTagByName(string tagName)
        {
            return TagDao.FindByName(tagName);
        }

        public TagBlock FindAllTags(int startIndex = 0, int count = 20)
        {
            List<Tag> tags = TagDao.FindAll(startIndex, count + 1);

            bool existMoreTags = (tags.Count == count + 1);

            if (existMoreTags) tags.RemoveAt(count);

            return new TagBlock(tags, existMoreTags);
        }

        #endregion Tag Members

        #region Category Members

        public List<Category> FindAllCategories()
        {
            return CategoryDao.FindAll();
        }

        #endregion Category Members

        #endregion IProductService Members
    }
}
