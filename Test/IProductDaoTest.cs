using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    [TestClass]
    public class IProductDaoTest
    {

        #region Variables

        private static IKernel kernel;
        private static IProductDao productDao;
        private static ICategoryDao categoryDao;
        private static Product product;
        private static Category category;

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

        /// <summary>
        ///A test for FindByKeywordsAndCategory. With Category
        ///</summary>
        [TestMethod]
        public void DAO_FindByKeywordsAndCategoryTest_WithCategory()
        {
            category = new Category();
            category.categoryName = categoryName;

            categoryDao.Create(category);

            Category category2 = new Category();
            category2.categoryName = "Category2";

            categoryDao.Create(category2);

            int numberProducts = 1;
            List<Product> createProducts = new List<Product>();
            product = new Product();
            for (int i = 0; i < 2; i++)
            {
                product.categoryId = category.categoryId;
                product.productName = "name" + i;
                product.productPrice = productPrice;
                product.productQuantity = productQuantity;
                product.productDate = productDate;
                productDao.Create(product);
                createProducts.Add(product);
            }
            for (int i = 0; i < 2; i++)
            {
                product.categoryId = category2.categoryId;
                product.productName = "name" + i;
                product.productPrice = productPrice;
                product.productQuantity = productQuantity;
                product.productDate = productDate;
                productDao.Create(product);
                createProducts.Add(product);
            }

            int count = 1;
            int startIndex = 0;

            List<Product> listProduct;
            List<Product> totalRetrievedProducts = new List<Product>(numberProducts);

            do
            {
                listProduct = productDao.FindByKeywordsAndCategory("1", category.categoryId);

                totalRetrievedProducts.AddRange(listProduct);

                Assert.IsTrue(listProduct.Count <= count);

                startIndex += count;
            } while (startIndex < numberProducts);

            // Check the total number of account retrieved
            Assert.AreEqual(1, totalRetrievedProducts.Count);

            // are the accounts retrieved the same than the originals?
            for (int i = 0; i < numberProducts; i++)
            {
                Assert.AreEqual(totalRetrievedProducts[i], createProducts[i]);
            }
        }

        /// <summary>
        ///A test for FindByKeywordsAndCategory. Within Category
        ///</summary>
        [TestMethod]
        public void DAO_FindByKeywordsAndCategoryTest_WithinCategory()
        {
            category = new Category();
            category.categoryName = categoryName;

            categoryDao.Create(category);

            Category category2 = new Category();
            category2.categoryName = "Category2";

            categoryDao.Create(category2);

            int numberProducts = 2;
            List<Product> createProducts = new List<Product>();
            product = new Product();
            for (int i = 0; i < 2; i++)
            {
                product.categoryId = category.categoryId;
                product.productName = "name" + i;
                product.productPrice = productPrice;
                product.productQuantity = productQuantity;
                product.productDate = productDate;
                productDao.Create(product);
                createProducts.Add(product);
            }
            for (int i = 0; i < 2; i++)
            {
                product.categoryId = category2.categoryId;
                product.productName = "name" + i;
                product.productPrice = productPrice;
                product.productQuantity = productQuantity;
                product.productDate = productDate;
                productDao.Create(product);
                createProducts.Add(product);
            }

            int count = 2;
            int startIndex = 0;

            List<Product> listProduct;
            List<Product> totalRetrievedProducts = new List<Product>(numberProducts);

            do
            {
                listProduct = productDao.FindByKeywordsAndCategory("1", -1);

                totalRetrievedProducts.AddRange(listProduct);

                Assert.IsTrue(listProduct.Count <= count);

                startIndex += count;
            } while (startIndex < numberProducts);

            // Check the total number of account retrieved
            Assert.AreEqual(1, totalRetrievedProducts.Count);

            // are the accounts retrieved the same than the originals?
            for (int i = 0; i < numberProducts; i++)
            {
                Assert.AreEqual(totalRetrievedProducts[i], createProducts[i]);
            }
        }

        /// <summary>
        ///A test for FindAll.
        ///</summary>
        [TestMethod]
        public void DAO_FindAll()
        {
            int numberProducts = 11;

            List<Product> createdProducts = new List<Product>(numberProducts);

            category = new Category();
            category.categoryName = categoryName;

            categoryDao.Create(category);

            for (int i = 0; i < numberProducts; i++)
            {
                product.categoryId = category.categoryId;
                product.productName = productName;
                product.productPrice = productPrice;
                product.productQuantity = productQuantity;
                product.productDate = productDate;
                productDao.Create(product);
                createdProducts.Add(product);
            }

            int count = 10;
            int startIndex = 0;

            List<Product> listProducts;
            List<Product> totalRetrievedProducts = new List<Product>(numberProducts);

            /* Get the accounts in blocks of "count" size */
            do
            {
                listProducts = productDao.FindAll();

                totalRetrievedProducts.AddRange(listProducts);

                Assert.IsTrue(listProducts.Count <= count);

                startIndex += count;
            } while (startIndex < numberProducts);

            // Check the total number of account retrieved
            Assert.AreEqual(numberProducts, totalRetrievedProducts.Count);

            // are the accounts retrieved the same than the originals?
            for (int i = 0; i < numberProducts; i++)
            {
                Assert.AreEqual(totalRetrievedProducts[i], createdProducts[i]);
            }
        }
    }
}
