using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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

        #region IDeliveryLineDao Members. Specific Operations

        public List<DeliveryLine> FindByDeliveryId(long deliveryId, int startIndex = 0, int count = 20)
        {
            List<DeliveryLine> deliveryLines = null;

            #region Option 1: Using Linq.

            DbSet<DeliveryLine> deliveryLinesFound = Context.Set<DeliveryLine>();

            var result =
                (from d in deliveryLinesFound
                 where d.deliveryId == deliveryId
                 orderby d.deliveryLineId ascending
                 select d).Skip(startIndex).Take(count);

            deliveryLines = result.ToList();

            #endregion Option 1: Using Linq.

            if (deliveryLines == null)
                throw new InstanceNotFoundException(deliveryId,
                    typeof(DeliveryLine).FullName);

            return deliveryLines;
        }

        #endregion IDeliveryLineDao Members. Specific Operations
    }
}
