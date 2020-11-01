using Courier.Shipping.Enums;

namespace Courier.Shipping.Models
{
    public class Parcel
    {
        public ParcelType Type { get; set; }
        public Dimension Dimension { get; set; }
        public int Weight { get; set; }
        public decimal ExtraWeightCharge { get; set; }
        public decimal UnitCost { get; set; }
        public bool IsFree { get; set; }
    }
}
