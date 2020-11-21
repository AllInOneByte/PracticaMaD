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
        /// Finds a Product by Key words or Category
        /// </summary>
        /// <param keyWords="keyWords">keyWords</param>
        /// <param categoryId="categoryId">categoryId</param>
        /// <returns>List of Product</returns>
        /// <exception cref="InstanceNotFoundException"/>
        public List<Product> FindByKeywordsAndCategory(string keyWords, long categoryId) {
            List<Product> product = null;

            #region Option 1: Using Linq.

            DbSet<Product> products = Context.Set<Product>();
            if (categoryId <= 0)
            {
                var result =
                    (from p in products
                     where p.productName.ToLower().Contains(keyWords.ToLower())
                     select p);

                product = result.ToList();

            }
            else {
                var result =
                    (from p in products
                     where (p.productName.ToLower().Contains(keyWords.ToLower()) && (p.categoryId == categoryId))
                     select p);

                product = result.ToList();

            }
            #endregion Option 1: Using Linq.

            if (product == null)
                throw new InstanceNotFoundException(keyWords,
                    typeof(Product).FullName);

            return product;

        }

        /// <summary>
        /// Finds all Products
        /// <returns>List of Products</returns>
        public List<Product> FindAll() {

            #region Option 1: Using Linq.

            DbSet<Product> products = Context.Set<Product>();

            var result =
                (from p in products
                 select p);

            #endregion Option 1: Using Linq.

            return result.ToList();
        }

        #endregion  IUserProfileDao Members. Specific Operations
    }
}