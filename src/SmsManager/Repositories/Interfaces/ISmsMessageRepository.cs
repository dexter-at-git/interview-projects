using System;
using System.Collections.Generic;
using SmsManager.Data.Entities;

namespace SmsManager.Repositories.Interfaces
{
    public interface ISmsMessageRepository
    {
        void SaveSms(SmsMessageEntity smsMessageEntity);
        IEnumerable<SmsMessageEntity> GetSmsMessagesList(DateTime startDate, DateTime endDate, int skip, int take);
        IEnumerable<SmsMessageEntity> GetSmsMessagesList(DateTime startDate, DateTime endDate, IEnumerable<string> countryCodes);
    }
}