using Es.Udc.DotNet.ModelUtil.Dao;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagDao
{
    public interface ITagDao : IGenericDao<Tag, long>
    {
        /// <summary>
        /// Finds all Tags
        /// <returns>List of Tags</returns>
        List<Tag> FindAll();

        /// <summary>
        /// Finds a tag by his name
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        Tag FindByName(string tagName);
    }
}
