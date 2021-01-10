using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session
{
    public class ShoppingCartSession
    {
        public List<ShoppingCart> ShoppingCart { get; set; }

        public string Address { get; set; }
    }
}