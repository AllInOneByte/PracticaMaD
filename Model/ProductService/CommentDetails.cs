using System;
using System.Collections.Generic;

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

        public override bool Equals(object obj)
        {
            var details = obj as CommentDetails;
            return details != null &&
                   CommentId == details.CommentId &&
                   CommentBody == details.CommentBody &&
                   CommentDate == details.CommentDate &&
                   UserId == details.UserId &&
                   EqualityComparer<List<string>>.Default.Equals(TagNames, details.TagNames);
        }

        public override int GetHashCode()
        {
            var hashCode = 463174743;
            hashCode = hashCode * -1521134295 + CommentId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CommentBody);
            hashCode = hashCode * -1521134295 + CommentDate.GetHashCode();
            hashCode = hashCode * -1521134295 + UserId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<string>>.Default.GetHashCode(TagNames);
            return hashCode;
        }
    }
}
