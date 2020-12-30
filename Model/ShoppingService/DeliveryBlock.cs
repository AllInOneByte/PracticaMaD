using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService
{
    public class DeliveryBlock
    {
        public List<Delivery> Deliveries { get; private set; }
        public bool ExistMoreDeliveries { get; private set; }

        public DeliveryBlock(List<Delivery> deliveries, bool existMoreDeliveries)
        {
            Deliveries = deliveries;
            ExistMoreDeliveries = existMoreDeliveries;
        }
    }
}
