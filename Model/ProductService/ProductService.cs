using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.LabeledDao;
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
        public ILabeledDao LabeledDao { private get; set; }

        [Inject]
        public ICommentDao CommentDao { private get; set; }

        [Inject]
        public ITagDao TagDao { private get; set; }

        #region IProductService Members

        #region Product Members

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void UpdateProductDetails(long productId, ProductUpdateDetails productDetails)
        {
            Product product = ProductDao.Find(productId);

            product.productName = productDetails.ProductName;
            product.productPrice = productDetails.ProductPrice;
            product.productQuantity = productDetails.ProductQuantity;

            ProductDao.Update(product);
        }

        public List<ProductDetails> FindAllProducts(int startIndex = 0, int count = 20)
        {
            List<ProductDetails> productsDetails = new List<ProductDetails>();
            List<Product> products = ProductDao.FindAll(startIndex, count);

            foreach (var p in products)
            {
                productsDetails.Add(new ProductDetails(p.productId, p.productName, p.Category.categoryName, p.productDate, p.productPrice));
            }

            return productsDetails;
        }

        public List<ProductDetails> FindAllProductsByKeyword(string keyword, long categoryId, int startIndex = 0, int count = 20)
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
        public long AddComment(long productId, long userId, CommentUpdate details)
        {
            Comment comment = new Comment
            {
                comment1 = details.CommentBody,
                commentDate = System.DateTime.Now,
                userId = userId,
                productId = productId
            };

            CommentDao.Create(comment);

            foreach (var tagId in details.TagIds)
            {
                Labeled labeled = new Labeled
                {
                    tagId = tagId,
                    commentId = comment.commentId
                };

                LabeledDao.Create(labeled);
            }

            return comment.commentId;
        }

        /// <exception cref="InstanceNotFoundException"/>
        public void DeleteComment(long commentId)
        {
            var comment = CommentDao.Find(commentId);
            foreach (var l in comment.Labeleds.ToList())
            {
                LabeledDao.Remove(l.labeledId);
            }
            CommentDao.Remove(comment.commentId);
        }

        /// <exception cref="InstanceNotFoundException"/>
        public void UpdateComment(long commentId, CommentUpdate details)
        {
            Comment c = CommentDao.Find(commentId);

            c.comment1 = details.CommentBody;

            CommentDao.Update(c);

            foreach (var tagId in details.TagIds)
            {
                Labeled labeled = new Labeled
                {
                    tagId = tagId,
                    commentId = commentId
                };

                LabeledDao.Create(labeled);
            }

        }

        public List<CommentDetails> FindAllProductComments(long productId, int startIndex = 0, int count = 20)
        {
            List<CommentDetails> details = new List<CommentDetails>();
            List<Comment> comments = CommentDao.FindByProductIdOrderByDeliveryDate(productId, startIndex, count);

            foreach (var c in comments)
            {
                List<string> tagNames = new List<string>();
                foreach (var l in c.Labeleds.ToList())
                {
                    tagNames.Add(l.Tag.tagName);
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

        public List<Tag> FindAllTags(int startIndex = 0, int count = 20)
            {
                return TagDao.FindAll(startIndex, count);
            }

            #endregion Tag Members

            #endregion IProductService Members
        }
}
