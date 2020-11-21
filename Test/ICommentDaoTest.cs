using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Transactions;
namespace Es.Udc.DotNet.PracticaMaD.Test
{
    [TestClass]
    public class ICommentDaoTest
    {

        #region Variables

        private static IKernel kernel;
        private static IProductDao productDao;
        private static ICategoryDao categoryDao;
        private static ICommentDao commentDao;
        private static Product product;
        private static Category category;
        private static Comment comment;

        // Variables used in several tests are initialized here
        private const String categoryName = "categoryTest";

        private const string productName = "productTest";
        private const decimal productPrice = 10;
        private const int productQuantity = 10;

        private System.DateTime date = System.DateTime.Now;

        private const string commentBody = "CommentTest";

        private TransactionScope transaction;

        private TestContext testContextInstance;

        #endregion Variables

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();
            productDao = kernel.Get<IProductDao>();
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

            category = new Category();
            category.categoryName = categoryName;

            categoryDao.Create(category);

            product.categoryId = category.categoryId;
            product.productName = productName;
            product.productPrice = productPrice;
            product.productQuantity = productQuantity;
            product.productDate = date;
            productDao.Create(product);
        }

        #endregion Additional test attributes

        /// <summary>
        ///A test for FindByProductIdOrderByDeliveryDate.
        ///</summary>
        [TestMethod]
        public void DAO_FindByProductIdOrderByDeliveryDateTest()
        {
            int numberComments = 3;

            List<Comment> createdComments = new List<Comment>(numberComments);

            for (int i = 0; i < numberComments; i++)
            {
                comment = new Comment();
                comment.comment1 = commentBody;
                comment.commentDate = date;
                comment.productId = product.productId;
                commentDao.Create(comment);
                createdComments.Add(comment);
            }

            int count = 2;
            int startIndex = 0;

            List<Comment> listComments;
            List<Comment> totalRetrievedComments = new List<Comment>(numberComments);

            /* Get the accounts in blocks of "count" size */
            do
            {
                listComments = commentDao.FindByProductIdOrderByDeliveryDate(product.productId);

                totalRetrievedComments.AddRange(listComments);

                Assert.IsTrue(listComments.Count <= count);

                startIndex += count;
            } while (startIndex < numberComments);

            // Check the total number of account retrieved
            Assert.AreEqual(numberComments, totalRetrievedComments.Count);

            // are the accounts retrieved the same than the originals?
            for (int i = 0; i < numberComments; i++)
            {
                Assert.AreEqual(totalRetrievedComments[i], createdComments[i]);
            }
        }
    }
}

