using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.Extensions.Logging;
using SmsManager.Models;
using SmsManager.Repositories;
using System.Linq;

namespace SmsManager.Services
{
    public class SmsSender : ISmsSender
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public SmsSender(ICountryRepository countryRepository, IMapper mapper, ILoggerFactory loggerFactory)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger("SmsSender");
        }

        public SmsMessage SendSms(string to, string from, string message)
        {
            var countryEntities = _countryRepository.GetCountries().ToList();
            var countries = _mapper.Map<IList<Country>>(countryEntities);

            var country = countries.Where(x => to.StartsWith("+" + x.Code)).FirstOrDefault();

            if (country == null)
            {
                throw new Exception("Unsupported country");
            }


            Array values = Enum.GetValues(typeof(SmsStatus));
            Random random = new Random();
            SmsStatus smsStatus = (SmsStatus)values.GetValue(random.Next(values.Length));


            SmsMessage smsMessage = new SmsMessage();
            smsMessage.To = to;
            smsMessage.From = from;
            smsMessage.Message = message;
            smsMessage.DateSent = DateTime.Now;
            smsMessage.Status = smsStatus;
            smsMessage.Country = country;

            _logger.LogInformation("Sms sent. To: {0}, From: {1}, Message: {2}, Status: {3}, Date: {4}", smsMessage.To, smsMessage.From, smsMessage.Message, smsMessage.Status, smsMessage.DateSent);


            return smsMessage;
        }
    }
}