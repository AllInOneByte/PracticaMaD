using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.DeliveryDao
{
    /// <summary>
    /// Specific Operations for Delivery
    /// </summary>
    public class DeliveryDaoEntityFramework :
        GenericDaoEntityFramework<Delivery, long>, IDeliveryDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public DeliveryDaoEntityFramework()
        {
        }

        #endregion Public Constructors
    }
}
