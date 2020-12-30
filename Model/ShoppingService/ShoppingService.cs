using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao;
using Es.Udc.DotNet.PracticaMaD.Model.DeliveryDao;
using Es.Udc.DotNet.PracticaMaD.Model.DeliveryLineDao;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Ninject;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService
{
    public class ShoppingService : IShoppingService
    {
        [Inject]
        public IUserProfileDao UserProfileDao { private get; set; }

        [Inject]
        public ICreditCardDao CreditCardDao { private get; set; }

        [Inject]
        public IDeliveryDao DeliveryDao { private get; set; }

        [Inject]
        public IDeliveryLineDao DeliveryLineDao { private get; set; }

        #region IShoppingService members

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="UnmatchingUserAndCardException"/>
        [Transactional]
        public Delivery CreateDelivery(decimal deliveryPrice, long cardId, long userId, string description,
            List<DeliveryLine> deliveryLines, string deliveryAddress = null)
        {
            if (CreditCardDao.FindByUserId(userId)
                .Contains(CreditCardDao.Find(cardId)))
            {
                Delivery delivery = new Delivery
                {
                    deliveryDate = DateTime.Now,
                    deliveryPrice = deliveryPrice,
                    deliveryAddress = deliveryAddress ?? UserProfileDao.Find(userId).address,
                    cardId = cardId,
                    userId = userId
                };

                DeliveryDao.Create(delivery);

                foreach (DeliveryLine item in deliveryLines)
                {
                    item.deliveryId = delivery.deliveryId;

                    DeliveryLineDao.Create(item);
                }

                return delivery;
            }
            else
            {
                throw new UnmatchingUserAndCardException(userId, cardId);
            }
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public DeliveryBlock GetAllDeliveries(long userId, int startIndex = 0, int count = 20)
        {
            List<Delivery> deliveries = DeliveryDao.FindByUserId(userId, startIndex, count + 1);

            bool existMoreDeliveries = (deliveries.Count == count + 1);

            if (existMoreDeliveries) deliveries.RemoveAt(count);

            return new DeliveryBlock(deliveries, existMoreDeliveries);
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public DeliveryLineBlock GetDeliveryDetails(long deliveryId, int startIndex = 0, int count = 20)
        {
            List<DeliveryLine> deliveryLines = DeliveryLineDao.FindByDeliveryId(deliveryId, startIndex, count + 1);

            bool existMoreDeliveryLines = (deliveryLines.Count == count + 1);

            if (existMoreDeliveryLines) deliveryLines.RemoveAt(count);

            return new DeliveryLineBlock(deliveryLines, existMoreDeliveryLines);
        }

        #endregion IShoppingService members
    }
}
