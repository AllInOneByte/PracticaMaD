﻿using Es.Udc.DotNet.ModelUtil.Dao;

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
    }
}
