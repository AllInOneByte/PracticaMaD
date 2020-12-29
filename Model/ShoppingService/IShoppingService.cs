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
        /// Creates a new delivery object in the database.
        /// </summary>
        /// <param name="delivery"> The delivery data. </param>
        /// <param name="deliveryLines"> The delivery lines. </param>
        /// <returns> The created delivery. </returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        Delivery CreateDelivery(Delivery delivery, List<DeliveryLine> deliveryLines);

        /// <summary>
        /// Retrieves all the deliveries of the user.
        /// </summary>
        /// <param name="userId"> The user profile id. </param>
        /// <param name="startIndex"> The index at which the deliveries list must start. </param>
        /// <param name="count"> The maximum number of deliveries that must return the function. </param>
        /// <returns> A list with all the deliveries from the user. </returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        List<Delivery> GetAllDeliveries(long userId, int startIndex = 0, int count = 20);

        /// <summary>
        /// Gets the details of a specific delivery.
        /// </summary>
        /// <param name="deliveryId"> The delivery id. </param>
        /// <param name = "startIndex" > The index at which the delivery lines list must start. </param>
        /// <param name="count"> The maximum number of delivery lines that must return the function. </param>
        /// <returns> The details of the delivery. </returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        List<DeliveryLine> GetDeliveryDetails(long deliveryId, int startIndex = 0, int count = 20);
    }
}
