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
        public void UpdateProductDetails(long productId, ProductUpdateDetails productDetails)
        {
            Product product = ProductDao.Find(productId);

            product.productName = productDetails.ProductName;
            product.productPrice = productDetails.ProductPrice;
            product.productQuantity = productDetails.ProductQuantity;

            ProductDao.Update(product);
        }

        public List<ProductDetails> FindAllProducts()
        {
            List<ProductDetails> productsDetails = new List<ProductDetails>();
            List<Product> products = ProductDao.FindAll();

            foreach (var p in products)
            {
                productsDetails.Add(new ProductDetails(p.productId, p.productName, p.Category.categoryName, p.productDate, p.productPrice));
            }

            return productsDetails;
        }

        public List<ProductDetails> FindAllProductsByKeyword(string keyword, long categoryId)
        {
            List<ProductDetails> productsDetails = new List<ProductDetails>();
            List<Product> products = ProductDao.FindByKeywordsAndCategory(keyword, categoryId);

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
        public void AddComment(long productId, long userId, string commentBody)
        {
            Comment comment = new Comment();

            comment.comment1 = commentBody;
            comment.commentDate = System.DateTime.Now;
            comment.userId = userId;
            comment.productId = productId;

            CommentDao.Create(comment);
        }

        /// <exception cref="InstanceNotFoundException"/>
        public void DeleteComment(long commentId)
        {
            CommentDao.Remove(commentId);
        }

        /// <exception cref="InstanceNotFoundException"/>
        public void UpdateComment(long commentId, string commentBody)
        {
            Comment c = CommentDao.Find(commentId);

            c.comment1 = commentBody;

            CommentDao.Update(c);
        }

        public List<CommentDetails> FindAllProductComments(long productId)
        {
            List<CommentDetails> details = new List<CommentDetails>();
            List<Comment> comments = CommentDao.FindByProductIdOrderByDeliveryDate(productId);

            foreach (var c in comments)
            {
                details.Add(new CommentDetails(c.commentId, c.comment1, c.commentDate, c.userId));
            }

            return details;
        }

        #endregion Comment Members

        #region Tag Members

        /// <exception cref="DuplicateInstanceException"/>
        public void addTag(string tagName)
        {
            try
            {
                TagDao.FindByName(tagName);

                throw new DuplicateInstanceException(tagName,
                    typeof(Tag).FullName);
            }
            catch (InstanceNotFoundException)
            {
                Tag tag = new Tag();

                tag.tagName = tagName;

                TagDao.Create(tag);
            }
        }

        public List<Tag> FindAllTags()
            {
                return TagDao.FindAll();
            }

            #endregion Tag Members

            #endregion IProductService Members
        }
}
