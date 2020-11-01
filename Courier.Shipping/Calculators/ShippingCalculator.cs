using Courier.Shipping.Models;
using Courier.Shipping.Services;
using System.Collections.Generic;

namespace Courier.Shipping.Calculators
{
    public class ShippingCalculator : IShippingCalculator
    {
        private readonly IParcelService _parcelService;

        public ShippingCalculator(IParcelService parcelService)
        {
            _parcelService = parcelService;
        }
        
        public ShippingCostResult Calculate(IList<Parcel> parcels)
        {
            var shippingCost = new ShippingCostResult();

            foreach (var parcel in parcels)
            {
                var parcelTypeSetting = _parcelService.GetParcelTypeSetting(parcel.Dimension);
                parcel.Type = parcelTypeSetting.Type;
                parcel.UnitCost = parcelTypeSetting.Cost;
                shippingCost.GrandTotal += parcel.UnitCost;
                shippingCost.Parcels.Add(parcel);
            }

            return shippingCost;
        }
    }
}
