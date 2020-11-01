using Courier.Shipping.Models;
using System.Collections.Generic;

namespace Courier.Shipping.Data.Repositories
{
    public interface IParcelTypeSettingsRepository
    {
        IList<ParcelTypeSetting> GetList();
    }
}