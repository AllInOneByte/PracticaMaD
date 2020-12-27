using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductDao
{
    public interface IProductDao : IGenericDao<Product, long>
    {
        /// <summary>
        /// Finds a Product by Key words or Category
        /// </summary>
        /// <param keyWords="keyWords">keyWords</param>
        /// <param categoryId="categoryId">categoryId</param>
        /// <returns>List of Product</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<Product> FindByKeywordsAndCategory(string keyWords, long categoryId, int startIndex, int count);

        /// <summary>
        /// Finds all Products
        /// <returns>List of Products</returns>
        List<Product> FindAll(int startIndex, int count);
    }
}