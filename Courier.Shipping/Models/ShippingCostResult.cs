using System.Collections.Generic;

namespace Courier.Shipping.Models
{
    public class ShippingCostResult
    {
        public ShippingCostResult()
        {
            Parcels = new List<Parcel>();
        }
        public IList<Parcel> Parcels { get; set; }
        public decimal SpeedyDeliveryCharge { get; set; }
        public decimal GrandTotal { get; set; }
    }
}
