using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    [TestClass]
    public class ICreditCardDaoTest
    {
        #region Variables

        private static IKernel kernel;
        private static ICreditCardDao creditCardDao;
        private static IUserProfileDao userProfileDao;
        private static List<CreditCard> creditCards;
        private static UserProfile user;

        // Variables used in several tests are initialized here
        private const long CARD_NUMBER = 5231962446920945;
        private const int VER_CODE = 555;
        private const string TYPE = "débito";

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
            creditCardDao = kernel.Get<ICreditCardDao>();
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
                cardNumber = CARD_NUMBER,
                cardType = TYPE,
                expirationDate = DateTime.Now.AddMonths(1),
                userId = user.usrId,
                verificationCode = VER_CODE,
                defaultCard = 1
            };

            creditCardDao.Create(defaultCreditCard);

            creditCards = new List<CreditCard>
            {
                defaultCreditCard
            };

            for (int i = 1; i < 6; i++)
            {
                CreditCard temp = new CreditCard
                {
                    cardNumber = CARD_NUMBER + i,
                    cardType = TYPE,
                    expirationDate = DateTime.Now.AddMonths(1),
                    userId = user.usrId,
                    verificationCode = VER_CODE,
                    defaultCard = 0
                };

                creditCardDao.Create(temp);
                creditCards.Add(temp);
            }
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }

        #endregion Additional test attributes

        [TestMethod()]
        public void DAO_FindByUserIdTest()
        {
            try
            {
                List<CreditCard> retrieved = creditCardDao.FindByUserId(user.usrId);

                Assert.AreEqual(creditCards.Count, retrieved.Count);

                for (int i = 0; i < creditCards.Count; i++)
                {
                    Assert.AreEqual(creditCards[i], retrieved[i]);
                }
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod()]
        public void DAO_FindDefaultUserIdCardTest()
        {
            try
            {
                CreditCard defaultFound = creditCardDao.FindDefaultUserIdCard(user.usrId);

                Assert.AreEqual(creditCards[0], defaultFound);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
