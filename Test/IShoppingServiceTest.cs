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
                List<ShoppingCart> details = new List<ShoppingCart>
                {
                    shoppingCart
                };

                Delivery delivery = shoppingService.CreateDelivery(new decimal(9.99), creditCard.cardNumber,
                    user.usrId, "Short description", details);

                Assert.IsNotNull(delivery.deliveryId);
                Assert.AreEqual(creditCard.cardId, delivery.cardId);
                Assert.AreEqual(user.address, delivery.deliveryAddress);
                Assert.IsNotNull(delivery.deliveryDate);
                Assert.AreEqual(new decimal(9.99), delivery.deliveryPrice);
                Assert.AreEqual("Short description", delivery.description);
                Assert.AreEqual(user.usrId, delivery.userId);
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
                List<ShoppingCart> details = new List<ShoppingCart>
                {
                    shoppingCart
                };

                Delivery delivery = shoppingService.CreateDelivery(new decimal(9.99), creditCard.cardNumber,
                    user.usrId, "Short description", details);
                DeliveryBlock foundDeliveries = shoppingService.GetAllDeliveries(user.usrId);

                Assert.AreEqual(1, foundDeliveries.Deliveries.Count);
                Assert.AreEqual(creditCard.cardNumber, foundDeliveries.Deliveries[0].CardNumber);
                Assert.AreEqual(System.DateTime.Now.ToString("dd/MM/yyyy"), foundDeliveries.Deliveries[0].DeliveryDate);
                Assert.AreEqual(product.productPrice, foundDeliveries.Deliveries[0].DeliveryPrice);
                Assert.AreEqual("Short description", foundDeliveries.Deliveries[0].Description);
                Assert.AreEqual(delivery.deliveryId, foundDeliveries.Deliveries[0].DeliveryId);
                Assert.IsFalse(foundDeliveries.ExistMoreDeliveries);
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
                List<ShoppingCart> details = new List<ShoppingCart>
                {
                    shoppingCart
                };

                Delivery delivery = shoppingService.CreateDelivery(new decimal(9.99), creditCard.cardNumber,
                    user.usrId, "Short description", details);
                DeliveryLineBlock foundDetails = shoppingService.GetDeliveryDetails(delivery.deliveryId);

                Assert.AreEqual(1, foundDetails.DeliveryLines.Count);
                Assert.AreEqual(shoppingCart.Amount, foundDetails.DeliveryLines[0].DeliveryLineAmount);
                Assert.AreEqual(product.productPrice, foundDetails.DeliveryLines[0].DeliveryLinePrice);
                Assert.AreEqual(product.productName, foundDetails.DeliveryLines[0].ProductName);
                Assert.IsFalse(foundDetails.ExistMoreDeliveryLines);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        // <summary>
        /// A test for UpdateShoppingCartDetails.
        /// </summary>
        [TestMethod()]
        public void UpdateShoppingCartDetailsTest()
        {
            try
            {
                List<ShoppingCart> details = new List<ShoppingCart>();

                details = shoppingService.UpdateShoppingCartDetails(details, product.productId, 1, false);

                Assert.AreEqual(1, details.Count);
                Assert.AreEqual(shoppingCart.Amount, details[0].Amount);
                Assert.AreEqual(product, details[0].Product);
                Assert.AreEqual(false, details[0].Gift);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        // <summary>
        /// A test for DeleteShoppingCartDetails.
        /// </summary>
        [TestMethod()]
        public void DeleteShoppingCartDetailsTest()
        {
            try
            {
                List<ShoppingCart> details = new List<ShoppingCart>();

                details = shoppingService.UpdateShoppingCartDetails(details, product.productId, 1, false);

                Assert.AreEqual(1, details.Count);

                details = shoppingService.DeleteShoppingCartDetails(details, product.productId);

                Assert.AreEqual(0, details.Count);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        // <summary>
        /// A test for ModifyAmountOfItems.
        /// </summary>
        [TestMethod()]
        public void ModifyAmountOfItemsTest()
        {
            try
            {
                List<ShoppingCart> details = new List<ShoppingCart>();

                details = shoppingService.UpdateShoppingCartDetails(details, product.productId, 1, false);

                Assert.AreEqual(1, details.Count);
                Assert.AreEqual(shoppingCart.Amount, details[0].Amount);
                Assert.AreEqual(product, details[0].Product);
                Assert.AreEqual(false, details[0].Gift);

                details = shoppingService.UpdateShoppingCartDetails(details, product.productId, details[0].Amount+1, false);

                Assert.AreEqual(1, details.Count);
                Assert.AreEqual(shoppingCart.Amount+1, details[0].Amount);
                Assert.AreEqual(product, details[0].Product);
                Assert.AreEqual(false, details[0].Gift);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        // <summary>
        /// A test for ModifyGift.
        /// </summary>
        [TestMethod()]
        public void ModifyGiftTest()
        {
            try
            {
                List<ShoppingCart> details = new List<ShoppingCart>();

                details = shoppingService.UpdateShoppingCartDetails(details, product.productId, 1, false);

                Assert.AreEqual(1, details.Count);
                Assert.AreEqual(shoppingCart.Amount, details[0].Amount);
                Assert.AreEqual(product, details[0].Product);
                Assert.AreEqual(false, details[0].Gift);

                details = shoppingService.ModifyGift(details, product.productId, true);

                Assert.AreEqual(1, details.Count);
                Assert.AreEqual(shoppingCart.Amount, details[0].Amount);
                Assert.AreEqual(product, details[0].Product);
                Assert.AreEqual(true, details[0].Gift);
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

            shoppingCart = new ShoppingCart(1, product, false);
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