using Courier.Shipping.Enums;

namespace Courier.Shipping.Models
{
    public class Parcel
    {
        public ParcelType Type { get; set; }
        public Dimension Dimension { get; set; }
        public decimal UnitCost { get; set; }
    }
}
