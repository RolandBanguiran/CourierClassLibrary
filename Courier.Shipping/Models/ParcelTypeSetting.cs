﻿using Courier.Shipping.Enums;

namespace Courier.Shipping.Models
{
    public class ParcelTypeSetting
    {
        public ParcelType Type { get; set; }
        public DimensionRange Range { get; set; }
        public int MaxWeight { get; set; }
        public decimal ChargePerExtraWeight { get; set; }
        public decimal Cost { get; set; }
    }
}
