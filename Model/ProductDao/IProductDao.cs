using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductDao
{
    public interface IProductDao : IGenericDao<Product, long>
    {
        /// <summary>
        /// Finds a Product by Key words and Category
        /// </summary>
        /// <param keyWords="keyWords">keyWords</param>
        /// <param categoryId="categoryId">categoryId</param>
        /// <returns>List of Product</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<Product> FindByKeywords(string keyWords, long categoryId, int startIndex = 0, int count = 20);

        /// <summary>
        /// Finds a Product by Key words
        /// </summary>
        /// <param keyWords="keyWords">keyWords</param>
        /// <returns>List of Product</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<Product> FindByKeywords(string keyWords, int startIndex = 0, int count = 20);

        /// <summary>
        /// Finds a Product by Tag Id
        /// </summary>
        /// <param tagId="tagId">TagIds</param>
        /// <returns>List of Product</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<Product> FindByTagId(long tagId, int startIndex = 0, int count = 20);

        /// <summary>
        /// Find a Product by Name
        /// </summary>
        /// <param productName="productName">Product Name</param>
        /// <returns>List of Product</returns>
        /// <exception cref="InstanceNotFoundException"/>
        Product FindByName(string productName);

        /// <summary>
        /// Finds all Products
        /// <returns>List of Products</returns>
        List<Product> FindAll(int startIndex = 0, int count = 20);
    }
}