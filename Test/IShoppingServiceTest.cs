using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    /// <summary>
    /// This is a test class for IShoppingServiceTest and is intended to contain all IShoppingServiceTest
    /// Unit Tests
    /// </summary>
    [TestClass()]
    public class IShoppingServiceTest
    {
        #region Variables

        private static IKernel kernel;
        private static IShoppingService shoppingService;
        private static IUserProfileDao userProfileDao;
        private static ICreditCardDao creditCardDao;
        private static ICategoryDao categoryDao; 
        private static IProductDao productDao;
        private static CreditCard creditCard;
        private static UserProfile user;
        private static Category category;
        private static Product product;
        private static ShoppingCart shoppingCart;
        private static ShoppingCartDetails shoppingCartDetails;

        private TransactionScope transaction;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #endregion Variables

        /// <summary>
        /// A test for CreateDelivery.
        /// </summary>
        [TestMethod()]
        public void CreateDeliveryTest()
        {
            try
            {
                List<ShoppingCartDetails> details = new List<ShoppingCartDetails>
                {
                    shoppingCartDetails
                };

                ShoppingCart savedCart = shoppingService.CreateDelivery(shoppingCart, details);

                Assert.IsNotNull(savedCart.DeliveryId);
                Assert.AreEqual(shoppingCart.CardId, savedCart.CardId);
                Assert.AreEqual(shoppingCart.DeliveryAddress, savedCart.DeliveryAddress);
                Assert.AreEqual(shoppingCart.DeliveryDate, savedCart.DeliveryDate);
                Assert.AreEqual(shoppingCart.DeliveryPrice, savedCart.DeliveryPrice);
                Assert.AreEqual(shoppingCart.Description, savedCart.Description);
                Assert.AreEqual(shoppingCart.UserId, savedCart.UserId);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// A test for GetAllDeliveries.
        /// </summary>
        [TestMethod()]
        public void GetAllDeliveriesTest()
        {
            try
            {
                List<ShoppingCartDetails> details = new List<ShoppingCartDetails>
                {
                    shoppingCartDetails
                };

                ShoppingCart savedCart = shoppingService.CreateDelivery(shoppingCart, details);
                List<ShoppingCart> foundCarts = shoppingService.GetAllDeliveries(user.usrId);

                Assert.AreEqual(1, foundCarts.Count);
                Assert.AreEqual(savedCart, foundCarts[0]);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// A test for GetDeliveryDetails.
        /// </summary>
        [TestMethod()]
        public void GetDeliveryDetailsTest()
        {
            try
            {
                List<ShoppingCartDetails> details = new List<ShoppingCartDetails>
                {
                    shoppingCartDetails
                };

                ShoppingCart savedCart = shoppingService.CreateDelivery(shoppingCart, details);
                List<ShoppingCartDetails> foundDetails = shoppingService.GetDeliveryDetails(savedCart.DeliveryId);

                Assert.AreEqual(1, foundDetails.Count);
                Assert.AreEqual(savedCart.DeliveryId, foundDetails[0].DeliveryId);
                Assert.AreEqual(shoppingCartDetails.DeliveryLineAmount, foundDetails[0].DeliveryLineAmount);
                Assert.AreEqual(shoppingCartDetails.DeliveryLinePrice, foundDetails[0].DeliveryLinePrice);
                Assert.AreEqual(shoppingCartDetails.ProductId, foundDetails[0].ProductId);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();

            shoppingService = kernel.Get<IShoppingService>();
            userProfileDao = kernel.Get<IUserProfileDao>();
            creditCardDao = kernel.Get<ICreditCardDao>();
            categoryDao = kernel.Get<ICategoryDao>();
            productDao = kernel.Get<IProductDao>();
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

            UserProfile newUser = new UserProfile
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

            userProfileDao.Create(newUser);
            user = newUser;

            CreditCard defaultCreditCard = new CreditCard
            {
                cardNumber = 5231962446920945,
                cardType = "débito",
                expirationDate = DateTime.Now.AddMonths(1),
                userId = user.usrId,
                verificationCode = 555,
                defaultCard = 1
            };

            creditCardDao.Create(defaultCreditCard);
            creditCard = defaultCreditCard;

            Category newCategory = new Category
            {
                categoryName = "Category test"
            };

            categoryDao.Create(newCategory);
            category = newCategory;

            Product newProduct = new Product
            {
                categoryId = category.categoryId,
                productDate = DateTime.Now.AddMonths(-1),
                productName = "Test product",
                productPrice = new decimal(9.99),
                productQuantity = 100
            };

            productDao.Create(newProduct);
            product = newProduct;

            shoppingCart = new ShoppingCart(DateTime.Now, new decimal(9.99),
                user.address, "Test description", user.usrId, creditCard.cardId);

            shoppingCartDetails = new ShoppingCartDetails(1, new decimal(9.99),
                product.productId);
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }

        #endregion Additional test attributes
    }
}