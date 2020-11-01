using Courier.Shipping.Models;
using System.Collections.Generic;

namespace Courier.Shipping.Calculators
{
    public abstract class BaseShippingCalculator : IShippingCalculator
    {
        private IShippingCalculator _shippingCalculator;

        public BaseShippingCalculator(IShippingCalculator shippingCalculator)
        {
            _shippingCalculator = shippingCalculator;
        }

        public virtual ShippingCostResult Calculate(IList<Parcel> parcels)
        {
            return _shippingCalculator.Calculate(parcels);
        }
    }
}
