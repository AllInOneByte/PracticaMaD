using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Util;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;

namespace Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session
{
    ///<summary>
    ///
    /// A facade utility class to transparently manage session objects and
    /// cookies. The following objects are mantained in the session:
    /// <list>
    ///  <item>* The user's data, stored in the UserSession object under the key
    ///    <c>USER_SESSION_ATTRIBUTE</c>. This attribute is only present
    ///    for authenticated users, and is only of interest to the view. The
    ///    UserSession object stores the firstName and the userProfileId</item>
    ///
    ///  <item>* The user's language and the user's country, stored in the
    ///     Locale object under the key <c>LOCALE_SESSION_ATTRIBUTE</c>. This
    ///     attribute is only present for authenticated users, and is only of
    ///     interest to the view.</item>
    ///</list>
    ///
    /// For users that select "remember my password" in the login wizard, the
    /// following cookies are used (managed in the CookiesManager object):
    /// <list>
    ///   <item>* <c>LOGIN_NAME_COOKIE</c>: to store the login name.</item>
    ///   <item>* <c>ENCRYPTED_PASSWORD_COOKIE</c>: to store the
    ///              encrypted password.</item>
    ///   <item>* <c>.ASPXAUTH</c>: Authentication Cookie.</item>
    /// </list>
    /// In order to make transparent the management of session objects and cookies
    /// to the implementation of controller actions, this class provides a number
    /// of methods equivalent to some of the ones provided by
    /// <c>UserService</c>, which manage session objects and cookies,
    /// and call upon the corresponding <c>UserService's</c> method.
    /// These methods are as follows:
    /// <list>
    ///   <item>* <c>Login</c>.</item>
    ///   <item>* <c>RegisterUser</c>.</item>
    ///   <item>* <c>FindUserProfileDetails</c>.</item>
    ///   <item>* <c>UpdateUserProfileDetails</c>.</item>
    ///   <item>* <c>ChangePassword</c>.</item>
    /// </list>
    ///
    /// It is important to remember that when needing to do some of the above
    /// actions from the controller, the corresponding method of this class
    /// (one of the previous list) must be called, and <b>not</b> the one in
    /// <c>UserService</c>. The rest of methods of <c>UserService</c> must be
    /// called directly.
    ///
    /// For example, in a personalizable portal,<c>UserService</c> could include:
    ///
    ///   <c>findServicePreferences</c>, <c>updateServicePreferences</c>,
    ///   <c>findLayout</c>, <c>updateLayout</c>, etc.
    ///
    /// In a electronic commerce shop <c>UserFacadeDelegate</c> could include:
    ///
    ///   <c>getShoppingCart</c>, <c>addToShoppingCart</c>,
    ///   <c>removeFromShoppingCart</c>, <c>makeOrder</c>,
    ///   <c>findPendingOrders</c>, etc.
    ///
    /// When needing to invoke directly a method of <c>UserSession</c>, the
    /// property <c>SessionManager.UserService</c> must be invoked in order
    /// to get the personal delegate (each user has his/her own delegate).
    ///
    /// Same as <c>UserService</c>, there exist some logical
    /// restrictions with regard to the order of method calling. In particular,
    /// <c>updateUserProfileDetails</c> and <c>changePassword</c>
    /// can not be called if <c>login</c> or <c>registerUser</c>
    /// have not been previously called. After the user calls one of these two
    ///  methods, the user is said to be authenticated.
    /// </summary>
    public class SessionManager
    {
        public static readonly String LOCALE_SESSION_ATTRIBUTE = "locale";

        public static readonly String USER_SESSION_ATTRIBUTE =
               "userSession";

        public static readonly String SHOPPING_CART_SESSION_ATTRIBUTE = "shoppingCartSession";

        private static IUserService userService;

        public IUserService UserService
        {
            set { userService = value; }
        }

        private static IShoppingService shoppingService;

        public IShoppingService ShoppingService
        {
            set { shoppingService = value; }
        }

        private static IProductService productService;

        public IProductService ProductService
        {
            set { productService = value; }
        }

        static SessionManager()
        {
            IIoCManager iocManager =
                (IIoCManager)HttpContext.Current.Application["managerIoC"];

            userService = iocManager.Resolve<IUserService>();
            shoppingService = iocManager.Resolve<IShoppingService>();
            productService = iocManager.Resolve<IProductService>();
        }

