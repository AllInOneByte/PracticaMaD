using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.DeliveryDao;
using Es.Udc.DotNet.PracticaMaD.Model.DeliveryLineDao;
using Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Es.Udc.DotNet.PracticaMaD.Model;
using Ninject;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    [TestClass]
    public class IDeliveryLineTest
    {

        #region Variables

        private static IKernel kernel;
        private static IUserProfileDao userProfileDao;
        private static IDeliveryDao deliveryDao;
        private static ICreditCardDao creditCardDao;
        private static IDeliveryLineDao deliveryLineDao;
        private static IProductDao productDao;
        private static ICategoryDao categoryDao;
        private UserProfile userProfile;
        private Delivery delivery;
        private Delivery delivery2;
        private CreditCard creditCard;
        private DeliveryLine deliveryLine;
        private Product product;
        private Category category;

        // Variables used in several tests are initialized here
        private const String loginName = "loginNameTest";
        private const String password = "password";
        private const String firstName = "name";
        private const String lastName = "lastName";
        private const String email = "user@udc.es";
        private const String language = "es";
        private const String country = "ES";
        private const byte role = 0;
        private const String address = "address";
        private const long NON_EXISTENT_USER_ID = -1;

        private const long CARD_NUMBER = 5231962446920945;
        private const int VER_CODE = 555;
        private const string TYPE = "débito";

        private System.DateTime deliveryDate = System.DateTime.Now;
        private const decimal deliveryPrice = 10;
        private const string description = "IDeliveryDaoTest";

        private const String categoryName = "categoryTest";

        private const string productName = "productTest";
        private const decimal productPrice = 10;
        private System.DateTime productDate = System.DateTime.Now;
        private const int productQuantity = 10;

        private const int deliveryLineAmount = 3;
        private const decimal deliveryLinePrice = 12;

        private TransactionScope transaction;

        private TestContext testContextInstance;

        #endregion

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
            userProfileDao = kernel.Get<IUserProfileDao>();
            creditCardDao = kernel.Get<ICreditCardDao>();
            deliveryDao = kernel.Get<IDeliveryDao>();
            deliveryLineDao = kernel.Get<IDeliveryLineDao>();
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

            userProfile = new UserProfile();
            userProfile.loginName = loginName;
            userProfile.enPassword = password;
            userProfile.firstName = firstName;
            userProfile.lastName = lastName;
            userProfile.email = email;
            userProfile.language = language;
            userProfile.country = country;
            userProfile.role = role;
            userProfile.address = address;

            userProfileDao.Create(userProfile);

            creditCard = new CreditCard();

            creditCard.cardType = TYPE;
            creditCard.cardNumber = CARD_NUMBER;
            creditCard.defaultCard = 1;
            creditCard.expirationDate = System.DateTime.Now;
            creditCard.verificationCode = VER_CODE;
            creditCard.userId = userProfile.usrId;

            creditCardDao.Create(creditCard);

            delivery = new Delivery();
            delivery.deliveryAddress = address;
            delivery.deliveryDate = deliveryDate;
            delivery.deliveryPrice = deliveryPrice;
            delivery.description = description;
            delivery.userId = userProfile.usrId;
            delivery.cardId = creditCard.cardId;

            deliveryDao.Create(delivery);

            delivery2 = new Delivery();
            delivery2.deliveryAddress = address;
            delivery2.deliveryDate = deliveryDate;
            delivery2.deliveryPrice = deliveryPrice;
            delivery2.description = description;
            delivery2.userId = userProfile.usrId;
            delivery2.cardId = creditCard.cardId;

            deliveryDao.Create(delivery2);

            category = new Category();
            category.categoryName = categoryName;

            categoryDao.Create(category);

            product = new Product();
            product.productName = productName;
            product.productPrice = productPrice;
            product.productDate = productDate;
            product.productQuantity = productQuantity;
            product.categoryId = category.categoryId;

            productDao.Create(product);
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }

        #endregion Additional test attributes

        /// <summary>
        ///A test for FindByDeliveryId.
        ///</summary>
        [TestMethod]
        public void DAO_FindByDeliveryId()
        {
            int numberDeliveryLines = 3;
            List<DeliveryLine> createdDeliveryLines = new List<DeliveryLine>(numberDeliveryLines);

            for (int i = 0; i < numberDeliveryLines; i++)
            {
                deliveryLine = new DeliveryLine
                {
                    deliveryLineAmount = deliveryLineAmount,
                    deliveryLinePrice = deliveryLinePrice,
                    deliveryId = delivery.deliveryId,
                    productId = product.productId
                };

                deliveryLineDao.Create(deliveryLine);
                createdDeliveryLines.Add(deliveryLine);

                deliveryLine = new DeliveryLine
                {
                    deliveryLineAmount = deliveryLineAmount,
                    deliveryLinePrice = deliveryLinePrice,
                    deliveryId = delivery2.deliveryId,
                    productId = product.productId
                };

                deliveryLineDao.Create(deliveryLine);
            }

            List<DeliveryLine> totalRetrievedDeliveryLines = deliveryLineDao.FindByDeliveryId(delivery.deliveryId);

            Assert.AreEqual(numberDeliveryLines, totalRetrievedDeliveryLines.Count);

            for (int i = 0; i < numberDeliveryLines; i++)
            {
                Assert.AreEqual(totalRetrievedDeliveryLines[i], createdDeliveryLines[i]);
            }
        }
    }
}
