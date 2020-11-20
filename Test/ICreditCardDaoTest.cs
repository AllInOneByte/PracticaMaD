using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    [TestClass]
    public class ICreditCardDaoTest
    {
        private static IKernel kernel;
        private static ICreditCardDao creditCardDao;
        private static CreditCard creditCard;

        // Variables used in several tests are initialized here
        private const long NON_EXISTENT_USER_ID = -1;

        private TransactionScope transaction;

        private TestContext testContextInstance;

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
