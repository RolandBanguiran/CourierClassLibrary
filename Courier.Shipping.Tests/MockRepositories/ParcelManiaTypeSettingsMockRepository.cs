using Courier.Shipping.Data.Repositories;
using Courier.Shipping.Enums;
using Courier.Shipping.Models;
using System.Collections.Generic;

namespace Courier.Shipping.Tests.MockRepositories
{
    public class ParcelManiaTypeSettingsMockRepository : IParcelManiaTypeSettingsRepository
    {
        public IList<ParcelManiaTypeSetting> GetList()
        {
            return new List<ParcelManiaTypeSetting>()
            {
                new ParcelManiaTypeSetting()
                {
                    Type = ParcelManiaType.SmallParcel,
                    NthFree = 4
                },
                new ParcelManiaTypeSetting()
                {
                    Type = ParcelManiaType.MediumParcel,
                    NthFree = 3
                },
                new ParcelManiaTypeSetting()
                {
                    Type = ParcelManiaType.MixedParcel,
                    NthFree = 5
                }
            };
        }
    }
}
