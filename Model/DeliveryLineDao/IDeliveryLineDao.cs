using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.DeliveryLineDao
{
    public interface IDeliveryLineDao : IGenericDao<DeliveryLine, long>
    {
        /// <summary>
        /// Finds a List of DeliveryLines by deliveryId
        /// </summary>
        /// <param deliveryId="deliveryId">deliveryId</param>
        /// <returns>List of DeliberyLine</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<DeliveryLine> FindByDeliveryId(long deliveryId, int startIndex = 0, int count = 20);
    }
}

