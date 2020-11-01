using Courier.Shipping.Enums;
using Courier.Shipping.Models;

namespace Courier.Shipping.Services
{
    public interface IParcelService
    {
        int GetMaxDimensionSize(Dimension dimension);
        ParcelTypeSetting GetParcelTypeSetting(Dimension dimension);
        ParcelTypeSetting GetParcelTypeSetting(ParcelType parcelType);
    }
}