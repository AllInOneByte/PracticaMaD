using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Transactions;
namespace Es.Udc.DotNet.PracticaMaD.Test
{
    [TestClass]
    public class ITagDaoTest
    {
        #region Variables

        private static IKernel kernel;
        private static ITagDao tagDao;
        private static Tag tag;

        // Variables used in several tests are initialized here
        private const String TagName = "tagTest";

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
            tagDao = kernel.Get<ITagDao>();
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
        ///A test for FindAll.
        ///</summary>
        [TestMethod]
        public void DAO_FindAll()
        {
            int numberTags = 5;

            List<Tag> createdTags = new List<Tag>(numberTags);

            for (int i = 0; i < numberTags; i++)
            {
                tag.tagName = TagName+i;
                tagDao.Create(tag);
                createdTags.Add(tag);
            }

            int count = 4;
            int startIndex = 0;

            List<Tag> listTags;
            List<Tag> totalRetrievedTags = new List<Tag>(numberTags);

            /* Get the accounts in blocks of "count" size */
            do
            {
                listTags = tagDao.FindAll();

                totalRetrievedTags.AddRange(listTags);

                Assert.IsTrue(listTags.Count <= count);

                startIndex += count;
            } while (startIndex < numberTags);

            // Check the total number of account retrieved
            Assert.AreEqual(numberTags, totalRetrievedTags.Count);

            // are the accounts retrieved the same than the originals?
            for (int i = 0; i < numberTags; i++)
            {
                Assert.AreEqual(totalRetrievedTags[i], createdTags[i]);
            }
        }

        /// <summary>
        ///A test for FindByName
        ///</summary>
        [TestMethod()]
        public void DAO_FindByNameTest()
        {
            try
            {
                tag.tagName = TagName;
                tagDao.Create(tag);

                Tag actual = tagDao.FindByName(tag.tagName);

                Assert.AreEqual(tag, actual, "User found does not correspond with the original one.");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
