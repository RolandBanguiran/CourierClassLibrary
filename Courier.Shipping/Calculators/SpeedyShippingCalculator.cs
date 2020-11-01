using Courier.Shipping.Models;
using System.Collections.Generic;

namespace Courier.Shipping.Calculators
{
    public class SpeedyShippingCalculator : BaseShippingCalculator
    {
        public SpeedyShippingCalculator(IShippingCalculator shippingCalculator) : base(shippingCalculator) { }

        public override ShippingCostResult Calculate(IList<Parcel> parcels)
        {
            var result = base.Calculate(parcels);

            result.SpeedyDeliveryCharge = result.GrandTotal;
            result.GrandTotal *= 2;

            return result;
        }
    }
}