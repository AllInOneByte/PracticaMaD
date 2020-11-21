﻿using Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.SpecificPropertyDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.LabeledDao;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Ninject;
using System.Configuration;
using System.Data.Entity;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    class TestManager
    {
        /// <summary>
        /// Configures and populates the Ninject kernel
        /// </summary>
        /// <returns>The NInject kernel</returns>
        public static IKernel ConfigureNInjectKernel()
        {
            NinjectSettings settings = new NinjectSettings() { LoadExtensions = true };

            IKernel kernel = new StandardKernel(settings);

            kernel.Bind<IProductDao>().
                To<ProductDaoEntityFramework>();

            kernel.Bind<ICategoryDao>().
                To<CategoryDaoEntityFramework>();

            kernel.Bind<ISpecificPropertyDao>().
                To<SpecificPropertyDaoEntityFramework>();

            kernel.Bind<ITagDao>().
                To<TagDaoEntityFramework>();

            kernel.Bind<ICommentDao>().
                To<CommentDaoEntityFramework>();

            kernel.Bind<IUserProfileDao>().
                To<UserProfileDaoEntityFramework>();

            kernel.Bind<ILabeledDao>().
                To<LabeledDaoEntityFramework>();

            kernel.Bind<IProductService>().
                To<ProductService>();

            kernel.Bind<ICreditCardDao>().
                To<CreditCardDaoEntityFramework>();

            kernel.Bind<IShoppingService>().
                To<ShoppingService>();

            string connectionString = ConfigurationManager.ConnectionStrings["practicamadEntities"].ConnectionString;

            kernel.Bind<DbContext>().
                ToSelf().
                InSingletonScope().
                WithConstructorArgument("nameOrConnectionString", connectionString);

            return kernel;
        }

        /// <summary>
        /// Configures the Ninject kernel from an external module file.
        /// </summary>
        /// <param name="moduleFilename">The module filename.</param>
        /// <returns>The NInject kernel</returns>
        public static IKernel ConfigureNInjectKernel(string moduleFilename)
        {
            NinjectSettings settings = new NinjectSettings() { LoadExtensions = true };
            IKernel kernel = new StandardKernel(settings);

            kernel.Load(moduleFilename);

            return kernel;
        }

        public static void ClearNInjectKernel(IKernel kernel)
        {
            kernel.Dispose();
        }
    }
}