using Courier.Shipping.Calculators;
using Courier.Shipping.Enums;
using Courier.Shipping.Models;
using Courier.Shipping.Services;
using Courier.Shipping.Tests.MockRepositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Courier.Shipping.Tests.UnitTests
{
    [TestClass]
    public class HeavyParcelCalculatorTests
    {
        private IShippingCalculator _shippingCalculator;
        private IShippingCalculator _extraWeightShippingCalculator;
        private IShippingCalculator _heavyParcelCalculator;

        [TestInitialize]
        public void Initialize()
        {
            var parcelTypeSettingsRepository = new ParcelTypeSettingsMockRepository();
            var parcelService = new ParcelService(parcelTypeSettingsRepository);
            _shippingCalculator = new ShippingCalculator(parcelService);
            _extraWeightShippingCalculator = new ExtraWeightChargeCalculator(_shippingCalculator, parcelService);
            _heavyParcelCalculator = new HeavyParcelCalculator(_extraWeightShippingCalculator, parcelService);
        }

        [TestMethod]
        public void Calculate_ShouldApplyHeavyParcelCharge()
        {
            // arrange
            var parcels = new List<Parcel>()
            {
                new Parcel()
                {
                    Dimension = new Dimension()
                    {
                        Length = 98,
                        Width = 99,
                        Height = 100
                    },
                    Weight = 50
                }
            };

            // act
            var result = _heavyParcelCalculator.Calculate(parcels);

            // assert
            Assert.AreEqual(ParcelType.HeavyParcel, result.Parcels[0].Type);
            Assert.AreEqual(50, result.Parcels[0].UnitCost);
            Assert.AreEqual(0, result.Parcels[0].ExtraWeightCharge);
            Assert.AreEqual(50, result.GrandTotal);
        }

        [TestMethod]
        public void Calculate_ShouldApplyHeavyParcelChargeAndExtraCharge()
        {
            // arrange
            var parcels = new List<Parcel>()
            {
                new Parcel()
                {
                    Dimension = new Dimension()
                    {
                        Length = 98,
                        Width = 99,
                        Height = 100
                    },
                    Weight = 51
                }
            };

            // act
            var result = _heavyParcelCalculator.Calculate(parcels);

            // assert
            Assert.AreEqual(ParcelType.HeavyParcel, result.Parcels[0].Type);
            Assert.AreEqual(51, result.Parcels[0].UnitCost);
            Assert.AreEqual(0, result.Parcels[0].ExtraWeightCharge);
            Assert.AreEqual(51, result.GrandTotal);
        }
    }
}
