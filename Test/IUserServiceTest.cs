using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Util;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao;
using Es.Udc.DotNet.PracticaMaD.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Transactions;


namespace Es.Udc.DotNet.PracticaMaD.Test
{
    [TestClass()]
    public class IUserServiceTest
        {
            // Variables used in several tests are initialized here
            private const string loginName = "loginNameTest";

            private const string clearPassword = "password";
            private const string firstName = "name";
            private const string lastName = "lastName";
            private const string email = "user@udc.es";
            private const string language = "es";
            private const string country = "ES";
            private const byte role = 1;
            private const string address = "n10c3";
            private const long NON_EXISTENT_USER_ID = -1;

            private const string cardType = "credito";
            private const long cardNumber = 12345677;
            private const int verificationCode = 123;
            private System.DateTime expirationDate = System.DateTime.Now;
            private const byte defaultCard = 0;

        private static IKernel kernel;
            private static IUserService userService;
            private static IUserProfileDao userProfileDao;
            private static ICreditCardDao creditCardDao;

            /// <summary>
            /// Gets or sets the test context which provides information about and functionality for the
            /// current test run.
            /// </summary>
            public TestContext TestContext { get; set; }

            /// <summary>
            /// A test for RegisterUser
            /// </summary>
            [TestMethod]
            public void RegisterUserTest()
            {
                using (var scope = new TransactionScope())
                {
                    // Register user and find profile
                    var userId =
                        userService.RegisterUser(loginName, clearPassword,
                            new UserProfileDetails(firstName, lastName, email, language, country, role, address));

                    var userProfile = userProfileDao.Find(userId);

                    // Check data
                    Assert.AreEqual(userId, userProfile.usrId);
                    Assert.AreEqual(loginName, userProfile.loginName);
                    Assert.AreEqual(PasswordEncrypter.Crypt(clearPassword), userProfile.enPassword);
                    Assert.AreEqual(firstName, userProfile.firstName);
                    Assert.AreEqual(lastName, userProfile.lastName);
                    Assert.AreEqual(email, userProfile.email);
                    Assert.AreEqual(language, userProfile.language);
                    Assert.AreEqual(country, userProfile.country);
                    Assert.AreEqual(role, userProfile.role);
                    Assert.AreEqual(address, userProfile.address);

                }
            }

            /// <summary>
            /// A test for registering a user that already exists in the database
            /// </summary>
            [TestMethod]
            [ExpectedException(typeof(DuplicateInstanceException))]
            public void RegisterDuplicatedUserTest()
            {
                using (var scope = new TransactionScope())
                {
                    // Register user
                    userService.RegisterUser(loginName, clearPassword,
                        new UserProfileDetails(firstName, lastName, email, language, country, role, address));

                    // Register the same user
                    userService.RegisterUser(loginName, clearPassword,
                        new UserProfileDetails(firstName, lastName, email, language, country, role, address));

                }
            }

            ///// <summary>
            /////A test for Login with clear password
            /////</summary>
            [TestMethod]
            public void LoginClearPasswordTest()
            {
                using (var scope = new TransactionScope())
                {
                    // Register user
                    var userId = userService.RegisterUser(loginName, clearPassword,
                        new UserProfileDetails(firstName, lastName, email, language, country, role, address));

                    var expected = new LoginResult(userId, firstName,
                        PasswordEncrypter.Crypt(clearPassword), language, country, role, address);

                    // Login with clear password
                    var actual =
                        userService.Login(loginName,
                            clearPassword, false);

                    // Check data
                    Assert.AreEqual(expected, actual);
                }
            }

            ///// <summary>
            /////A test for Login with encrypted password
            /////</summary>
            [TestMethod]
            public void LoginEncryptedPasswordTest()
            {
                using (var scope = new TransactionScope())
                {
                    // Register user
                    var userId = userService.RegisterUser(loginName, clearPassword,
                        new UserProfileDetails(firstName, lastName, email, language, country, role, address));

                    var expected = new LoginResult(userId, firstName,
                        PasswordEncrypter.Crypt(clearPassword), language, country, role, address);

                    // Login with encrypted password
                    var obtained =
                        userService.Login(loginName,
                            PasswordEncrypter.Crypt(clearPassword), true);

                    // Check data
                    Assert.AreEqual(expected, obtained);

                }
            }

