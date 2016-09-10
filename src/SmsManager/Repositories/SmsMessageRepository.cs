using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmsManager.Data;
using SmsManager.Data.Entities;

namespace SmsManager.Repositories
{
    public interface ISmsMessageRepository
    {
        void SaveSms(SmsMessageEntity smsMessageEntity);
        IEnumerable<SmsMessageEntity> GetSmsMessagesList(DateTime startDate, DateTime endDate, int skip, int take);
    }


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
            var messages = _context.SmsMessages;
            return messages;
        }
    }
}
