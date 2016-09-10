using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmsManager.Data;
using SmsManager.Data.Entities;

namespace SmsManager.Repositories
{
    public interface ICountryRepository
    {
        IEnumerable<CountryEntity> GetCountries();
    }


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
