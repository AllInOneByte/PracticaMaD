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

        /// <summary>
        /// update the data of a creditCard 
        /// </summary>
        /// <param creditCardId="creditCardId">creditCardId</param>
        /// <param creditCardDetails="creditCardDetails">creditCardDetails</param>
        /// <returns> void </returns>
        /// <exception cref="InstanceNotFoundException"/>
        void UpdateCreditCard(long creditCardId, CreditCardDetails creditCardDetails);
        /// <summary>
        /// Add a Credit Card
        /// </summary>
        /// <param creditCardId="creditCardId">creditCardId</param>
        /// <param creditCardDetails="creditCardDetails">creditCardDetails</param>
        /// <returns> void </returns>
        void AddCreditCard(long creditCardId, CreditCardDetails creditCardDetails);
        /// <summary>
        /// update de default creditCard
        /// </summary>
        /// <param creditCardId="creditCardId">creditCardId</param>
        ///  <param creditCardId2="creditCardId2">creditCardId2</param>
        /// <returns> void </returns>
        void AsignDefaultCard(long creditCardId, long creditCardId2);
        /// <summary>
        /// Find all creditCards of a user
        /// </summary>
        /// <param userId="userId">userId</param>
        /// <returns>A list of CreditCards</returns>
        List<CreditCard> FindAllCreditCards(long userID);



    }
}

