using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductService
{
    public class ProductService : IProductService
    {
        [Inject]
        IProductDao productDao { private get; set; }

        [Inject]
        ICommentDao comment { private get; set; }

        [Inject]
        ITagDao tagDao { private get; set; }

        #region IProductService Members

        #region Product Members

        #endregion Product Members

        #region Comment Members

        #endregion Comment Members

        #region Tag Members

        #endregion Tag Members

        #endregion IProductService Members
    }
}
