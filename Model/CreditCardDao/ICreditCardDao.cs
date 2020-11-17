using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao
{
    public interface ICreditCardDao : IGenericDao<CreditCard, long>
    {
        /// <summary>
        /// Finds a CreditCard by userId
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>List of CreditCard</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<CreditCard> FindByUserId(long userId);

        /// <summary>
        /// Finds a default CreditCard by userId 
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>The Default CreditCard</returns>
        /// <exception cref="InstanceNotFoundException"/>
        CreditCard FindDefaultUserIdCard(long userId);


    }
}

