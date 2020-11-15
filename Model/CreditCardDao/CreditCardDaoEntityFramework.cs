using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao
{
    /// <summary>
    /// Specific Operations for CreditCard
    /// </summary>
    public class CreditCardDaoEntityFramework :
        GenericDaoEntityFramework<CreditCard, long>, ICreditCardDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public CreditCardDaoEntityFramework()
        {
        }

        #endregion Public Constructors
    }
}
