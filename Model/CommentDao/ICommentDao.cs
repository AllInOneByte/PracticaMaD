using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentDao
{
    public interface ICommentDao : IGenericDao<Comment, long>
    {
        /// <summary>
        /// Find a List of Comments by productId
        /// </summary>
        /// <param name="productId">productId</param>
        /// <returns>List of Comment</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<Comment> FindByProductIdOrderByDeliveryDate(long productId, int startIndex = 0, int count = 20);

        /// <summary>
        /// Find a comment by userId and productId
        /// </summary>
        /// <param name="productId"> The productId. </param>
        /// <param name="userId"> The userId. </param>
        /// <returns> A comment made by the user in that product. </returns>
        /// <exception cref="InstanceNotFoundException"/>
        Comment FindByProductIdAndUserId(long productId, long userId);
    }
}