        public static void UpdateProduct(long productId, string productName, decimal productPrice, int productQuantity)
        {
            productService.UpdateProductDetails(productId, productName, productPrice, productQuantity);
        }

        public static Product FindProduct(long productId)
        {
            return productService.FindProduct(productId);
        }

        public static void AddToShoppingCart(HttpContext context, long productId, int amount, bool gift)
        {
            ShoppingCartSession shoppingCart = (ShoppingCartSession)context.Session[SHOPPING_CART_SESSION_ATTRIBUTE];

            shoppingCart.ShoppingCart = shoppingService.UpdateShoppingCartDetails(shoppingCart.ShoppingCart, productId, amount, gift);

            UpdateCartSession(context, shoppingCart);
        }

        public static void CreateTag(string tagName)
        {
            productService.AddTag(tagName);
        }
        

        public static TagBlock FindAllTags(int startIndex, int count)
        {
            return productService.FindAllTags(startIndex,count);
        }

        public static List<ShoppingCart> GetShoppingCart(HttpContext context)
        {
            ShoppingCartSession shoppingCart = (ShoppingCartSession)context.Session[SHOPPING_CART_SESSION_ATTRIBUTE];
            return shoppingCart.ShoppingCart;
        }

        public static String GetAddress(HttpContext context)
        {
            ShoppingCartSession shoppingCart = (ShoppingCartSession)context.Session[SHOPPING_CART_SESSION_ATTRIBUTE];
            return shoppingCart.Address;
        }

        public static long GetCreditCardNumber(HttpContext context)
        {
            UserSession userSession =
                (UserSession)context.Session[USER_SESSION_ATTRIBUTE];
            return userSession.CardDefaultNumber;
        }

        public static Boolean CardExists(long number)
        {
            return userService.CardExists(number);
        }

        public static decimal GetTotalPrice(HttpContext context)
        {
            ShoppingCartSession shoppingCart = (ShoppingCartSession)context.Session[SHOPPING_CART_SESSION_ATTRIBUTE];

            decimal price = 0;
            foreach(ShoppingCart line in shoppingCart.ShoppingCart)
            {
                price += line.Product.productPrice * line.Amount;
            }

            return price;
        }
        public static void Buy(HttpContext context, long creditNumber, string description, string address)
        {
           
            UserSession userSession =
                (UserSession)context.Session[USER_SESSION_ATTRIBUTE];

            ShoppingCartSession shoppingCart = (ShoppingCartSession)context.Session[SHOPPING_CART_SESSION_ATTRIBUTE];

            shoppingService.CreateDelivery(GetTotalPrice(context), creditNumber, userSession.UserProfileId, description, shoppingCart.ShoppingCart, address);

            shoppingCart.ShoppingCart = new List<ShoppingCart>();

            UpdateCartSession(context, shoppingCart);
        }

        public static void ForGift(HttpContext context, long productId, bool gitf) 
        {
            ShoppingCartSession shoppingCart = (ShoppingCartSession)context.Session[SHOPPING_CART_SESSION_ATTRIBUTE];

            shoppingCart.ShoppingCart = shoppingService.ModifyGift(shoppingCart.ShoppingCart, productId, gitf);

            UpdateCartSession(context, shoppingCart);
        }

        public static void ModifyAmount(HttpContext context, long productId, int amount)
        {
            ShoppingCartSession shoppingCart = (ShoppingCartSession)context.Session[SHOPPING_CART_SESSION_ATTRIBUTE];

            shoppingCart.ShoppingCart = shoppingService.ModifyAmountOfItems(shoppingCart.ShoppingCart, productId, amount);

            UpdateCartSession(context, shoppingCart);
        }

        public static void DeleteProductOfCart(HttpContext context, long productId)
        {
            ShoppingCartSession shoppingCart = (ShoppingCartSession)context.Session[SHOPPING_CART_SESSION_ATTRIBUTE];

            shoppingCart.ShoppingCart = shoppingService.DeleteShoppingCartDetails(shoppingCart.ShoppingCart, productId);

            UpdateCartSession(context, shoppingCart);
        }


        public static DeliveryBlock GetAllDelevireis(HttpContext context, int startIndex, int count)
        {
            UserSession userSession =
                (UserSession)context.Session[USER_SESSION_ATTRIBUTE];

            return shoppingService.GetAllDeliveries(userSession.UserProfileId, startIndex, count);
        }

