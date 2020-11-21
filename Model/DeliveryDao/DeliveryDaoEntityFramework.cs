using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Exceptions;
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

        #region IDeliveryDao Members. Specific Operations
        /// <summary>
        /// Finds a List of Deliveries by userId
        /// </summary>
        /// <param userId="userId">userId</param>
        /// <returns>List of Deliverys</returns>
        /// <exception cref="InstanceNotFoundException"/>
        public List<Delivery> FindByUserId(long userId)
        {
            List<Delivery> delivery = null;

            #region Option 1: Using Linq.

            DbSet<Delivery> deliveryFound = Context.Set<Delivery>();

            var result =
                (from d in deliveryFound
                 where d.userId == userId
                 select d);

            delivery = result.ToList();

            #endregion Option 1: Using Linq.

            if (deliveryFound == null)
                throw new InstanceNotFoundException(userId,
                    typeof(DeliveryLine).FullName);

            return delivery;
        }

        #endregion IDeliveryLineDao Members. Specific Operations
    }
}