            ///// <summary>
            /////A test for Login with incorrect password
            /////</summary>
            [TestMethod]
            [ExpectedException(typeof(IncorrectPasswordException))]
            public void LoginIncorrectPasswordTest()
            {
                using (var scope = new TransactionScope())
                {
                    // Register user
                    var userId = userService.RegisterUser(loginName, clearPassword,
                        new UserProfileDetails(firstName, lastName, email, language, country, role, address));

                    // Login with incorrect (clear) password
                    var actual =
                        userService.Login(loginName, clearPassword + "X", false);

                }
            }

            ///// <summary>
            /////A test for Login with a non-existing user
            /////</summary>
            [TestMethod]
            [ExpectedException(typeof(InstanceNotFoundException))]
            public void LoginNonExistingUserTest()
            {
                // Login for a user that has not been registered
                var actual =
                    userService.Login(loginName, clearPassword, false);
            }

            /// <summary>
            /// A test for FindUserProfileDetails
            /// </summary>
            [TestMethod]
            public void FindUserProfileDetailsTest()
            {
                using (var scope = new TransactionScope())
                {
                    var expected =
                        new UserProfileDetails(firstName, lastName, email, language, country, role, address);

                    var userId =
                        userService.RegisterUser(loginName, clearPassword, expected);

                    var obtained =
                        userService.FindUserProfileDetails(userId);

                    // Check data
                    Assert.AreEqual(expected, obtained);

                }
            }

            /// <summary>
            /// A test for FindUserProfileDetails when the user does not exist
            /// </summary>
            [TestMethod]
            [ExpectedException(typeof(InstanceNotFoundException))]
            public void FindUserProfileDetailsForNonExistingUserTest()
            {
                userService.FindUserProfileDetails(NON_EXISTENT_USER_ID);
            }

            /// <summary>
            /// A test for UpdateUserProfileDetails
            /// </summary>
            [TestMethod]
            public void UpdateUserProfileDetailsTest()
            {
                using (var scope = new TransactionScope())
                {
                    // Register user and update profile details
                    var userId = userService.RegisterUser(loginName, clearPassword,
                        new UserProfileDetails(firstName, lastName, email, language, country, role, address));

                    var expected =
                        new UserProfileDetails(firstName + "X", lastName + "X",
                            email + "X", "XX", "XX", 0, "XX");

                    userService.UpdateUserProfileDetails(userId, expected);

                    var obtained =
                        userService.FindUserProfileDetails(userId);

                    // Check changes
                    Assert.AreEqual(expected, obtained);

                }
            }

            /// <summary>
            /// A test for UpdateUserProfileDetails when the user does not exist
            /// </summary>
            [TestMethod]
            [ExpectedException(typeof(InstanceNotFoundException))]
            public void UpdateUserProfileDetailsForNonExistingUserTest()
            {
                using (var scope = new TransactionScope())
                {
                    userService.UpdateUserProfileDetails(NON_EXISTENT_USER_ID,
                        new UserProfileDetails(firstName, lastName, email, language, country, role, address));
                }
            }

            /// <summary>
            /// A test for ChangePassword
            /// </summary>
            [TestMethod]
            public void ChangePasswordTest()
            {
                using (var scope = new TransactionScope())
                {
                    // Register user
                    var userId = userService.RegisterUser(loginName, clearPassword,
                        new UserProfileDetails(firstName, lastName, email, language, country, role, address));

                    // Change password
                    var newClearPassword = clearPassword + "X";
                    userService.ChangePassword(userId, clearPassword, newClearPassword);

                    // Try to login with the new password. If the login is correct, then the password
                    // was successfully changed.
                    userService.Login(loginName, newClearPassword, false);
                }
            }

            /// <summary>
            /// A test for ChangePassword entering a wrong old password
            /// </summary>
            [TestMethod]
            [ExpectedException(typeof(IncorrectPasswordException))]
            public void ChangePasswordWithIncorrectPasswordTest()
            {
                using (var scope = new TransactionScope())
                {
                    // Register user
                    var userId = userService.RegisterUser(loginName, clearPassword,
                        new UserProfileDetails(firstName, lastName, email, language, country, role, address));

                    // Change password
                    var newClearPassword = clearPassword + "X";
                    userService.ChangePassword(userId, clearPassword + "Y", newClearPassword);
                
                }
            }

            /// <summary>
            /// A test for ChangePassword when the user does not exist
            /// </summary>
            [TestMethod]
            [ExpectedException(typeof(InstanceNotFoundException))]
            public void ChangePasswordForNonExistingUserTest()
            {
                userService.ChangePassword(NON_EXISTENT_USER_ID,
                    clearPassword, clearPassword + "X");
            }

