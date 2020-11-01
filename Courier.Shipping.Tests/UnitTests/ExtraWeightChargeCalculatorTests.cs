using Courier.Shipping.Calculators;
using Courier.Shipping.Models;
using Courier.Shipping.Services;
using Courier.Shipping.Tests.MockRepositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Cuorier.Shipping.Tests.UnitTests
{
    [TestClass]
    public class ExtraWeightShippingCalculatorTests
    {
        private IShippingCalculator _shippingCalculator;
        private IShippingCalculator _extraWeightShippingCalculator;

        [TestInitialize]
        public void Initialize()
        {
            var parcelTypeSettingsRepository = new ParcelTypeSettingsMockRepository();
            var parcelService = new ParcelService(parcelTypeSettingsRepository);
            _shippingCalculator = new ShippingCalculator(parcelService);
            _extraWeightShippingCalculator = new ExtraWeightChargeCalculator(_shippingCalculator, parcelService);
        }

        [TestMethod]
        public void Calculate_ShouldApplyExtraCharge()
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
                    Weight = 2
                }
            };

            // act
            var result = _extraWeightShippingCalculator.Calculate(parcels);

            // assert
            Assert.AreEqual(2, result.Parcels[0].ExtraWeightCharge);
            Assert.AreEqual(5, result.Parcels[0].UnitCost);
            Assert.AreEqual(5, result.GrandTotal);
        }

        [TestMethod]
        public void Calculate_ShouldNotApplyExtraCharge()
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
                    Weight = 1
                }
            };

            // act
            var result = _extraWeightShippingCalculator.Calculate(parcels);

            // assert
            Assert.AreEqual(0, result.Parcels[0].ExtraWeightCharge);
            Assert.AreEqual(3, result.Parcels[0].UnitCost);
            Assert.AreEqual(3, result.GrandTotal);
        }
    }
}
