using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductService
{
    public class CommentDetails
    {

        #region Properties Region

        public long CommentId { get; private set; }

        public string CommentBody { get; private set; }

        public System.DateTime CommentDate { get; private set; }

        public long UserId { get; private set; }

        #endregion

        public CommentDetails(long commentId, string commentBody, DateTime commentDate, long userId)
        {
            this.CommentId = commentId;
            this.CommentBody = commentBody;
            this.CommentDate = commentDate;
            this.UserId = userId;
        }

        public override bool Equals(object obj)
        {
            var details = obj as CommentDetails;
            return details != null &&
                   CommentId == details.CommentId &&
                   CommentBody == details.CommentBody &&
                   CommentDate == details.CommentDate &&
                   UserId == details.UserId;
        }

        public override int GetHashCode()
        {
            var hashCode = 2029225270;
            hashCode = hashCode * -1521134295 + CommentId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CommentBody);
            hashCode = hashCode * -1521134295 + CommentDate.GetHashCode();
            hashCode = hashCode * -1521134295 + UserId.GetHashCode();
            return hashCode;
        }
    }
}
