using Courier.Shipping.Enums;
using Courier.Shipping.Models;
using Courier.Shipping.Services;
using System.Collections.Generic;
using System.Linq;

namespace Courier.Shipping.Calculators
{
    public class SmallParcelManiaCalculator : BaseShippingCalculator
    {
        private readonly IParcelManiaService _parcelManiaService;

        public SmallParcelManiaCalculator(IShippingCalculator shippingCalculator,
            IParcelManiaService parcelManiaService
            ) : base(shippingCalculator)
        {
            _parcelManiaService = parcelManiaService;
        }

        public override ShippingCostResult Calculate(IList<Parcel> parcels)
        {
            var result = base.Calculate(parcels);

            var parcelManiaTypeSetting = _parcelManiaService.GetParcelManiaTypeSetting(ParcelManiaType.SmallParcel);
            var smallParcels = result.Parcels.Where(a => a.Type == ParcelType.Small)
                                .OrderByDescending(a => a.UnitCost)
                                .ToList();
            int nthFree = parcelManiaTypeSetting.NthFree;

            if (smallParcels.Count >= nthFree)
            {
                int loopCounter = 0;
                foreach (var parcel in smallParcels)
                {
                    loopCounter++;

                    if (loopCounter % nthFree == 0)
                    {
                        var groupedParcels = smallParcels.Skip(loopCounter - nthFree).Take(parcelManiaTypeSetting.NthFree);
                        var cheapestParcelCost = groupedParcels.Min(a => a.UnitCost);
                        var freeParcel = groupedParcels.Where(a => a.UnitCost == cheapestParcelCost).FirstOrDefault();
                        freeParcel.IsFree = true;
                    }
                }
            }

            var discount = result.Parcels.Where(a => a.Type == ParcelType.Small && a.IsFree).Sum(a => a.UnitCost);
            result.Discount += discount;
            result.GrandTotal = result.GrandTotal - discount;

            return result;
        }
    }
}
