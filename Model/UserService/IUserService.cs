﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    interface IUserService
    {
        [Inject]
       IUserProfileDao UserProfileDao { set; }

    /// <summary>
    /// Changes the password.
    /// </summary>
    /// <param name="userProfileId"> The user profile id. </param>
    /// <param name="oldClearPassword"> The old clear password. </param>
    /// <param name="newClearPassword"> The new clear password. </param>
    /// <exception cref="IncorrectPasswordException"/>
    /// <exception cref="InstanceNotFoundException"/>
    void ChangePassword(long userProfileId, String oldClearPassword,
        String newClearPassword);

    /// <summary>
    /// Finds the user profile details.
    /// </summary>
    /// <param name="userProfileId"> The user profile id. </param>
    /// <returns> The user profile details </returns>
    /// <exception cref="InstanceNotFoundException"/>
    [Transactional]
    UserProfileDetails FindUserProfileDetails(long userProfileId);
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
    LoginResult Login(String loginName, String password,
        Boolean passwordIsEncrypted);

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="loginName"> Name of the login. </param>
    /// <param name="clearPassword"> The clear password. </param>
    /// <param name="userProfileDetails"> The user profile details. </param>
    /// <exception cref="DuplicateInstanceException"/>
    [Transactional]
    long RegisterUser(String loginName, String clearPassword,
        UserProfileDetails userProfileDetails);

    /// <summary>
    /// Updates the user profile details.
    /// </summary>
    /// <param name="userProfileId"> The user profile id. </param>
    /// <param name="userProfileDetails"> The user profile details. </param>
    /// <exception cref="InstanceNotFoundException"/>
    [Transactional]
    void UpdateUserProfileDetails(long userProfileId,
        UserProfileDetails userProfileDetails);

    /// <summary>
    /// Checks if the specified loginName corresponds to a valid user.
    /// </summary>
    /// <param name="loginName"> User loginName. </param>
    /// <returns> Boolean to indicate if the loginName exists </returns>
    bool UserExists(string loginName);

    /// <summary>
    /// update the data of a creditCard 
    /// </summary>
    /// <param creditCardId="creditCardId">creditCardId</param>
    /// <param creditCardDetails="creditCardDetails">creditCardDetails</param>
    /// <returns> void </returns>
    /// <exception cref="InstanceNotFoundException"/>
    void UpdateCreditCard(long creditCardId, CreditCardDetails creditCardDetails);
    /// <summary>
    /// Add a Credit Card
    /// </summary>
    /// <param creditCardId="creditCardId">creditCardId</param>
    /// <param creditCardDetails="creditCardDetails">creditCardDetails</param>
    /// <returns> void </returns>
    void AddCreditCard(long creditCardId, CreditCardDetails creditCardDetails);
    /// <summary>
    /// update de default creditCard
    /// </summary>
    /// <param creditCardId="creditCardId">creditCardId</param>
    ///  <param userId="userId">userId</param>
    /// <returns> void </returns>
    void AssignDefaultCard(long creditCardId, long userId);
    /// <summary>
    /// Find all creditCards of a user
    /// </summary>
    /// <param userId="userId">userId</param>
    /// <returns>A list of CreditCards</returns>
    List<CreditCard> FindAllCreditCardsDetails(long userID);
}
}
