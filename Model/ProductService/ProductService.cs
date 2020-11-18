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
    public class ProductService : IProductService
    {
        [Inject]
        IProductDao productDao { private get; set; }

        [Inject]
        ICommentDao comment { private get; set; }

        [Inject]
        ITagDao tagDao { private get; set; }

        #region IProductService Members

        #region Product Members

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        void UpdateProductDetails(long productId, ProductUpdateDetails productDetails)
        {
            Product product = productDao.Find(productId);

            product.productName = productDetails.ProductName;
            product.productPrice = productDetails.ProductPrice;
            product.productQuantity = productDetails.ProductQuantity;

            productDao.Update(product);
        }

        List<ProductsDetails> FindAllProducts()
        {
            List<ProductsDetails> productsDetails = new List<ProductDetails>();
            List<Product> products = productDao.FindAll();

            foreach (var p in products)
            {
                productsDetails.Add(new ProductDetails(p.productId, p.productName, p.Category.categoryName, p.productDate, p.productPrice));
            }

            return productsDetails;
        }

        List<ProductsDetails> FindAllProductsByKeyword(string keyword, long categoryId)
        {
            List<ProductsDetails> productsDetails = new List<ProductDetails>();
            List<Product> products = productDao.FindByKeywordsAndCategory(keyword, categoryId);

            foreach (var p in products)
            {
                productsDetails.Add(new ProductDetails(p.productId, p.productName, p.Category.categoryName, p.productDate, p.productPrice));
            }

            return productsDetails;
        }

        /// <exception cref="InstanceNotFoundException"/>
        ProductLinkDetails FindProduct(long productId)
        {
            Product product = productDao.Find(productId);

            return new ProductLinkDetails(p.productId, p.productName, p.Category.categoryName, p.productDate,
                p.productPrice, p.productQuantity, p.SpecificProperties.propertyName, p.SpecificProperties.propertyValue);
        }

        #endregion Product Members

        #region Comment Members

        #endregion Comment Members

        #region Tag Members

        #endregion Tag Members

        #endregion IProductService Members
    }
}
