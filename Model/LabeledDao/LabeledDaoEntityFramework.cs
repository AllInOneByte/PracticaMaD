using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.LabeledDao
{
    /// <summary>
    /// Specific Operations for Labeled
    /// </summary>
    public class LabeledDaoEntityFramework :
        GenericDaoEntityFramework<Labeled, long>, ILabeledDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public LabeledDaoEntityFramework()
        {
        }

        #endregion Public Constructors
    }
}
