using Courier.Shipping.Data.Repositories;
using Courier.Shipping.Enums;
using Courier.Shipping.Models;
using System;
using System.Linq;

namespace Courier.Shipping.Services
{
    public class ParcelManiaService : IParcelManiaService
    {
        private readonly IParcelManiaTypeSettingsRepository _parcelManiaTypeSettingsRepository;

        public ParcelManiaService(IParcelManiaTypeSettingsRepository parcelManiaTypeSettingsRepository)
        {
            _parcelManiaTypeSettingsRepository = parcelManiaTypeSettingsRepository;
        }

        public ParcelManiaTypeSetting GetParcelManiaTypeSetting(ParcelManiaType parcelManiaType)
        {
            var parcelManiaSettings = _parcelManiaTypeSettingsRepository.GetList();
            return parcelManiaSettings.Where(a => a.Type == parcelManiaType).FirstOrDefault();
        }
    }
}
