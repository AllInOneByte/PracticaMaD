using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao;
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
        private static IKernel kernel;
        private static ICreditCardDao creditCardDao;
        private static List<CreditCard> creditCards;

        // Variables used in several tests are initialized here
        private const long CARD_NUMBER = 5231962446920945;

        private TransactionScope transaction;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();
            creditCardDao = kernel.Get<ICreditCardDao>();
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

            CreditCard defaultCreditCard = new CreditCard
            {
                cardNumber = CARD_NUMBER, // hay que hacer que acepte long en vez de int
                cardType = "plastic????", // no idea what this is
                expirationDate = DateTime.Now.AddMonths(1),
                userId = 1,
                verificationCode = 1 // no idea what is this either, gotta ask
            };
            // pendiente de guardar en bd y crear el resto de tarjetas
            creditCards.Add(defaultCreditCard);
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }

        #endregion Additional test attributes

        [TestMethod()]
        public void DAO_FindByUserId()
        {
            try
            {
                //TODO
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod()]
        public void DAO_FindDefaultUserIdCard()
        {
            try
            {
                //TODO
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
