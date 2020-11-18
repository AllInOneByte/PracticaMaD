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

        public List<string> TagNames { get; private set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentDetails"/>
        /// class.
        /// </summary>
        /// <param name="commentId">The comment's ID.</param>
        /// <param name="commentBody">The comment's body.</param>
        /// <param name="commentDate">The comment's date.</param>
        /// <param name="userId">The user's ID.</param>
        /// <param name="tagNames">The product's tags names.</param>
        public CommentDetails(long commentId, string commentBody, DateTime commentDate, long userId, List<string> tagNames)
        {
            this.CommentId = commentId;
            this.CommentBody = commentBody;
            this.CommentDate = commentDate;
            this.UserId = userId;
            this.TagNames = tagNames;
        }

    }
}
