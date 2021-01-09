using System;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session
{
    public class ShoppingCartSession
    {

        private List<ShoppingCart> shoppingCart;
        private string address;

        public List<ShoppingCart> ShoppingCart
        {
            get { return shoppingCart; }
            set { shoppingCart = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }
    }
}