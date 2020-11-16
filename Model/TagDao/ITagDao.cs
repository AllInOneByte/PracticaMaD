using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagDao
{
    public interface ITagDao : IGenericDao<Tag, long>
    {
        /// <summary>
        /// Finds all Tags
        /// <returns>List of Tags</returns>
        List<Tag> FindAll();
    }
}
