using Courier.Shipping.Enums;
using Courier.Shipping.Models;
using Courier.Shipping.Services;
using System.Collections.Generic;
using System.Linq;

namespace Courier.Shipping.Calculators
{
    public class ExtraWeightChargeCalculator : BaseShippingCalculator
    {
        private readonly IParcelService _parcelService;

        public ExtraWeightChargeCalculator(IShippingCalculator shippingCalculator,
            IParcelService parcelService
            ) : base(shippingCalculator)
        {
            _parcelService = parcelService;
        }

        public override ShippingCostResult Calculate(IList<Parcel> parcels)
        {
            var result = base.Calculate(parcels);

            foreach (var parcel in result.Parcels)
            {
                parcel.ExtraWeightCharge = GetExtraWeightCharge(parcel.Type, parcel.Weight);
                parcel.UnitCost += parcel.ExtraWeightCharge;
            }

            result.GrandTotal = result.Parcels.Sum(a => a.UnitCost);

            return result;
        }

        private decimal GetExtraWeightCharge(ParcelType parcelType, int parcelWeight)
        {
            var parcelTypeSetting = _parcelService.GetParcelTypeSetting(parcelType);

            if (parcelWeight > parcelTypeSetting.MaxWeight)
            {
                var extraWeight = parcelWeight - parcelTypeSetting.MaxWeight;
                return extraWeight * parcelTypeSetting.ChargePerExtraWeight;
            }

            return 0;
        }
    }
}