            /// <summary>
            /// A test to check if a valid user loginName is found
            /// </summary>
            [TestMethod]
            public void UserExistsForValidUser()
            {
                using (var scope = new TransactionScope())
                {
                    // Register user
                    userService.RegisterUser(loginName, clearPassword,
                        new UserProfileDetails(firstName, lastName, email, language, country, role, address));

                    bool userExists = userService.UserExists(loginName);

                    Assert.IsTrue(userExists);
                }
            }

            /// <summary>
            /// A test to check if a not valid user loginame is found
            /// </summary>
            [TestMethod]
            public void UserExistsForNotValidUser()
            {
                using (var scope = new TransactionScope())
                {
                    String invalidLoginName = loginName + "_someFakeUserSuffix";

                    bool userExists = userService.UserExists(invalidLoginName);

                    Assert.IsFalse(userExists);
                }
            }

        /// <summary>
        /// A test to AddCreditCard
        /// </summary>
        [TestMethod]
        public void AddCreditCardTest()
        {
            using (var scope = new TransactionScope())
            {
                var user = new UserProfile
                {
                    loginName = loginName,
                    enPassword = clearPassword,
                    firstName = firstName,
                    lastName = lastName,
                    email = email,
                    language = language,
                    country = country,
                    role = 1,
                    address = address
                };
                userProfileDao.Create(user);

                var creditCardId = userService.AddCreditCard(new CreditCardDetails(cardType, cardNumber, verificationCode, expirationDate, defaultCard, user.usrId));

                var creditCard = creditCardDao.Find(creditCardId);

                // Check data
                Assert.AreEqual(cardType, creditCard.cardType);
                Assert.AreEqual(cardNumber, creditCard.cardNumber);
                Assert.AreEqual(defaultCard, creditCard.defaultCard);
                Assert.AreEqual(expirationDate.Date, creditCard.expirationDate.Date);
                Assert.AreEqual(verificationCode, creditCard.verificationCode);
                Assert.AreEqual(user.usrId, creditCard.userId);
            }
        }

                /// <summary>
                /// A test to UpdateCreditCard
                /// </summary>
        [TestMethod]
        public void UpdateCreditCardTest()
        {
            using (var scope = new TransactionScope())
            {
                var user = new UserProfile
                {
                    loginName = loginName,
                    enPassword = clearPassword,
                    firstName = firstName,
                    lastName = lastName,
                    email = email,
                    language = language,
                    country = country,
                    role = 1,
                    address = address
                };
                userProfileDao.Create(user);

                var creditCardId = userService.AddCreditCard(new CreditCardDetails(cardType, cardNumber, verificationCode, expirationDate, defaultCard, user.usrId));

                var creditCard = creditCardDao.Find(creditCardId);

                // Check data
                Assert.AreEqual(cardType, creditCard.cardType);
                Assert.AreEqual(cardNumber, creditCard.cardNumber);
                Assert.AreEqual(defaultCard, creditCard.defaultCard);
                Assert.AreEqual(expirationDate.Date, creditCard.expirationDate.Date);
                Assert.AreEqual(verificationCode, creditCard.verificationCode);
                Assert.AreEqual(user.usrId, creditCard.userId);

                userService.UpdateCreditCard(creditCardId,
                    new CreditCardDetails("debito", cardNumber, verificationCode, expirationDate, defaultCard, user.usrId));

                creditCard = creditCardDao.Find(creditCardId);

                // Check data
                Assert.AreEqual("debito", creditCard.cardType);
                Assert.AreEqual(cardNumber, creditCard.cardNumber);
                Assert.AreEqual(defaultCard, creditCard.defaultCard);
                Assert.AreEqual(expirationDate.Date, creditCard.expirationDate.Date);
                Assert.AreEqual(verificationCode, creditCard.verificationCode);
                Assert.AreEqual(user.usrId, creditCard.userId);
            }
        }