        public static DeliveryLineBlock GetAllDeleviryLines(long deliveryId, int startIndex, int count)
        {
            return shoppingService.GetDeliveryDetails(deliveryId, startIndex, count);
        }

        /// <summary>
        /// Registers the creditCard.
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        /// <param name="creditType">The type of the creditCard.</param>
        /// <param name="creditNumber">The number of the creditCard.</param>
        /// <param name="verificationCode">The verification code of the creditCard.</param>
        /// <param name="expirationDate">The expiration date of the creditCard.</param>
        /// <exception cref="DuplicateInstanceException"/>
        public static void RegisterCreditCard(HttpContext context,
            String creditType, long creditNumber, int verificationCode, bool defaultCard, System.DateTime expirationDate)
        {
            UserSession userSession =
                (UserSession)context.Session[USER_SESSION_ATTRIBUTE];

            long id = userService.AddCreditCard(new CreditCardDetails(creditType, creditNumber,verificationCode,expirationDate,0,userSession.UserProfileId));
            if (defaultCard) {
                AssignDefaultCardToUser(context, id, creditNumber);
            }
        }

        public static List<CreditCard> FindAllCredritCards(HttpContext context)
        {
            UserSession userSession =
                (UserSession)context.Session[USER_SESSION_ATTRIBUTE];

            return userService.FindAllCreditCardsDetails(userSession.UserProfileId);
        }

        public static CreditCard FindCreditCard(String creditId)
        {
            long ID = Convert.ToInt64(creditId);

            return userService.FindCreditCardsDetails(ID);
        }

        public static void UpdateCreditCardDetails(HttpContext context, long cardId, Boolean defaultCard, CreditCardDetails creditCardDetails)
        {
          userService.UpdateCreditCard(cardId, creditCardDetails);
            if (defaultCard)
            {
                AssignDefaultCardToUser(context, cardId, creditCardDetails.cardNumber);
            }
            else
            {
                DesAssignDefaultCardToUser(context, cardId);
            }
        }

        public static void AssignDefaultCardToUser(HttpContext context, long cardId, long number)
        {
            UserSession userSession =
                (UserSession)context.Session[USER_SESSION_ATTRIBUTE];

            userSession.CardDefaultId = cardId;
            userSession.CardDefaultNumber = number;

            userService.AssignDefaultCard(cardId, userSession.UserProfileId);
            context.Session.Add(USER_SESSION_ATTRIBUTE, userSession);
        }

        public static void DesAssignDefaultCardToUser(HttpContext context, long cardId)
        {
            UserSession userSession =
                (UserSession)context.Session[USER_SESSION_ATTRIBUTE];
            
            if(userSession.CardDefaultId.Equals(cardId))
            {
                UserSession user = new UserSession();

                user.UserProfileId = userSession.UserProfileId;
                user.FirstName = userSession.FirstName;
                user.Rol = userSession.Rol;
                
                context.Session.Remove(USER_SESSION_ATTRIBUTE);
                context.Session.Add(USER_SESSION_ATTRIBUTE, user);
            }
        }

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        /// <param name="loginName">Username</param>
        /// <param name="clearPassword">Password in clear text</param>
        /// <param name="userProfileDetails">The user profile details.</param>
        /// <exception cref="DuplicateInstanceException"/>
        public static long RegisterUser(HttpContext context,
            String loginName, String clearPassword,
            UserProfileDetails userProfileDetails)
        {
            /* Register user. */
            long usrId = userService.RegisterUser(loginName, clearPassword,
                userProfileDetails);

            /* Insert necessary objects in the session. */
            UserSession userSession = new UserSession();
            userSession.UserProfileId = usrId;
            userSession.Rol = userProfileDetails.role;
            userSession.FirstName = userProfileDetails.firstName;

            Locale locale = new Locale(userProfileDetails.language,
                userProfileDetails.country);

            ShoppingCartSession shoppingCart = (ShoppingCartSession)context.Session[SHOPPING_CART_SESSION_ATTRIBUTE];

            shoppingCart.Address = userProfileDetails.address;

            UpdateSessionForAuthenticatedUser(context, userSession, locale);
            UpdateCartSession(context, shoppingCart);

            FormsAuthentication.SetAuthCookie(loginName, false);

            return usrId;
        }


