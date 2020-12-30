using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService
{
    public class DeliveryLineBlock
    {
        public List<DeliveryLine> DeliveryLines { get; private set; }
        public bool ExistMoreDeliveryLines { get; private set; }

        public DeliveryLineBlock(List<DeliveryLine> deliveryLines, bool existMoreDeliveryLines)
        {
            DeliveryLines = deliveryLines;
            ExistMoreDeliveryLines = existMoreDeliveryLines;
        }
    }
}
