using System;
using System.Collections.Generic;
using SmsManager.Models;

namespace SmsManager.Services.Interfaces
{
    public interface ISmsManagerService
    {
        SmsStatus Send(string to, string from, string message);
        IEnumerable<SmsMessage> GetSmsMessages(DateTime startDate, DateTime endDate, int skip, int take);
        IEnumerable<SmsMessage> GetSmsMessages(DateTime startDate, DateTime endDate, IEnumerable<string> countryCodes);
        IEnumerable<Country> GetCountries();
    }
}