        /// <summary>
        /// Login method. Authenticates an user in the current context.
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        /// <param name="loginName">Username</param>
        /// <param name="clearPassword">Password in clear text</param>
        /// <param name="rememberMyPassword">Remember password to the next logins</param>
        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        public static void Login(HttpContext context, String loginName,
           String clearPassword, Boolean rememberMyPassword)
        {
            /* Try to login, and if successful, update session with the necessary
             * objects for an authenticated user. */
            LoginResult loginResult = DoLogin(context, loginName,
                clearPassword, false, rememberMyPassword);

            /* Add cookies if requested. */
            if (rememberMyPassword)
            {
                CookiesManager.LeaveCookies(context, loginName,
                    loginResult.EncryptedPassword);
            }
        }

        /// <summary>
        /// Tries to log in with the corresponding method of
        /// <c>UserService</c>, and if successful, inserts in the
        /// session the necessary objects for an authenticated user.
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        /// <param name="loginName">Username</param>
        /// <param name="password">User Password</param>
        /// <param name="passwordIsEncrypted">Password is either encrypted or
        /// in clear text</param>
        /// <param name="rememberMyPassword">Remember password to the next
        /// logins</param>
        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        private static LoginResult DoLogin(HttpContext context,
             String loginName, String password, Boolean passwordIsEncrypted,
             Boolean rememberMyPassword)
        {
            LoginResult loginResult =
                userService.Login(loginName, password,
                    passwordIsEncrypted);

            /* Insert necessary objects in the session. */

            UserSession userSession = new UserSession();
            userSession.UserProfileId = loginResult.UserProfileId;
            userSession.FirstName = loginResult.FirstName;
            userSession.Rol = loginResult.Rol;
            userSession.CardDefaultId = loginResult.CardId;
            userSession.CardDefaultNumber = loginResult.CardNumber;

            ShoppingCartSession shoppingCart = (ShoppingCartSession)context.Session[SHOPPING_CART_SESSION_ATTRIBUTE];

            shoppingCart.Address = loginResult.Addres;

            Locale locale =
                new Locale(loginResult.Language, loginResult.Country);

            UpdateSessionForAuthenticatedUser(context, userSession, locale);
            UpdateCartSession(context, shoppingCart);

            return loginResult;
        }

        /// <summary>
        /// Updates the session values for an previously authenticated user
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        /// <param name="userSession">The user data stored in session.</param>
        /// <param name="locale">The locale info.</param>
        private static void UpdateSessionForAuthenticatedUser(
            HttpContext context, UserSession userSession, Locale locale)
        {
            /* Insert objects in session. */
            context.Session.Add(USER_SESSION_ATTRIBUTE, userSession);
            context.Session.Add(LOCALE_SESSION_ATTRIBUTE, locale);
        }

        /// <summary>
        /// Determine if a user is authenticated
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        /// <returns>
        /// 	<c>true</c> if is user authenticated
        ///     <c>false</c> otherwise
        /// </returns>
        public static Boolean IsUserAuthenticated(HttpContext context)
        {
            if (context.Session == null)
                return false;

            return (context.Session[USER_SESSION_ATTRIBUTE] != null);
        }

        public static Boolean IsAdminAuthenticated(HttpContext context)
        {
            if (context.Session == null)
                return false;

            UserSession userSession = (UserSession)context.Session[USER_SESSION_ATTRIBUTE];
            if ( userSession != null)
            {
                if (userSession.Rol == 1)
                {
                    return true;
                }
            }

            return false;
        }

        public static Locale GetLocale(HttpContext context)
        {
            Locale locale =
                (Locale)context.Session[LOCALE_SESSION_ATTRIBUTE];

            return locale;
        }

        /// <summary>
        /// Updates the user profile details.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="userProfileDetails">The user profile details.</param>
        public static void UpdateUserProfileDetails(HttpContext context,
            UserProfileDetails userProfileDetails)
        {

            ShoppingCartSession shoppingCart =
                (ShoppingCartSession)context.Session[SHOPPING_CART_SESSION_ATTRIBUTE];

            /* Update user's profile details. */

            UserSession userSession =
                (UserSession)context.Session[USER_SESSION_ATTRIBUTE];

            userService.UpdateUserProfileDetails(userSession.UserProfileId,
                userProfileDetails);

            /* Update user's session objects. */

            Locale locale = new Locale(userProfileDetails.language,
                userProfileDetails.country);

            shoppingCart.Address = userProfileDetails.address;

            userSession.FirstName = userProfileDetails.firstName;

            UpdateSessionForAuthenticatedUser(context, userSession, locale);
            UpdateCartSession(context, shoppingCart);
        }

