using Courier.Shipping.Data.Repositories;
using Courier.Shipping.Enums;
using Courier.Shipping.Models;
using System;
using System.Linq;

namespace Courier.Shipping.Services
{
    public class ParcelService : IParcelService
    {
        private readonly IParcelTypeSettingsRepository _parcelTypeSettingsRepository;

        public ParcelService(IParcelTypeSettingsRepository parcelTypeSettingsRepository)
        {
            _parcelTypeSettingsRepository = parcelTypeSettingsRepository;
        }

        public int GetMaxDimensionSize(Dimension dimension)
        {
            return Math.Max(dimension.Length, Math.Max(dimension.Width, dimension.Height));
        }

        public ParcelTypeSetting GetParcelTypeSetting(Dimension dimension)
        {
            var parcelSettings = _parcelTypeSettingsRepository.GetList();
            var maxDimensionSize = GetMaxDimensionSize(dimension);

            return parcelSettings.Where(a => maxDimensionSize >= a.Range.From && maxDimensionSize < a.Range.To).FirstOrDefault();
        }

        public ParcelTypeSetting GetParcelTypeSetting(ParcelType parcelType)
        {
            var parcelSettings = _parcelTypeSettingsRepository.GetList();
            return parcelSettings.Where(a => a.Type == parcelType).FirstOrDefault();
        }
    }
}
