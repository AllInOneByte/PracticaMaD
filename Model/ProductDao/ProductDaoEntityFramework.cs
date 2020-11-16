using Es.Udc.DotNet.ModelUtil.Dao;

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
    }
}