        /// <summary>
        /// Finds the user profile with the id stored in the session.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static UserProfileDetails FindUserProfileDetails(HttpContext context)
        {
            UserSession userSession =
                (UserSession)context.Session[USER_SESSION_ATTRIBUTE];

            
            UserProfileDetails userProfileDetails =
                userService.FindUserProfileDetails(userSession.UserProfileId);
            

            return userProfileDetails;
        }

        /// <summary>
        /// Gets the user info stored in the session.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static UserSession GetUserSession(HttpContext context)
        {
            if (IsUserAuthenticated(context))
                return (UserSession)context.Session[USER_SESSION_ATTRIBUTE];
            else
                return null;
        }

        /// <summary>
        /// Changes the user's password
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        /// <param name="oldClearPassword">The old password in clear text</param>
        /// <param name="newClearPassword">The new password in clear text</param>
        /// <exception cref="IncorrectPasswordException"/>
        public static void ChangePassword(HttpContext context,
               String oldClearPassword, String newClearPassword)
        {
            UserSession userSession =
                (UserSession)context.Session[USER_SESSION_ATTRIBUTE];

            userService.ChangePassword(userSession.UserProfileId,
                oldClearPassword, newClearPassword);

            /* Remove cookies. */
            CookiesManager.RemoveCookies(context);
        }

        /// <summary>
        /// Destroys the session, and removes the cookies if the user had
        /// selected "remember my password".
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        public static void Logout(HttpContext context)
        {
            /* Remove cookies. */
            CookiesManager.RemoveCookies(context);

            /* Invalidate session. */
            context.Session.Abandon();

            /* Invalidate Authentication Ticket */
            FormsAuthentication.SignOut();
        }

        /// <sumary>
        /// Guarantees that the session will have the necessary objects if the
        /// user has been authenticated or had selected "remember my password"
        /// in the past.
        /// </sumary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        public static void TouchSession(HttpContext context)
        {
            /* Check if "UserSession" object is in the session. */
            UserSession userSession = null;

            if (context.Session != null)
            {
                userSession =
                    (UserSession)context.Session[USER_SESSION_ATTRIBUTE];

                // If userSession object is in the session, nothing should be doing.
                if (userSession != null)
                {
                    return;
                }
            }

            /*
             * The user had not been authenticated or his/her session has
             * expired. We need to check if the user has selected "remember my
             * password" in the last login (login name and password will come
             * as cookies). If so, we reconstruct user's session objects.
             */
            UpdateSessionFromCookies(context);
        }

        public static void UpdateCartSession(HttpContext context, ShoppingCartSession shoppingCartSession)
        {
            context.Session.Add(SHOPPING_CART_SESSION_ATTRIBUTE, shoppingCartSession);
        }

        public static void TouchCart(HttpContext context)
        {
            ShoppingCartSession shoppingCartSession = new ShoppingCartSession();
            shoppingCartSession.ShoppingCart = new List<ShoppingCart>();

            UpdateCartSession(context, shoppingCartSession);
        }

        /// <summary>
        /// Tries to login (inserting necessary objects in the session) by using
        /// cookies (if present).
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        private static void UpdateSessionFromCookies(HttpContext context)
        {
            HttpRequest request = context.Request;
            if (request.Cookies == null)
            {
                return;
            }

            /*
             * Check if the login name and the encrypted password come as
             * cookies.
             */
            String loginName = CookiesManager.GetLoginName(context);
            String encryptedPassword = CookiesManager.GetEncryptedPassword(context);

            if ((loginName == null) || (encryptedPassword == null))
            {
                return;
            }

            /* If loginName and encryptedPassword have valid values (the user selected "remember
             * my password" option) try to login, and if successful, update session with the
             * necessary objects for an authenticated user.
             */
            try
            {
                DoLogin(context, loginName, encryptedPassword, true, true);

                /* Authentication Ticket. */
                FormsAuthentication.SetAuthCookie(loginName, true);
            }
            catch (Exception)
            { // Incorrect loginName or encryptedPassword
                return;
            }
        }
    }
}