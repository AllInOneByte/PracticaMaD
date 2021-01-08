using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService
{
    public class DeliveryBlock
    {
        public List<DeliveryDetails> Deliveries { get; private set; }
        public bool ExistMoreDeliveries { get; private set; }

        public DeliveryBlock(List<DeliveryDetails> deliveries, bool existMoreDeliveries)
        {
            Deliveries = deliveries;
            ExistMoreDeliveries = existMoreDeliveries;
        }
    }
}
