using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentDao
{
    public interface ICommentDao : IGenericDao<Comment, long>
    {
        /// <summary>
        /// Finds a List of Comments by productId
        /// </summary>
        /// <param name="productId">productId</param>
        /// <returns>List of Comment</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<Comment> FindByProductIdOrderByDeliveryDate(long productId);
    }
}
