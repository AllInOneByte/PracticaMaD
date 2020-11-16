using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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

        #region ICommentDao Members. Specific Operations

        public List<Comment> FindByProductIdOrderByDeliveryDate(long productId)
        {
            List<Comment> comments = null;

            #region Option 1: Using Linq.

            DbSet<Comment> commentsFound = Context.Set<Comment>();

            var result =
                (from c in commentsFound
                 where c.productId == productId
                 orderby c.commentDate descending
                 select c);

            comments = result.ToList();

            #endregion Option 1: Using Linq.

            if (comments == null)
                throw new InstanceNotFoundException(productId,
                    typeof(Comment).FullName);

            return comments;
        }

        #endregion ICommentDao Members. Specific Operations
    }
}
