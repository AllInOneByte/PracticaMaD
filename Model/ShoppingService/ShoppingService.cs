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
                    deliveryDate = shoppingCart.DeliveryDate,
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
                ShoppingCart shoppingCart = new ShoppingCart(delivery.deliveryDate,
                    delivery.deliveryPrice, delivery.deliveryAddress,
                    delivery.description, delivery.userId, delivery.cardId)
                {
                    DeliveryId = delivery.deliveryId
                };

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
                ShoppingCartDetails shoppingCartDetails = new ShoppingCartDetails(
                    line.deliveryLineAmount, line.deliveryLinePrice, line.productId)
                {
                    DeliveryId = line.deliveryId
                };

                details.Add(shoppingCartDetails);
            }

            return details;
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]

        public ShoppingCart UpdateShoppingCartDetails(ShoppingCart shoppingCart) { 
        
            if ((CreditCardDao.Find(shoppingCart.CardId) != null) {

                ShoppingCart shoppingCart = shoppingCart.Update(shoppingCart);

            }

            return shoppingCart;
        
        }


        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void DeleteShoppingCartDetails(ShoppingCart shoppingCart)
        {
            if ((CreditCardDao.Find(shoppingCart.CardId) != null) {

                shoppingCart.Delete(shoppingCart);

            }

        }
        #endregion IShoppingService members
    }
}
