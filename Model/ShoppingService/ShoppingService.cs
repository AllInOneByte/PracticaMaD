using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao;
using Es.Udc.DotNet.PracticaMaD.Model.DeliveryDao;
using Es.Udc.DotNet.PracticaMaD.Model.DeliveryLineDao;
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
        [Transactional]
        public ShoppingCart CreateDelivery(ShoppingCart shoppingCart,
            List<ShoppingCartDetails> shoppingCartDetails)
        {
            if (CreditCardDao.FindByUserId(shoppingCart.UserId)
                .Contains(CreditCardDao.Find(shoppingCart.CardId)))
            {
                Delivery delivery = new Delivery
                {
                    cardId = shoppingCart.CardId,
                    userId = shoppingCart.UserId,
                    deliveryAddress = shoppingCart.DeliveryAddress,
                    deliveyDate = shoppingCart.DeliveryDate,
                    deliveryPrice = shoppingCart.DeliveryPrice,
                    description = shoppingCart.Description
                };

                DeliveryDao.Create(delivery);

                foreach (ShoppingCartDetails item in shoppingCartDetails)
                {
                    DeliveryLine line = new DeliveryLine
                    {
                        deliveryLineAmount = item.DeliveryLineAmount,
                        deliveryLinePrice = item.DeliveryLinePrice,
                        productId = item.ProductId,
                        deliveryId = delivery.deliveryId
                    };

                    DeliveryLineDao.Create(line);
                }

                shoppingCart.DeliveryId = delivery.deliveryId;

                return shoppingCart;
                
            }
            else
            {
                throw new Exception(); /// TODO: make a custom exception like UnmatchingUserAndCardException 
            }
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public List<ShoppingCart> GetAllDeliveries(long userId)
        {
            List<Delivery> deliveries = DeliveryDao.FindByUserId(userId);
            List<ShoppingCart> shoppingCarts = new List<ShoppingCart>();

            foreach (Delivery delivery in deliveries)
            {
                ShoppingCart shoppingCart = new ShoppingCart(delivery.deliveyDate,
                    delivery.deliveryPrice, delivery.deliveryAddress,
                    delivery.description, delivery.userId, delivery.cardId);

                shoppingCart.DeliveryId = delivery.deliveryId;

                shoppingCarts.Add(shoppingCart);
            }

            return shoppingCarts;
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public List<ShoppingCartDetails> GetDeliveryDetails(long deliveryId)
        {
            List<DeliveryLine> lines = DeliveryLineDao.FindByDeliveryId(deliveryId);
            List<ShoppingCartDetails> details = new List<ShoppingCartDetails>();

            foreach (DeliveryLine line in lines)
            {
                details.Add(new ShoppingCartDetails(line.deliveryLineAmount, line.deliveryLinePrice, line.deliveryId, line.productId));
            }

            return details;
        }

        #endregion IShoppingService members
    }
}
