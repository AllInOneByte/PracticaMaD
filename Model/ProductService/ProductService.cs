﻿using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using System;
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

        #endregion Comment Members

        #region Tag Members

        #endregion Tag Members

        #endregion IProductService Members
    }
}
