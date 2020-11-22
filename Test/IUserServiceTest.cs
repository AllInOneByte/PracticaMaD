﻿using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
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
            private const int role = "1";
            private const string address = "n10c3";
            private const long NON_EXISTENT_USER_ID = -1;
            private static IKernel kernel;
            private static IUserService userService;
            private static IUserProfileDao userProfileDao;

            private TransactionScope transaction;

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

                // transaction.Complete() is not called, so Rollback is executed.
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

                    // transaction.Complete() is not called, so Rollback is executed.
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

                    // transaction.Complete() is not called, so Rollback is executed.
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

                    // transaction.Complete() is not called, so Rollback is executed.
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

                    // transaction.Complete() is not called, so Rollback is executed.
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

                    // transaction.Complete() is not called, so Rollback is executed.
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
                            email + "X", "XX", "XX");

                    userService.UpdateUserProfileDetails(userId, expected);

                    var obtained =
                        userService.FindUserProfileDetails(userId);

                    // Check changes
                    Assert.AreEqual(expected, obtained);

                    // transaction.Complete() is not called, so Rollback is executed.
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

                    // transaction.Complete() is not called, so Rollback is executed.
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

                    // transaction.Complete() is not called, so Rollback is executed.
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

                    // transaction.Complete() is not called, so Rollback is executed.
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

                    // transaction.Complete() is not called, so Rollback is executed.
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

                    // transaction.Complete() is not called, so Rollback is executed.
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
}
}
