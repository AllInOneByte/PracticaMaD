using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao;
using Es.Udc.DotNet.PracticaMaD.Model.DeliveryDao;
using Es.Udc.DotNet.PracticaMaD.Model.DeliveryLineDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Ninject;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService
{
    public interface IShoppingService
    {
        [Inject]
        IUserProfileDao UserProfileDao { set; }

        [Inject]
        ICreditCardDao CreditCardDao { set; }

        [Inject]
        IDeliveryDao DeliveryDao { set; }

        [Inject]
        IDeliveryLineDao DeliveryLineDao { set; }

        /// <summary>
        /// Creates a new delivery object from the shopping cart.
        /// </summary>
        /// <param name="shoppingCart"> The shopping cart data. </param>
        /// <param name="userId"> The user profile id. </param>
        /// <param name="cardId"> The credit card id. </param>
        /// <returns> The created delivery </returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        ShoppingCart CreateDelivery(ShoppingCart shoppingCart,
            List<ShoppingCartDetails> shoppingCartDetails);

        /// <summary>
        /// Retrieves all the deliveries of the user.
        /// </summary>
        /// <param name="userId"> The user profile id. </param>
        /// <returns> A list with all the deliveries from the user </returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        List<ShoppingCart> GetAllDeliveries(long userId);

        /// <summary>
        /// Gets the details of a specific delivery.
        /// </summary>
        /// <param name="deliveryId"> The delivery id. </param>
        /// <returns> The details of the delivery </returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        List<ShoppingCartDetails> GetDeliveryDetails(long deliveryId);

        /// <summary>
        /// Update a ShoppingCart
        /// </summary>
        /// <param name="shoppingCart"> The ShoppingCart data. </param>
        /// <returns> The details of the shoppingCart </returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        ShoppingCart UpdateShoppingCartDetails( ShoppingCart shoppingCart);

        /// <summary>
        /// Delete a ShoppingCart
        /// </summary>
        /// <param name="shoppingCartId"> The ShoppingCart Id. </param>
        /// <returns> Delete the details of the shoppingCart </returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        void DeleteShoppingCartDetails(ShoppingCart shoppingCart);


    }
}
