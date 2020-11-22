using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
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
        private static IUserProfileDao userProfileDao;

        // Variables used in several tests are initialized here
        private Category category;
        private Product product;
        private UserProfile user;

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
            commentDao = kernel.Get<ICommentDao>();
            userProfileDao = kernel.Get<IUserProfileDao>();
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

            category = new Category();
            category.categoryName = categoryName;

            categoryDao.Create(category);

            product = new Product();
            product.categoryId = category.categoryId;
            product.productName = productName;
            product.productPrice = productPrice;
            product.productQuantity = productQuantity;
            product.productDate = date;


            productDao.Create(product);

            user = new UserProfile
            {
                loginName = "loginNameTest",
                enPassword = "password",
                firstName = "name",
                lastName = "lastName",
                email = "user@udc.es",
                language = "es",
                country = "ES",
                role = 0,
                address = "addressTest"
            };

            userProfileDao.Create(user);
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transaction.Dispose();
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
                var comment = new Comment();
                comment.comment1 = commentBody;
                comment.commentDate = date;
                comment.productId = product.productId;
                comment.userId = user.usrId;
                commentDao.Create(comment);
                createdComments.Add(comment);
            }

            List<Comment> totalRetrievedComments = commentDao.FindByProductIdOrderByDeliveryDate(product.productId);

            Assert.AreEqual(numberComments, totalRetrievedComments.Count);
            
            for (int i = 0; i < numberComments; i++)
            {
                Assert.AreEqual(totalRetrievedComments[i], createdComments[i]);
            }
        }
    }
}

