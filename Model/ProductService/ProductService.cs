using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Ninject;
using System.Collections.Generic;
using System.Linq;

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
        [Transactional]
        public void UpdateProductDetails(long productId, string productName, decimal productPrice, int productQuantity)
        {
            Product product = ProductDao.Find(productId);

            product.productName = productName;
            product.productPrice = productPrice;
            product.productQuantity = productQuantity;

            ProductDao.Update(product);
        }

        public List<ProductDetails> FindAllProducts(int startIndex, int count)
        {
            List<ProductDetails> productsDetails = new List<ProductDetails>();
            List<Product> products = ProductDao.FindAll(startIndex, count);

            foreach (var p in products)
            {
                productsDetails.Add(new ProductDetails(p.productId, p.productName, p.Category.categoryName, p.productDate, p.productPrice));
            }

            return productsDetails;
        }

        public List<ProductDetails> FindAllProductsByKeyword(string keyword, long categoryId, int startIndex, int count)
        {
            List<ProductDetails> productsDetails = new List<ProductDetails>();
            List<Product> products = ProductDao.FindByKeywordsAndCategory(keyword, categoryId, startIndex, count);

            foreach (var p in products)
            {
                productsDetails.Add(new ProductDetails(p.productId, p.productName, p.Category.categoryName, p.productDate, p.productPrice));
            }

            return productsDetails;
        }

        /// <exception cref="InstanceNotFoundException"/>
        public ProductLinkDetails FindProduct(long productId)
        {
            Product p = ProductDao.Find(productId);
            List<string> specificName = new List<string>();
            List<string> specificValue = new List<string>();

            foreach (var s in p.SpecificProperties.ToList())
            {
                specificName.Add(s.propertyName);
                specificValue.Add(s.propertyValue);
            }

            return new ProductLinkDetails(p.productId, p.productName, p.Category.categoryName, p.productDate,
                p.productPrice, p.productQuantity, specificName, specificValue);
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

        public List<CommentDetails> FindAllProductComments(long productId, int startIndex, int count)
        {
            List<CommentDetails> details = new List<CommentDetails>();
            List<Comment> comments = CommentDao.FindByProductIdOrderByDeliveryDate(productId, startIndex, count);

            foreach (var c in comments)
            {
                List<string> tagNames = new List<string>();
                foreach (var tag in c.Tags.ToList())
                {
                    tagNames.Add(tag.tagName);
                }
                details.Add(new CommentDetails(c.commentId, c.comment1, c.commentDate, c.userId, tagNames));
            }

            return details;
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

        public List<Tag> FindAllTags(int startIndex, int count)
            {
                return TagDao.FindAll(startIndex, count);
            }

            #endregion Tag Members

            #endregion IProductService Members
        }
}
