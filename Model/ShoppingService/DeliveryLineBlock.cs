using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService
{
    public class DeliveryLineBlock
    {
        public List<DeliveryLineDetails> DeliveryLines { get; private set; }
        public bool ExistMoreDeliveryLines { get; private set; }

        public DeliveryLineBlock(List<DeliveryLineDetails> deliveryLines, bool existMoreDeliveryLines)
        {
            DeliveryLines = deliveryLines;
            ExistMoreDeliveryLines = existMoreDeliveryLines;
        }
    }
}
