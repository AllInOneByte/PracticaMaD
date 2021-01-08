using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.DeliveryDao
{
    public interface IDeliveryDao : IGenericDao<Delivery, long>
    {
        /// <summary>
        /// Finds a List of Deliverys by userId
        /// </summary>
        /// <param userId="userId">userId</param>
        /// <returns>List of Deliverys</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<Delivery> FindByUserId(long userId, int startIndex = 0, int count = 20);
    }
}

