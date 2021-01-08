using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService
{
    
    public class DeliveryDetails
    {
        public long DeliveryId { get; private set; }
        public string DeliveryDate { get; private set; }
        public decimal DeliveryPrice { get; private set; }
        public string DeliveryAddress { get; private set; }
        public long CardNumber { get; private set; }
        public string Description { get; private set; }

        public DeliveryDetails(long deliveryId, String deliveryDate, decimal deliveryPrice, 
            string deliveryAddress, long cardNumber, string description)
        {
            this.CardNumber = cardNumber;
            this.DeliveryAddress = deliveryAddress;
            this.DeliveryDate = deliveryDate;
            this.DeliveryId = deliveryId;
            this.Description = description;
            this.DeliveryPrice = deliveryPrice;
        }

        public override bool Equals(object obj)
        {
            var details = obj as DeliveryDetails;
            return details != null &&
                   DeliveryId == details.DeliveryId &&
                   DeliveryDate == details.DeliveryDate &&
                   DeliveryPrice == details.DeliveryPrice &&
                   DeliveryAddress == details.DeliveryAddress &&
                   CardNumber == details.CardNumber &&
                   Description == details.Description;
        }

        public override int GetHashCode()
        {
            var hashCode = 1007649997;
            hashCode = hashCode * -1521134295 + DeliveryId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DeliveryDate);
            hashCode = hashCode * -1521134295 + DeliveryPrice.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DeliveryAddress);
            hashCode = hashCode * -1521134295 + CardNumber.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            return hashCode;
        }
    }
}
