using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductService
{
    class CommentDetails
    {

        #region Properties Region

        public long CommentId { get; private set; }

        public string CommentBody { get; private set; }

        public System.DateTime CommentDate { get; private set; }

        public long UserId { get; private set; }

        #endregion

        public CommentDetails(long commentId, string commentBody, System.DataTime commentDate, long userId)
        {
            this.CommentId = commentId;
            this.CommentBody = commentBody;
            this.CommentDate = commentDate;
            this.UserId = userId;
        }
    }
}
