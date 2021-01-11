using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Transactions;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    [TestClass]
    public class IProductDaoTest
    {

        #region Variables

        private static IKernel kernel;
        private static IProductDao productDao;
        private static ICategoryDao categoryDao;
        private static ITagDao tagDao;
        private static IUserProfileDao userProfileDao;
        private static ICommentDao commentDao;
        private Product product;
        private Category category;
        private Category category2;
        private Tag tag;
        private UserProfile user;

        // Variables used in several tests are initialized here
        private const String categoryName = "categoryTest";

        private const string productName = "productTest";
        private const decimal productPrice = 10;
        private System.DateTime productDate = System.DateTime.Now;
        private const int productQuantity = 10;

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
            tagDao = kernel.Get<ITagDao>();
            userProfileDao = kernel.Get<IUserProfileDao>();
            commentDao = kernel.Get<ICommentDao>();
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

            category2 = new Category();
            category2.categoryName = "Category2";

            categoryDao.Create(category);
            categoryDao.Create(category2);

            tag = new Tag();
            tag.tagName = "tag";
            tagDao.Create(tag);

            user = new UserProfile();
            user.address = "address";
            user.country = "ES";
            user.language = "es";
            user.loginName = "login";
            user.enPassword = "password";
            user.firstName = "name";
            user.lastName = "surname";
            user.role = 0;
            user.email = "email";
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
        ///A test for FindByTagId.
        ///</summary>
        [TestMethod]
        public void DAO_FindByTagId()
        {
            int numberFoundProducts = 1;
            List<Product> createdProducts = new List<Product>();

            product = new Product();
            product.productName = productName + 1;
            product.productPrice = productPrice;
            product.productDate = productDate;
            product.productQuantity = productQuantity;
            product.Category = category;

            productDao.Create(product);

            Comment comment = new Comment();

            comment.comment1 = "comment";
            comment.commentDate = System.DateTime.Now;
            comment.productId = product.productId;
            comment.userId = user.usrId;
            comment.Tags.Add(tag);

            commentDao.Create(comment);

            createdProducts.Add(product);

            product = new Product();
            product.productName = "name" + 2;
            product.productPrice = productPrice;
            product.productDate = productDate;
            product.productQuantity = productQuantity;
            product.categoryId = category2.categoryId;

            productDao.Create(product);

            List<Product> totalRetrievedProducts = productDao.FindByTagId(tag.tagId);

            Assert.AreEqual(numberFoundProducts, totalRetrievedProducts.Count);

            for (int i = 0; i < numberFoundProducts; i++)
            {
                Assert.AreEqual(totalRetrievedProducts[i], createdProducts[i]);
            }
        }

        /// <summary>
        ///A test for FindByKeywordsAndCategory. With Category
        ///</summary>
        [TestMethod]
        public void DAO_FindByKeywordsAndCategoryTest_WithCategory()
        {
            int numberFoundProducts = 1;
            int numberProducts = 2;
            List<Product> createdProducts = new List<Product>();

            for (int i = 0; i < numberProducts; i++)
            {
                product = new Product();
                product.productName = productName + i;
                product.productPrice = productPrice;
                product.productDate = productDate;
                product.productQuantity = productQuantity;
                product.Category = category;

                productDao.Create(product);
                if (i == 1)
                {
                    createdProducts.Add(product);
                }

                product = new Product();
                product.productName = "name" + i;
                product.productPrice = productPrice;
                product.productDate = productDate;
                product.productQuantity = productQuantity;
                product.categoryId = category2.categoryId;

                productDao.Create(product);
            }

            List<Product> totalRetrievedProducts = productDao.FindByKeywords("1",category.categoryId);

            Assert.AreEqual(numberFoundProducts, totalRetrievedProducts.Count);

            for (int i = 0; i < numberFoundProducts; i++)
            {
                Assert.AreEqual(totalRetrievedProducts[i], createdProducts[i]);
            }
        }



        /// <summary>
        ///A test for FindByKeywordsAndCategory. Within Category
        ///</summary>
        [TestMethod]
        public void DAO_FindByKeywordsAndCategoryTest_WithinCategory()
        {
            int numberProducts = 2;
            List<Product> createdProducts = new List<Product>();

            for (int i = 0; i < numberProducts; i++)
            {
                product = new Product
                {
                    productName = productName + i,
                    productPrice = productPrice,
                    productDate = productDate,
                    productQuantity = productQuantity,
                    categoryId = category.categoryId
                };

                productDao.Create(product);

                if (i == 1)
                {
                    createdProducts.Add(product);
                }

                product = new Product
                {
                    productName = "name" + i,
                    productPrice = productPrice,
                    productDate = productDate,
                    productQuantity = productQuantity,
                    categoryId = category2.categoryId
                };

                productDao.Create(product);

                if (i == 1)
                {
                    createdProducts.Add(product);
                }
            }

            List<Product> sortedProducts = createdProducts.OrderBy(p => p.productName).ToList();

            List<Product> totalRetrievedProducts = productDao.FindByKeywords("1");

            Assert.AreEqual(sortedProducts.Count, totalRetrievedProducts.Count);

            for (int i = 0; i < numberProducts; i++)
            {
                Assert.AreEqual(totalRetrievedProducts[i], sortedProducts[i]);
            }
        }

        /// <summary>
        ///A test for FindAll.
        ///</summary>
        [TestMethod]
        public void DAO_FindAll()
        {
            int numberProducts = 8;
            List<Product> createdProducts = new List<Product>(numberProducts);

            for (int i = 0; i < numberProducts; i++)
            {
                product = new Product
                {
                    productName = productName + i,
                    productPrice = productPrice,
                    productDate = productDate,
                    productQuantity = productQuantity,
                    categoryId = category.categoryId
                };

                productDao.Create(product);
                createdProducts.Add(product);
            }

            List<Product> totalRetrievedProducts = productDao.FindAll();

            Assert.AreEqual(numberProducts, totalRetrievedProducts.Count);
            
            for (int i = 0; i < numberProducts; i++)
            {
                Assert.AreEqual(totalRetrievedProducts[i], createdProducts[i]);
            }
        }
    }
}
