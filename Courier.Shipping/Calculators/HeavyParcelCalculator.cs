using Courier.Shipping.Enums;
using Courier.Shipping.Models;
using Courier.Shipping.Services;
using System.Collections.Generic;
using System.Linq;

namespace Courier.Shipping.Calculators
{
    public class HeavyParcelCalculator : BaseShippingCalculator
    {
        private readonly IParcelService _parcelService;

        public HeavyParcelCalculator(IShippingCalculator shippingCalculator,
            IParcelService parcelService
            ) : base(shippingCalculator)
        {
            _parcelService = parcelService;
        }

        public override ShippingCostResult Calculate(IList<Parcel> parcels)
        {
            var result = base.Calculate(parcels);

            var heavyParcelTypeSetting = _parcelService.GetParcelTypeSetting(ParcelType.HeavyParcel);
            
            foreach (var parcel in result.Parcels)
            {
                bool isHeavyParcel = parcel.Weight >= heavyParcelTypeSetting.MaxWeight;

                if (isHeavyParcel)
                {
                    parcel.Type = ParcelType.HeavyParcel;

                    var heavyParcelCost = heavyParcelTypeSetting.Cost + (heavyParcelTypeSetting.ChargePerExtraWeight * (parcel.Weight - heavyParcelTypeSetting.MaxWeight));

                    if (heavyParcelCost < parcel.UnitCost)
                    {
                        parcel.UnitCost = heavyParcelCost;
                        parcel.ExtraWeightCharge = 0;
                    }
                }
            }

            result.GrandTotal = result.Parcels.Sum(a => a.UnitCost);

            return result;
        }
    }
}
