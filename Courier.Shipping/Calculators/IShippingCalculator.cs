using Courier.Shipping.Models;
using System.Collections.Generic;

namespace Courier.Shipping.Calculators
{
    public interface IShippingCalculator
    {
        ShippingCostResult Calculate(IList<Parcel> parcels);
    }
}