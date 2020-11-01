using Courier.Shipping.Enums;
using Courier.Shipping.Models;

namespace Courier.Shipping.Services
{
    public interface IParcelManiaService
    {
        ParcelManiaTypeSetting GetParcelManiaTypeSetting(ParcelManiaType parcelManiaType);
    }
}