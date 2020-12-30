using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductService
{
    public class TagBlock
    {
        public List<Tag> Tags { get; private set; }
        public bool ExistMoreTags { get; private set; }

        public TagBlock(List<Tag> tags, bool existMoreTags)
        {
            Tags = tags;
            ExistMoreTags = existMoreTags;
        }
    }
}
