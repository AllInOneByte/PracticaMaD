using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagDao
{
    class TagDaoEntityFramework :
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
        public List<Tag> FindAll()
        {
            List<Tag> tags = new List<Tag>();

            #region Option 1: Using Linq.

            DbSet<Tag> dbTags = Context.Set<Tag>();

            var result =
                    (from t in dbTags
                     select t);

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
        public FindByName(string tagName)
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
                    typeof(Tag).tagName);

            return tag;
        }

        #endregion ITagDaoMembers
    }
}
