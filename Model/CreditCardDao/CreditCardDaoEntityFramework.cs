using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao
{
    /// <summary>
    /// Specific Operations for CreditCard
    /// </summary>
    public class CreditCardDaoEntityFramework :
        GenericDaoEntityFramework<CreditCard, Int64>, ICreditCardDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public CreditCardDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        #region ICreditCardDao Members. Specific Operations

        #region FindByUserId

        /// <summary>
        /// Finds the creditcards of a user by his id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<CreditCard> FindByUserId(long userId)
        {
            List<CreditCard> cards = new List<CreditCard>();

            #region Option 1: Using Linq.

            DbSet<CreditCard> creditCards = Context.Set<CreditCard>();

            var result =
                    (from c in creditCards
                     where c.userId == userId
                     select c);

            cards = result.ToList<CreditCard>();

            #endregion Option 1: Using Linq.

            return cards;
        }

        #endregion FindByUserId

        #region FindByNumber

        public CreditCard FindByNumber(long number)
        {
            CreditCard card = null;

            #region Option 1: Using Linq.

            DbSet<CreditCard> creditCards = Context.Set<CreditCard>();

            var result =
                    (from c in creditCards
                     where c.cardNumber == number
                     select c);

            card = result.FirstOrDefault();

            #endregion Option 1: Using Linq.

            if (card == null)
                throw new InstanceNotFoundException(number,
                    typeof(CreditCard).FullName);

            return card;
        }

        #endregion FindByNumber

        #region FindDefaultByUserIdCard

        /// <summary>
        /// Finds the default creditcard of a user by his id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CreditCard FindDefaultUserIdCard(long userId)
        {
            CreditCard card = null;

            #region Option 1: Using Linq.

            DbSet<CreditCard> creditCards = Context.Set<CreditCard>();

            var result =
                    (from c in creditCards
                     where c.userId == userId && c.defaultCard == 1
                     select c);

            card = result.FirstOrDefault();

            #endregion Option 1: Using Linq.

            if (card == null)
                throw new InstanceNotFoundException(userId,
                    typeof(CreditCard).FullName);

            return card;
        }

        #endregion FindDefaultByUserIdCard

        #endregion ICreditCardDaoMembers

    }

}
