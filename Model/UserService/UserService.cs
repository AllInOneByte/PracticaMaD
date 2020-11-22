﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Util;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    class UserService : IUserService {
        private object creditCard;

        [Inject]
        public IUserProfileDao UserProfileDao { private get; set; }

        [Inject]

        public ICreditCardDao creditcardDao { private get; set; }
        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="userProfileId"> The user profile id. </param>
        /// <param name="oldClearPassword"> The old clear password. </param>
        /// <param name="newClearPassword"> The new clear password. </param>
        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        public void ChangePassword(long userProfileId, string oldClearPassword,
            string newClearPassword) {
            UserProfile userProfile = UserProfileDao.Find(userProfileId);
            String storedPassword = userProfile.enPassword;

            if (!PasswordEncrypter.IsClearPasswordCorrect(oldClearPassword,
                 storedPassword))
            {
                throw new IncorrectPasswordException(userProfile.loginName);
            }

            userProfile.enPassword =
            PasswordEncrypter.Crypt(newClearPassword);

            UserProfileDao.Update(userProfile);

        }
        /// <summary>
        /// Finds the user profile details.
        /// </summary>
        /// <param name="userProfileId"> The user profile id. </param>
        /// <returns> The user profile details </returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public UserProfileDetails FindUserProfileDetails(long userProfileId) {
            UserProfile userProfile = UserProfileDao.Find(userProfileId);

            UserProfileDetails userProfileDetails =
                new UserProfileDetails(userProfile.firstName,
                    userProfile.lastName, userProfile.email,
                    userProfile.language, userProfile.country,
                    userProfile.role, userProfile.address);

            return userProfileDetails;

        }
        /// <summary>
        /// Logins the specified login name.
        /// </summary>
        /// <param name="loginName"> Name of the login. </param>
        /// <param name="password"> The password. </param>
        /// <param name="passwordIsEncrypted"> if set to <c> true </c> [password is encrypted]. </param>
        /// <returns> LoginResult </returns>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="IncorrectPasswordException"/>
        [Transactional]
        public LoginResult Login(String loginName, String password,
            Boolean passwordIsEncrypted) {
            UserProfile userProfile =
                      UserProfileDao.FindByLoginName(loginName);

            String storedPassword = userProfile.enPassword;

            if (passwordIsEncrypted)
            {
                if (!password.Equals(storedPassword))
                {
                    throw new IncorrectPasswordException(loginName);
                }
            }
            else
            {
                if (!PasswordEncrypter.IsClearPasswordCorrect(password,
                        storedPassword))
                {
                    throw new IncorrectPasswordException(loginName);
                }
            }

            return new LoginResult(userProfile.usrId, userProfile.firstName,
                storedPassword, userProfile.language, userProfile.country);
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="loginName"> Name of the login. </param>
        /// <param name="clearPassword"> The clear password. </param>
        /// <param name="userProfileDetails"> The user profile details. </param>
        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        public long RegisterUser(String loginName, String clearPassword,
            UserProfileDetails userProfileDetails)
        {
            try
            {
                UserProfileDao.FindByLoginName(loginName);

                throw new DuplicateInstanceException(loginName,
                    typeof(UserProfile).FullName);
            }
            catch (InstanceNotFoundException)
            {
                String encryptedPassword = PasswordEncrypter.Crypt(clearPassword);

                UserProfile userProfile = new UserProfile();

                userProfile.loginName = loginName;
                userProfile.enPassword = encryptedPassword;
                userProfile.firstName = userProfileDetails.firstName;
                userProfile.lastName = userProfileDetails.lastName;
                userProfile.email = userProfileDetails.email;
                userProfile.language = userProfileDetails.language;
                userProfile.country = userProfileDetails.country;
                userProfile.role = userProfileDetails.role;
                userProfile.address = userProfileDetails.address;
                UserProfileDao.Create(userProfile);

                return userProfile.usrId;
            }
        }

            /// <summary>
            /// Updates the user profile details.
            /// </summary>
            /// <param name="userProfileId"> The user profile id. </param>
            /// <param name="userProfileDetails"> The user profile details. </param>
            /// <exception cref="InstanceNotFoundException"/>
            [Transactional]
            public void UpdateUserProfileDetails(long userProfileId,
                UserProfileDetails userProfileDetails)
            {

                String encryptedPassword = PasswordEncrypter.Crypt(clearPassword);

                UserProfile userProfile =
                      UserProfileDao.Find(userProfileId);

                userProfile.loginName = loginName;
                userProfile.enPassword = encryptedPassword;
                userProfile.firstName = userProfileDetails.firstName;
                userProfile.lastName = userProfileDetails.lastName;
                userProfile.email = userProfileDetails.email;
                userProfile.language = userProfileDetails.language;
                userProfile.country = userProfileDetails.country;
                userProfile.role = userProfileDetails.role;
                userProfile.address = userProfileDetails.address;
                UserProfileDao.Update(userProfile);
            }

            /// <summary>
            /// Checks if the specified loginName corresponds to a valid user.
            /// </summary>
            /// <param name="loginName"> User loginName. </param>
            /// <returns> Boolean to indicate if the loginName exists </returns>
            public bool UserExists(string loginName) {
                try
                {
                    UserProfile userProfile = UserProfileDao.FindByLoginName(loginName);
                }
                catch (InstanceNotFoundException e)
                {
                    return false;
                }

                return true;
            }

            /// <summary>
            /// update the data of a creditCard 
            /// </summary>
            /// <param creditCardId="creditCardId">creditCardId</param>
            /// <param creditCardDetails="creditCardDetails">creditCardDetails</param>
            /// <returns> void </returns>
            /// <exception cref="InstanceNotFoundException"/>
            public void UpdateCreditCard(long creditCardId, CreditCardDetails creditCardDetails) {

                CreditCard creditCard =
                          CreditCardDao.find(creditCardId);

                creditCard.cardType = creditCardDetails.cardType;
                creditCard.cardNumber = creditCardDetails.cardNumber;
                creditCard.verificationCode = creditCardDetails.verificationCode;
                creditCard.expirationDate = creditCardDetails.ExpirationDate;
                creditCard.defaultCard = creditCardDetails.DefaultCard;
                creditCard.userId = creditCardDetails.userId;
                CreditCardDao.update(creditCard);

            }
        /// <summary>
        /// Add a Credit Card
        /// </summary>
        /// <param creditCardId="creditCardId">creditCardId</param>
        /// <param creditCardDetails="creditCardDetails">creditCardDetails</param>
        /// <returns> void </returns>
        public void AddCreditCard(long creditCardId, CreditCardDetails creditCardDetails) {
            CreditCard creditCard = new CreditCard();
             try
            {
                creditCard = CreditCardDao.Find(creditCardId);

                throw new DuplicateInstanceException(creditCardDetails.cardNumber,
                    typeof(CreditCard).FullName);
            }
            catch (InstanceNotFoundException)
            {

                creditCard.cardType = creditCardDetails.CardType;
                creditCard.cardNumber = creditCardDetails.cardNumber;
                creditCard.verificationCode = creditCardDetails.VerificationCode;
                creditCard.expirationDate = creditCardDetails.ExpirationDate;
                creditCard.defaultCard = creditCardDetails.DefaultCard;
                creditCard.userId = creditCardDetails.userId;
                creditCardDao.Create(creditCard);
            }
        }
        /// <summary>
        /// update de default creditCard
        /// </summary>
        /// <param creditCardId="creditCardId">creditCardId</param>
        ///  <param userId="userId">userId</param>
        /// <returns> void </returns>
        public void AssignDefaultCard(long creditCardId, long userId) {
            try { 
                creditCard = CreditCardDao.Find(creditCardId);

                throw new InstanceNotFoundException(creditCardId,
                     typeof(creditCard).FullName);

                } catch (InstanceNotFoundException)
                try { 
                CreditCard creditCard2 = CreditCardDao.FindDefaultUserIdCard(userId);
                creditCard2.defaultCard = 0;
                CreditCardDao.Update(creditCard2);
                        
                } catch (InstanceNotFoundException)
                creditCard.defaultCard = 1;
                CreditCardDao.Update(CreditCard);
            }

        }
        
        /// <summary>
        /// Find all creditCards of a user
        /// </summary>
        /// <param userId="userId">userId</param>
        /// <returns>A list of CreditCards</returns>
        public List<CreditCard> FindAllCreditCardsDetails(long userID) {
                   
        List<CreditCard> creditsCards = creditcardDao.FindByUserId(userID);

        return creditsCards;
        }
        
    }
}
   


