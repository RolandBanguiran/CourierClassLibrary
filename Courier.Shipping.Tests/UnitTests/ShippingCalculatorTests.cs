using Courier.Shipping.Calculators;
using Courier.Shipping.Enums;
using Courier.Shipping.Models;
using Courier.Shipping.Services;
using Courier.Shipping.Tests.MockRepositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Cuorier.Shipping.Tests.UnitTests
{
    [TestClass]
    public class ShippingCalculatorTests
    {
        private IShippingCalculator _shippingCalculator;

        [TestInitialize]
        public void Initialize()
        {
            var parcelTypeSettingsRepository = new ParcelTypeSettingsMockRepository();
            var parcelService = new ParcelService(parcelTypeSettingsRepository);
            _shippingCalculator = new ShippingCalculator(parcelService);
        }

        [TestMethod]
        public void Calculate_SmallParcel()
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
            var result = _shippingCalculator.Calculate(parcels);

            // assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Parcels);
            Assert.AreEqual(1, result.Parcels.Count);
            Assert.AreEqual(ParcelType.Small, result.Parcels[0].Type);
            Assert.AreEqual(3, result.Parcels[0].UnitCost);
            Assert.AreEqual(3, result.GrandTotal);
        }

        [TestMethod]
        public void Calculate_MediumParcel()
        {
            // arrange
            var parcels = new List<Parcel>()
            {
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
            var result = _shippingCalculator.Calculate(parcels);

            // assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Parcels);
            Assert.AreEqual(1, result.Parcels.Count);
            Assert.AreEqual(ParcelType.Medium, result.Parcels[0].Type);
            Assert.AreEqual(8, result.Parcels[0].UnitCost);
            Assert.AreEqual(8, result.GrandTotal);
        }

        [TestMethod]
        public void Calculate_LargeParcel()
        {
            // arrange
            var parcels = new List<Parcel>()
            {
                new Parcel()
                {
                    Dimension = new Dimension()
                    {
                        Length = 51,
                        Width = 52,
                        Height = 53
                    }
                }
            };

            // act
            var result = _shippingCalculator.Calculate(parcels);

            // assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Parcels);
            Assert.AreEqual(1, result.Parcels.Count);
            Assert.AreEqual(ParcelType.Large, result.Parcels[0].Type);
            Assert.AreEqual(15, result.Parcels[0].UnitCost);
            Assert.AreEqual(15, result.GrandTotal);
        }

        [TestMethod]
        public void Calculate_ExtraLargeParcel()
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
                    }
                }
            };

            // act
            var result = _shippingCalculator.Calculate(parcels);

            // assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Parcels);
            Assert.AreEqual(1, result.Parcels.Count);
            Assert.AreEqual(ParcelType.ExtraLarge, result.Parcels[0].Type);
            Assert.AreEqual(25, result.Parcels[0].UnitCost);
            Assert.AreEqual(25, result.GrandTotal);
        }

        [TestMethod]
        public void Calculate_MultipleParcels()
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
                },
                new Parcel()
                {
                    Dimension = new Dimension()
                    {
                        Length = 51,
                        Width = 52,
                        Height = 53
                    }
                },
                new Parcel()
                {
                    Dimension = new Dimension()
                    {
                        Length = 98,
                        Width = 99,
                        Height = 100
                    }
                }
            };

            // act
            var result = _shippingCalculator.Calculate(parcels);

            // assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Parcels);
            Assert.AreEqual(4, result.Parcels.Count);
            Assert.AreEqual(ParcelType.Small, result.Parcels[0].Type);
            Assert.AreEqual(ParcelType.Medium, result.Parcels[1].Type);
            Assert.AreEqual(ParcelType.Large, result.Parcels[2].Type);
            Assert.AreEqual(ParcelType.ExtraLarge, result.Parcels[3].Type);
            Assert.AreEqual(3, result.Parcels[0].UnitCost);
            Assert.AreEqual(8, result.Parcels[1].UnitCost);
            Assert.AreEqual(15, result.Parcels[2].UnitCost);
            Assert.AreEqual(25, result.Parcels[3].UnitCost);
            Assert.AreEqual(51, result.GrandTotal);
        }
    }
}
