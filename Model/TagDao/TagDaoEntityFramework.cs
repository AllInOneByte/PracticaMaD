using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagDao
{
    public class TagDaoEntityFramework :
         GenericDaoEntityFramework<Tag, long>, ITagDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public TagDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        #region ITagDao Members. Specific Operations

        /// <summary>
        /// Finds all the tags
        /// </summary>
        /// <returns></returns>
        public List<Tag> FindAll(int startIndex = 0, int count = 20)
        {
            List<Tag> tags = new List<Tag>();

            #region Option 1: Using Linq.

            DbSet<Tag> dbTags = Context.Set<Tag>();

            var result =
                    (from t in dbTags
                     select t).Skip(startIndex).Take(count);

            tags = result.ToList<Tag>();

            #endregion Option 1: Using Linq.

            return tags;
        }

        /// <summary>
        /// Finds a tag by his name
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public Tag FindByName(string tagName)
        {
            Tag tag = null;

            #region Option 1: Using Linq.

            DbSet<Tag> tags = Context.Set<Tag>();

            var result =
                (from t in tags
                 where t.tagName == tagName
                 select t);

            tag = result.FirstOrDefault();

            #endregion Option 1: Using Linq.

            if (tag == null)
                throw new InstanceNotFoundException(tagName,
                    typeof(Tag).FullName);

            return tag;
        }

        #endregion ITagDaoMembers
    }
}
