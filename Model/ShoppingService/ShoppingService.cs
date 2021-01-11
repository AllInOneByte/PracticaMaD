using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao;
using Es.Udc.DotNet.PracticaMaD.Model.DeliveryDao;
using Es.Udc.DotNet.PracticaMaD.Model.DeliveryLineDao;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model;
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

        [Inject]
        public IProductDao ProductDao { private get; set; }


        #region IShoppingService members

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        private void DecreaseProductStock(Product product, int quantity)
        {
            product.productQuantity = product.productQuantity - quantity;

            ProductDao.Update(product);
        }

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="UnmatchingUserAndCardException"/>
        [Transactional]
        public Delivery CreateDelivery(decimal deliveryPrice, long cardNumber, long userId, string description,
            List<ShoppingCart> shoppingCart, string deliveryAddress = null)
        {

            CreditCard card = CreditCardDao.FindByNumber(cardNumber);

            if (CreditCardDao.FindByUserId(userId)
                .Contains(card))
            {
                Delivery delivery = new Delivery
                {
                    deliveryDate = DateTime.Now,
                    deliveryPrice = deliveryPrice,
                    deliveryAddress = deliveryAddress ?? UserProfileDao.Find(userId).address,
                    cardId = card.cardId,
                    userId = userId,
                    description = description
                };

                DeliveryDao.Create(delivery);

                DeliveryLine deliveryLine;
                foreach (ShoppingCart item in shoppingCart)
                {
                    deliveryLine = new DeliveryLine();

                    if (item.Product.productQuantity - item.Amount >= 0)
                    {
                        DecreaseProductStock(item.Product, item.Amount);
                        deliveryLine.deliveryLineAmount = item.Amount;
                    }
                    else
                    {
                        throw new StockEmptyException(item.Product.productId, item.Product.productName);
                    }
                    
                    deliveryLine.deliveryLinePrice = item.Product.productPrice;
                    deliveryLine.productId = item.Product.productId;
                    deliveryLine.deliveryId = delivery.deliveryId;

                    DeliveryLineDao.Create(deliveryLine);
                }

                return delivery;
            }
            else
            {
                throw new UnmatchingUserAndCardException(userId, cardNumber);
            }
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public DeliveryBlock GetAllDeliveries(long userId, int startIndex = 0, int count = 20)
        {
            List<Delivery> deliveries = DeliveryDao.FindByUserId(userId, startIndex, count + 1);
            List<DeliveryDetails> details = new List<DeliveryDetails>();

            bool existMoreDeliveries = (deliveries.Count == count + 1);

            if (existMoreDeliveries) deliveries.RemoveAt(count);

            foreach(Delivery delivery in deliveries)
            {
                details.Add(new DeliveryDetails(delivery.deliveryId, delivery.deliveryDate.ToString("dd/MM/yyyy"),
                    delivery.deliveryPrice, delivery.deliveryAddress, delivery.CreditCard.cardNumber, delivery.description));
            }

            return new DeliveryBlock(details, existMoreDeliveries);
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public DeliveryLineBlock GetDeliveryDetails(long deliveryId, int startIndex = 0, int count = 20)
        {
            List<DeliveryLine> deliveryLines = DeliveryLineDao.FindByDeliveryId(deliveryId, startIndex, count + 1);
            List<DeliveryLineDetails> details = new List<DeliveryLineDetails>();

            bool existMoreDeliveryLines = (deliveryLines.Count == count + 1);

            if (existMoreDeliveryLines) deliveryLines.RemoveAt(count);

            foreach (DeliveryLine line in deliveryLines)
            {
                details.Add(new DeliveryLineDetails(line.deliveryLineAmount, line.deliveryLinePrice, line.Product.productName));
            }

            return new DeliveryLineBlock(details, existMoreDeliveryLines);
        }
        
        public List<ShoppingCart> UpdateShoppingCartDetails(List<ShoppingCart> shoppingCart, long productId, int amount, bool gitf)
        {
            foreach (ShoppingCart item in shoppingCart)
            {
                if (item.Product.productId == productId)
                {
                    if ((item.Product.productQuantity + item.Amount) - amount >= 0)
                    {
                        item.Amount = amount;
                    }
                    else
                    {
                        throw new StockEmptyException(item.Product.productId, item.Product.productName);
                    }

                    return shoppingCart;
                };

            }

            Product product = ProductDao.Find(productId);

            if (product.productQuantity - amount >= 0)
            {
                ShoppingCart shop = new ShoppingCart(amount, product, gitf);

                shoppingCart.Add(shop);

                return shoppingCart;
            }
            else
            {
                throw new StockEmptyException(product.productId, product.productName);
            }
            
        
        }

        
        public List<ShoppingCart> DeleteShoppingCartDetails(List<ShoppingCart> shoppingCart, long productId)
        {
            List<ShoppingCart> shoppingCart_aux = new List<ShoppingCart>();

            foreach (ShoppingCart item in shoppingCart)
            {
                if (item.Product.productId != productId)
                {
                    shoppingCart_aux.Add(item);

                }

            }
            return shoppingCart_aux;

        }
        
        public List<ShoppingCart> ModifyAmountOfItems(List<ShoppingCart> shoppingCart, long productId, int amount)
        {
            
            foreach (ShoppingCart item in shoppingCart)
            {
                if (item.Product.productId == productId) 
                {
                    if (item.Product.productQuantity >= item.Amount + amount)
                    {
                        if(item.Amount + amount >= 0)
                        {
                            item.Amount += amount;
                        }

                    }
                    else
                    {
                        throw new StockEmptyException(item.Product.productId, item.Product.productName);
                    }
                    
      
                };

            }

            return shoppingCart;
        }

        public List<ShoppingCart> ModifyGift(List<ShoppingCart> shoppingCart, long productId, bool gift)
        {
            foreach (ShoppingCart item in shoppingCart)
            {
                if (item.Product.productId == productId)
                {
                    item.Gift = gift;
                };

            }

            return shoppingCart;
        }
        #endregion IShoppingService members
    }
}
