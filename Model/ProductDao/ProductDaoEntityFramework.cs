using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Dao;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductDao
{
    /// <summary>
    /// Specific Operations for Product
    /// </summary>
    public class ProductDaoEntityFramework :
        GenericDaoEntityFramework<Product, long>, IProductDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public ProductDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        #region IUserProfileDao Members. Specific Operations
        /// <summary>
        /// Finds a Product by Key words and Category
        /// </summary>
        /// <param keyWords="keyWords">keyWords</param>
        /// <param categoryId="categoryId">categoryId</param>
        /// <returns>List of Product</returns>
        /// <exception cref="InstanceNotFoundException"/>
        public List<Product> FindByKeywords(string keyWords, int startIndex = 0, int count = 20) {
            List<Product> product = null;

            #region Option 1: Using Linq.

            DbSet<Product> products = Context.Set<Product>();

            
            var result =
                (from p in products
                    where p.productName.ToLower().Contains(keyWords.ToLower())
                    orderby p.productName ascending
                    select p).Skip(startIndex).Take(count);

            product = result.ToList();

            

            #endregion Option 1: Using Linq.

            if (product == null)
                throw new InstanceNotFoundException(keyWords,
                    typeof(Product).FullName);

            return product;

        }

        /// <summary>
        /// Finds a Product by Tag Id
        /// </summary>
        /// <param tagId="tagId">TagIds</param>
        /// <returns>List of Product</returns>
        /// <exception cref="InstanceNotFoundException"/>
        public List<Product> FindByTagId(long tagId, int startIndex = 0, int count = 20)
        {
            List<Product> product = null;

            #region Option 1: Using Linq.

            DbSet<Product> products = Context.Set<Product>();

            var result =
                (from p in products
                 where p.Comments.Any(c => c.Tags.Any(t => t.tagId == tagId))
                 orderby p.productName ascending
                 select p).Skip(startIndex).Take(count);

            product = result.ToList();


            #endregion Option 1: Using Linq.

            return product;

        }

        /// <summary>
        /// Finds a Product by Key words
        /// </summary>
        /// <param keyWords="keyWords">keyWords</param>
        /// <returns>List of Product</returns>
        /// <exception cref="InstanceNotFoundException"/>
        public List<Product> FindByKeywords(string keyWords, long categoryId, int startIndex = 0, int count = 20)
        {
            List<Product> product = null;

            #region Option 1: Using Linq.

            DbSet<Product> products = Context.Set<Product>();

            var result =
                (from p in products
                    where (p.productName.ToLower().Contains(keyWords.ToLower()) && (p.categoryId == categoryId))
                    orderby p.productName ascending
                    select p).Skip(startIndex).Take(count);

            product = result.ToList();


            #endregion Option 1: Using Linq.

            if (product == null)
                throw new InstanceNotFoundException(keyWords,
                    typeof(Product).FullName);

            return product;

        }

        /// <summary>
        /// Finds all Products
        /// <returns>List of Products</returns>
        public List<Product> FindAll(int startIndex, int count) {

            #region Option 1: Using Linq.

            DbSet<Product> products = Context.Set<Product>();

            var result =
                (from p in products
                 orderby p.productName ascending
                 select p).Skip(startIndex).Take(count);

            #endregion Option 1: Using Linq.

            return result.ToList();
        }

        #endregion  IUserProfileDao Members. Specific Operations
    }
}