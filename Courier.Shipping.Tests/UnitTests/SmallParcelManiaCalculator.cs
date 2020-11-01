using Courier.Shipping.Calculators;
using Courier.Shipping.Models;
using Courier.Shipping.Services;
using Courier.Shipping.Tests.MockRepositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Courier.Shipping.Tests.UnitTests
{
    [TestClass]
    public class SmallParcelManiaCalculatorTests
    {
        private IShippingCalculator _shippingCalculator;
        private IShippingCalculator _extraWeightShippingCalculator;
        private IShippingCalculator _smallParcelManiaCalculator;

        [TestInitialize]
        public void Initialize()
        {
            var parcelTypeSettingsRepository = new ParcelTypeSettingsMockRepository();
            var parcelManiaTypeSettingsRepository = new ParcelManiaTypeSettingsMockRepository();
            var parcelService = new ParcelService(parcelTypeSettingsRepository);
            var parcelManiaService = new ParcelManiaService(parcelManiaTypeSettingsRepository);
            _shippingCalculator = new ShippingCalculator(parcelService);
            _extraWeightShippingCalculator = new ExtraWeightChargeCalculator(_shippingCalculator, parcelService);
            _smallParcelManiaCalculator = new SmallParcelManiaCalculator(_extraWeightShippingCalculator, parcelManiaService);
        }

        [TestMethod]
        public void Calculate_FourthCheapestSmallParcelIsFree()
        {
            // arrange
            var parcels = new List<Parcel>()
            {
                new Parcel()
                {
                    Dimension = new Dimension()
                    {
                        Length = 1,
                        Width = 2,
                        Height = 3
                    },
                    Weight = 4
                },
                // This is the cheapest
                new Parcel()
                {
                    Dimension = new Dimension()
                    {
                        Length = 1,
                        Width = 2,
                        Height = 3
                    },
                    Weight = 1
                },
                new Parcel()
                {
                    Dimension = new Dimension()
                    {
                        Length = 1,
                        Width = 2,
                        Height = 3
                    },
                    Weight = 3
                },
                new Parcel()
                {
                    Dimension = new Dimension()
                    {
                        Length = 1,
                        Width = 2,
                        Height = 3
                    },
                    Weight = 2
                }
            };

            // act
            var result = _smallParcelManiaCalculator.Calculate(parcels);

            // assert
            Assert.IsFalse(result.Parcels[0].IsFree);
            Assert.IsTrue(result.Parcels[1].IsFree);
            Assert.IsFalse(result.Parcels[2].IsFree);
            Assert.IsFalse(result.Parcels[3].IsFree);
            Assert.IsNotNull(result.Discount);
            Assert.AreEqual(3, result.Discount);
            Assert.AreEqual(9, result.Parcels[0].UnitCost);
            Assert.AreEqual(3, result.Parcels[1].UnitCost);
            Assert.AreEqual(7, result.Parcels[2].UnitCost);
            Assert.AreEqual(5, result.Parcels[3].UnitCost);
        }
    }
}
