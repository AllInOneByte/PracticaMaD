using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.SpecificPropertyDao
{
    /// <summary>
    /// Specific Operations for SpecificProperty
    /// </summary>
    public class SpecificPropertyDaoEntityFramework :
        GenericDaoEntityFramework<SpecificProperty, long>, ISpecificPropertyDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public SpecificPropertyDaoEntityFramework()
        {
        }

        #endregion Public Constructors
    }
}
