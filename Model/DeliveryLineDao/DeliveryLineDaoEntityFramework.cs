using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.DeliveryLineDao
{
    /// <summary>
    /// Specific Operations for Delivery
    /// </summary>
    public class DeliveryLineDaoEntityFramework :
        GenericDaoEntityFramework<DeliveryLine, long>, IDeliveryLineDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public DeliveryLineDaoEntityFramework()
        {
        }

        #endregion Public Constructors
    }
}
