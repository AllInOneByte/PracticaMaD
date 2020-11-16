using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentDao
{
    /// <summary>
    /// Specific Operations for Comment
    /// </summary>
    public class CommentDaoEntityFramework :
        GenericDaoEntityFramework<Comment, long>, ICommentDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public CommentDaoEntityFramework()
        {
        }

        #endregion Public Constructors
    }
}
