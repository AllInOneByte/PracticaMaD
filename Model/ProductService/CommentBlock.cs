using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductService
{
    public class CommentBlock
    {
        public List<Comment> Comments { get; private set; }
        public bool ExistMoreComments { get; private set; }

        public CommentBlock(List<Comment> comments, bool existMoreComments)
        {
            Comments = comments;
            ExistMoreComments = existMoreComments;
        }
    }
}
