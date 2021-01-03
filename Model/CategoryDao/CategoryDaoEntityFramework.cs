using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.CategoryDao
{
    /// <summary>
    /// Specific Operations for Category
    /// </summary>
    public class CategoryDaoEntityFramework :
        GenericDaoEntityFramework<Category, long>, ICategoryDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public CategoryDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        #region ICategoryDao Members. Specific Operations

        /// <summary>
        /// Find all the categories
        /// </summary>
        /// <returns> A list with all the categories. </returns>
        public List<Category> FindAll()
        {
            List<Category> tags = new List<Category>();

            #region Option 1: Using Linq.

            DbSet<Category> dbCategories = Context.Set<Category>();

            var result =
                    (from c in dbCategories
                     orderby c.categoryName ascending
                     select c);

            tags = result.ToList<Category>();

            #endregion Option 1: Using Linq.

            return tags;
        }

        #endregion ICategoryDao Members. Specific Operations
    }
}
