using System;
using System.Collections.Generic;
using SmsManager.Data.Entities;
using SmsManager.Models;

namespace SmsManager.Tests.Services
{
    public class BaseTest
    {
        protected List<CountryEntity> countryEntityList = new List<CountryEntity>();
        protected List<Country> countryList = new List<Country>();
        protected List<SmsMessageEntity> smsEntityList = new List<SmsMessageEntity>();
        protected List<SmsMessage> smsList = new List<SmsMessage>();
        protected List<string> countryCodes = new List<string>();

        public BaseTest()
        {
            countryEntityList.Add(new CountryEntity() { Id = 1, Name = "Germany", Code = "49", MobileCode = "262", SmsPrice = 0.055M });
            countryEntityList.Add(new CountryEntity() { Id = 1, Name = "Austria", Code = "43", MobileCode = "232", SmsPrice = 0.053M });
            countryEntityList.Add(new CountryEntity() { Id = 1, Name = "Poland", Code = "48", MobileCode = "260", SmsPrice = 0.032M });

            countryList.Add(new Country() { Id = 1, Name = "Germany", Code = "49", MobileCode = "262", SmsPrice = 0.055M });
            countryList.Add(new Country() { Id = 1, Name = "Austria", Code = "43", MobileCode = "232", SmsPrice = 0.053M });
            countryList.Add(new Country() { Id = 1, Name = "Poland", Code = "48", MobileCode = "260", SmsPrice = 0.032M });


            smsEntityList.Add(new SmsMessageEntity() { Id = 1, CountryId = 1, DateSent = DateTime.Now, From = "from", To = "to", Message = "text", Status = SmsStatus.Success });

            smsList.Add(new SmsMessage() { Id = 1, DateSent = DateTime.Now, From = "from", To = "to", Message = "text", Status = SmsStatus.Success });

            countryCodes.Add("262");
            countryCodes.Add("260");
            countryCodes.Add("230");
        }
    }
}