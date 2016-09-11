using System.Collections.Generic;
using SmsManager.Data.Entities;

namespace SmsManager.Repositories.Interfaces
{
    public interface ICountryRepository
    {
        IEnumerable<CountryEntity> GetCountries();
    }
}