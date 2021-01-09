using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService
{
    public class DeliveryLineDetails
    {
        public int DeliveryLineAmount { get; set; }
        public decimal DeliveryLinePrice { get; set; }
        public string ProductName { get; set; }

        public DeliveryLineDetails(int deliveryLineAmount, decimal deliveryLinePrice, string productName)
        {
            this.DeliveryLineAmount = deliveryLineAmount;
            this.DeliveryLinePrice = deliveryLinePrice;
            this.ProductName = productName;
        }

        public override bool Equals(object obj)
        {
            var details = obj as DeliveryLineDetails;
            return details != null &&
                   DeliveryLineAmount == details.DeliveryLineAmount &&
                   DeliveryLinePrice == details.DeliveryLinePrice &&
                   ProductName == details.ProductName;
        }

        public override int GetHashCode()
        {
            var hashCode = -1470472998;
            hashCode = hashCode * -1521134295 + DeliveryLineAmount.GetHashCode();
            hashCode = hashCode * -1521134295 + DeliveryLinePrice.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ProductName);
            return hashCode;
        }
    }
}
