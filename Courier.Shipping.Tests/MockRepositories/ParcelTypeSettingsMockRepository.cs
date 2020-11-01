using Courier.Shipping.Data.Repositories;
using Courier.Shipping.Enums;
using Courier.Shipping.Models;
using System;
using System.Collections.Generic;

namespace Courier.Shipping.Tests.MockRepositories
{
    public class ParcelTypeSettingsMockRepository : IParcelTypeSettingsRepository
    {
        public IList<ParcelTypeSetting> GetList()
        {
            return new List<ParcelTypeSetting>()
            {
                new ParcelTypeSetting()
                {
                    Type = ParcelType.Small,
                    Range = new DimensionRange() {
                        From = 0,
                        To = 9
                    },
                    Cost = 3,
                    MaxWeight = 1,
                    ChargePerExtraWeight = 2
                },
                new ParcelTypeSetting() 
                {
                    Type = ParcelType.Medium,
                    Range = new DimensionRange() {
                        From = 10,
                        To = 49
                    },
                    Cost = 8,
                    MaxWeight = 3,
                    ChargePerExtraWeight = 2
                },
                new ParcelTypeSetting()
                {
                    Type = ParcelType.Large,
                    Range = new DimensionRange() {
                        From = 50,
                        To = 99
                    },
                    Cost = 15,
                    MaxWeight = 6,
                    ChargePerExtraWeight = 2
                },
                new ParcelTypeSetting()
                {
                    Type = ParcelType.ExtraLarge,
                    Range = new DimensionRange() {
                        From = 100,
                        To = Int32.MaxValue
                    },
                    Cost = 25,
                    MaxWeight = 10,
                    ChargePerExtraWeight = 2
                }
            };
        }
    }
}
