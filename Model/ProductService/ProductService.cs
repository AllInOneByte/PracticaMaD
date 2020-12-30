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
    public class ProductService : IProductService
    {
        [Inject]
        public IProductDao ProductDao { private get; set; }

        [Inject]
        public ICommentDao CommentDao { private get; set; }

        [Inject]
        public ITagDao TagDao { private get; set; }

        #region IProductService Members

        #region Product Members

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="NegativeStockException"/>
        [Transactional]
        public void DecreaseProductStock(long productId, int quantity)
        {
            Product product = ProductDao.Find(productId);

            if (product.productQuantity < quantity)
                throw new NegativeStockException(productId, product.productQuantity, quantity);

            product.productQuantity = product.productQuantity - quantity;

            ProductDao.Update(product);
        }

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

        public ProductBlock FindAllProductsByKeyword(string keyword, long categoryId = -1, 
            int startIndex = 0, int count = 20)
        {
            List<Product> products = ProductDao.FindByKeywordsAndCategory(keyword, categoryId, 
                startIndex, count + 1);

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

            comment.comment1 = commentBody;
            foreach (var tagId in newTags)
            {
                comment.Tags.Add(TagDao.Find(tagId));
            }

            CommentDao.Update(comment);
        }

        /// <exception cref="InstanceNotFoundException"/>
        public void UpdateComment(long commentId, string commentBody, List<long> newTags, List<long> removeTags)
        {
            Comment comment = CommentDao.Find(commentId);

            comment.comment1 = commentBody;
            foreach (var tagId in newTags)
            {
                comment.Tags.Add(TagDao.Find(tagId));
            }
            foreach (var tagId in removeTags)
            {
                comment.Tags.Remove(TagDao.Find(tagId));
            }

            CommentDao.Update(comment);
        }

        public CommentBlock FindAllProductComments(long productId, int startIndex = 0, int count = 20)
        {
            List<Comment> comments = CommentDao.FindByProductIdOrderByDeliveryDate(productId, startIndex, count + 1);

            bool existMoreComments = (comments.Count == count + 1);

            if (existMoreComments) comments.RemoveAt(count);

            return new CommentBlock(comments, existMoreComments);
        }

        #endregion Comment Members

        #region Tag Members

        /// <exception cref="DuplicateInstanceException"/>
        public long addTag(string tagName)
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

        public TagBlock FindAllTags(int startIndex = 0, int count = 20)
        {
            List<Tag> tags = TagDao.FindAll(startIndex, count + 1);

            bool existMoreTags = (tags.Count == count + 1);

            if (existMoreTags) tags.RemoveAt(count);

            return new TagBlock(tags, existMoreTags);
        }

        #endregion Tag Members

        #endregion IProductService Members
    }
}