        /// <summary>
        /// A test to AssignDefaultCard
        /// </summary>
        [TestMethod]
        public void AssignDefaultCardTest()
        {
            using (var scope = new TransactionScope())
            {
                var user = new UserProfile
                {
                    loginName = loginName,
                    enPassword = clearPassword,
                    firstName = firstName,
                    lastName = lastName,
                    email = email,
                    language = language,
                    country = country,
                    role = 1,
                    address = address
                };
                userProfileDao.Create(user);

                var creditCardId1 = userService.AddCreditCard(new CreditCardDetails(cardType, cardNumber, verificationCode, expirationDate, defaultCard, user.usrId));
                var creditCardId2 = userService.AddCreditCard(new CreditCardDetails(cardType, 12345678, verificationCode, expirationDate, 1, user.usrId));

                var creditCard1 = creditCardDao.Find(creditCardId1);
                var creditCard2 = creditCardDao.Find(creditCardId2);

                Assert.AreEqual(0, creditCard1.defaultCard);
                Assert.AreEqual(1, creditCard2.defaultCard);

                userService.AssignDefaultCard(creditCardId1, user.usrId);

                creditCard1 = creditCardDao.Find(creditCardId1);
                creditCard2 = creditCardDao.Find(creditCardId2);

                Assert.AreEqual(1, creditCard1.defaultCard);
                Assert.AreEqual(0, creditCard2.defaultCard);
            }
        }

        /// <summary>
        /// A test to FindAllCreditCardsDetails
        /// </summary>
        [TestMethod]
        public void FindAllCreditCardsDetailsTest()
        {
            using (var scope = new TransactionScope())
            {
                var user = new UserProfile
                {
                    loginName = loginName,
                    enPassword = clearPassword,
                    firstName = firstName,
                    lastName = lastName,
                    email = email,
                    language = language,
                    country = country,
                    role = 1,
                    address = address
                };
                userProfileDao.Create(user);

                int numberCard = 3;
                List<CreditCard> cards = new List<CreditCard>(numberCard);

                for (int i = 0; i < numberCard; i++)
                {
                    var creditCardId = userService.AddCreditCard(new CreditCardDetails(cardType, cardNumber+i, verificationCode, expirationDate, defaultCard, user.usrId));
                    cards.Add(creditCardDao.Find(creditCardId));
                }

                List<CreditCard> retriveFoundCards = userService.FindAllCreditCardsDetails(user.usrId);

                // Check data
                Assert.AreEqual(numberCard, retriveFoundCards.Count);
                for (int i = 0; i < numberCard; i++)
                {
                    Assert.AreEqual(cards[i].cardType, retriveFoundCards[i].cardType);
                    Assert.AreEqual(cards[i].cardNumber, retriveFoundCards[i].cardNumber);
                    Assert.AreEqual(cards[i].defaultCard, retriveFoundCards[i].defaultCard);
                    Assert.AreEqual(cards[i].expirationDate.Date, retriveFoundCards[i].expirationDate.Date);
                    Assert.AreEqual(cards[i].verificationCode, retriveFoundCards[i].verificationCode);
                }
            }
        }

        /// <summary>
        /// A test to FindCreditCardsDetails
        /// </summary>
        [TestMethod]
        public void FindCreditCardsDetailsTest()
        {
            using (var scope = new TransactionScope())
            {
                var user = new UserProfile
                {
                    loginName = loginName,
                    enPassword = clearPassword,
                    firstName = firstName,
                    lastName = lastName,
                    email = email,
                    language = language,
                    country = country,
                    role = 1,
                    address = address
                };
                userProfileDao.Create(user);
                
                var creditCardId = userService.AddCreditCard(new CreditCardDetails(cardType, cardNumber, verificationCode, expirationDate, defaultCard, user.usrId));


                CreditCard retriveFoundCard = userService.FindCreditCardsDetails(creditCardId);

                Assert.AreEqual(cardType, retriveFoundCard.cardType);
                Assert.AreEqual(cardNumber, retriveFoundCard.cardNumber);
                Assert.AreEqual(defaultCard, retriveFoundCard.defaultCard);
                Assert.AreEqual(expirationDate.Date, retriveFoundCard.expirationDate.Date);
                Assert.AreEqual(verificationCode, retriveFoundCard.verificationCode);

            }
        }

        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
            public static void MyClassInitialize(TestContext testContext)
            {
                kernel = TestManager.ConfigureNInjectKernel();

                userProfileDao = kernel.Get<IUserProfileDao>();
                userService = kernel.Get<IUserService>();
                creditCardDao = kernel.Get<ICreditCardDao>();
            }

            //Use ClassCleanup to run code after all tests in a class have run
            [ClassCleanup]
            public static void MyClassCleanup()
            {
                TestManager.ClearNInjectKernel(kernel);
            }

            //Use TestInitialize to run code before running each test
            [TestInitialize]
            public void MyTestInitialize()
            {
            }

            //Use TestCleanup to run code after each test has run
            [TestCleanup]
            public void MyTestCleanup()
            {
            }

            #endregion Additional test attributes
    }
}
