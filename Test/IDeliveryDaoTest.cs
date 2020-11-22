using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.DeliveryDao;
using Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    [TestClass]
    public class IDeliveryDaoTest
    {
        private static IKernel kernel;
        private static IUserProfileDao userProfileDao;
        private static IDeliveryDao deliveryDao;
        private static ICreditCardDao creditCardDao;
        private UserProfile userProfile;
        private UserProfile userProfile2;
        private Delivery delivery;
        private CreditCard creditCard;

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

        private TransactionScope transaction;

        private TestContext testContextInstance;

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

            userProfile2 = new UserProfile();
            userProfile2.loginName = loginName + 2;
            userProfile2.enPassword = password + 2;
            userProfile2.firstName = firstName;
            userProfile2.lastName = lastName;
            userProfile2.email = email;
            userProfile2.language = language;
            userProfile2.country = country;
            userProfile2.role = role;
            userProfile2.address = address;

            userProfileDao.Create(userProfile2);

            creditCard = new CreditCard();

            creditCard.cardType = TYPE;
            creditCard.cardNumber = CARD_NUMBER;
            creditCard.defaultCard = 1;
            creditCard.expirationDate = System.DateTime.Now;
            creditCard.verificationCode = VER_CODE;
            creditCard.userId = userProfile.usrId;

            creditCardDao.Create(creditCard);
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }

        #endregion Additional test attributes

        /// <summary>
        ///A test for FindByUserId.
        ///</summary>
        [TestMethod]
        public void DAO_FindByUserId()
        {
            int numberDeliverys = 7;
            List<Delivery> createdDeliverys = new List<Delivery>(numberDeliverys);

            for (int i = 0; i < numberDeliverys; i++)
            {
                delivery = new Delivery();
                delivery.deliveryAddress = address;
                delivery.deliveryDate = deliveryDate;
                delivery.deliveryPrice = deliveryPrice;
                delivery.description = description + i;
                delivery.userId = userProfile.usrId;
                delivery.cardId = creditCard.cardId;

                deliveryDao.Create(delivery);
                createdDeliverys.Add(delivery);
            }

            delivery = new Delivery();
            delivery.deliveryAddress = address;
            delivery.deliveryDate = deliveryDate;
            delivery.deliveryPrice = deliveryPrice;
            delivery.description = description;
            delivery.userId = userProfile2.usrId;
            delivery.cardId = creditCard.cardId;

            deliveryDao.Create(delivery);

            List<Delivery> totalRetrievedDeliverys = deliveryDao.FindByUserId(userProfile.usrId);

            Assert.AreEqual(numberDeliverys, totalRetrievedDeliverys.Count);

            for (int i = 0; i < numberDeliverys; i++)
            {
                Assert.AreEqual(totalRetrievedDeliverys[i], createdDeliverys[i]);
            }
        }
    }
}
