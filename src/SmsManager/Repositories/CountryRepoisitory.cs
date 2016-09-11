using System.Collections.Generic;
using SmsManager.Data;
using SmsManager.Data.Entities;
using SmsManager.Repositories.Interfaces;

namespace SmsManager.Repositories
{
    public class CountryRepoisitory : ICountryRepository
    {
        private readonly SmsManagerContext _context;

        public CountryRepoisitory(SmsManagerContext context)
        {
            _context = context;
        }

        public IEnumerable<CountryEntity> GetCountries()
        {
            var countries = _context.Countries;
            return countries;
        }
    }
}
