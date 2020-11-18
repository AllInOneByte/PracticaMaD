using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductService
{
    public class CommentUpdate
    {

        #region Properties Region

        public string CommentBody { get; private set; }
        
        public List<long> TagIds { get; private set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentUpdate"/>
        /// class.
        /// </summary>
        /// <param name="commentBody">The comment's body.</param>
        /// <param name="tagIds">The product's tags IDs.</param>
        public CommentUpdate(string commentBody, List<long> tagIds)
        {
            this.CommentBody = commentBody;
            this.TagIds = tagIds;
        }
    }
}
