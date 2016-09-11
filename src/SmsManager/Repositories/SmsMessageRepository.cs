using System;
using System.Collections.Generic;
using System.Linq;
using SmsManager.Data;
using SmsManager.Data.Entities;
using Microsoft.EntityFrameworkCore;
using SmsManager.Repositories.Interfaces;

namespace SmsManager.Repositories
{
    public class SmsMessageRepository : ISmsMessageRepository
    {
        private readonly SmsManagerContext _context;

        public SmsMessageRepository(SmsManagerContext context)
        {
            _context = context;
        }

        public void SaveSms(SmsMessageEntity smsMessageEntity)
        {
            _context.SmsMessages.Add(smsMessageEntity);
            _context.SaveChanges();
        }

        public IEnumerable<SmsMessageEntity> GetSmsMessagesList(DateTime startDate, DateTime endDate, int skip, int take)
        {
            var messages = _context.SmsMessages.Where(x => x.DateSent >= startDate & x.DateSent < endDate.AddDays(1))
                                               .Skip(skip)
                                               .Take(take);

            return messages.Include(x => x.Country);
        }

        public IEnumerable<SmsMessageEntity> GetSmsMessagesList(DateTime startDate, DateTime endDate, IEnumerable<string> countryCodes)
        {
            IQueryable<SmsMessageEntity> messages = _context.SmsMessages.Where(x => x.DateSent >= startDate & x.DateSent < endDate.AddDays(1));

            if (!countryCodes.Any(String.IsNullOrEmpty))
            {
                messages = messages.Where(x => countryCodes.Contains(x.Country.MobileCode));
            }

            return messages.Include(x => x.Country);
        }
    }
}
