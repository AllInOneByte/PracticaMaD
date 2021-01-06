using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CategoryDao
{
    public interface ICategoryDao : IGenericDao<Category, long>
    {
        /// <summary>
        /// Find all Categories
        /// <returns>List of Categories</returns>
        List<Category> FindAll();
    }
}
