using Courier.Shipping.Calculators;
using Courier.Shipping.Models;
using Courier.Shipping.Services;
using Courier.Shipping.Tests.MockRepositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Cuorier.Shipping.Tests.UnitTests
{
    [TestClass]
    public class SpeedyShippingCalculatorTests
    {
        private IShippingCalculator _shippingCalculator;
        private IShippingCalculator _speedyShippingCalculator;

        [TestInitialize]
        public void Initialize()
        {
            var parcelTypeSettingsRepository = new ParcelTypeSettingsMockRepository();
            var parcelService = new ParcelService(parcelTypeSettingsRepository);
            _shippingCalculator = new ShippingCalculator(parcelService);
            _speedyShippingCalculator = new SpeedyShippingCalculator(_shippingCalculator);
        }

        [TestMethod]
        public void Calculate_DoublesTheCostOfEntireOrder()
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
                    }
                }
            };

            // act
            var result = _speedyShippingCalculator.Calculate(parcels);

            // assert
            Assert.AreEqual(3 * 2, result.GrandTotal);
        }

        [TestMethod]
        public void Calculate_ShouldBeListedAsASeparateItemInOutput()
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
                    }
                }
            };

            // act
            var result = _speedyShippingCalculator.Calculate(parcels);

            // assert
            Assert.IsNotNull(result.SpeedyDeliveryCharge);
            Assert.AreEqual(3, result.SpeedyDeliveryCharge);
        }

        [TestMethod]
        public void Calculate_ShouldNotImpactThePriceOfIndividualParcel()
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
                    }
                },
                new Parcel()
                {
                    Dimension = new Dimension()
                    {
                        Length = 11,
                        Width = 12,
                        Height = 13
                    }
                }
            };

            // act
            var result = _speedyShippingCalculator.Calculate(parcels);

            // assert
            Assert.AreEqual(3, result.Parcels[0].UnitCost);
            Assert.AreEqual(8, result.Parcels[1].UnitCost);
        }
    }
}