using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Collections.Generic;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    [TestClass]
    public class ICategoryDaoTest
    {
        #region Variables

        private static IKernel kernel;
        private static ICategoryDao categoryDao;

        private TransactionScope transaction;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #endregion Variables

        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();
            categoryDao = kernel.Get<ICategoryDao>();
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            transaction = new TransactionScope();
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }

        #endregion Additional test attributes

        [TestMethod()]
        public void DAO_FindAllTest()
        {
            int numberCategories = 5;

            List<Category> createdCategories = new List<Category>(numberCategories);

            for (int i = 0; i < numberCategories; i++)
            {
                Category cat = new Category
                {
                    categoryName = "CategoryTest" + i
                };

                categoryDao.Create(cat);
                createdCategories.Add(cat);
            }

            List<Category> retrievedCategories = categoryDao.FindAll();

            Assert.AreEqual(numberCategories, retrievedCategories.Count);

            for (int i = 0; i < numberCategories; i++)
            {
                Assert.AreEqual(retrievedCategories[i], createdCategories[i]);
            }
        }
    }
}
