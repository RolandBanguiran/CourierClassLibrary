using Courier.Shipping.Models;
using System.Collections.Generic;

namespace Courier.Shipping.Data.Repositories
{
    public interface IParcelManiaTypeSettingsRepository
    {
        IList<ParcelManiaTypeSetting> GetList();
    }
}