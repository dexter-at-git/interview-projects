using System;
using System.Collections.Generic;
using AutoMapper;
using SmsManager.Data.Entities;
using SmsManager.Models;
using SmsManager.Repositories.Interfaces;
using SmsManager.Services.Interfaces;

namespace SmsManager.Services
{
    public class SmsManagerService : ISmsManagerService
    {
        private readonly ISmsMessageRepository _smsMessageRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ISmsSender _smsSender;
        private readonly IMapper _mapper;

        public SmsManagerService(ISmsMessageRepository smsMessageRepository, ICountryRepository countryRepository, ISmsSender smsSender, IMapper mapper)
        {
            _smsMessageRepository = smsMessageRepository;
            _countryRepository = countryRepository;
            _smsSender = smsSender;
            _mapper = mapper;
        }

        public SmsStatus Send(string to, string from, string message)
        {
            var smsMessage = _smsSender.SendSms(to, from, message);
            var smsMessageEntity = _mapper.Map<SmsMessageEntity>(smsMessage);
            
            _smsMessageRepository.SaveSms(smsMessageEntity);

            return smsMessage.Status;
        }

        public IEnumerable<SmsMessage> GetSmsMessages(DateTime startDate, DateTime endDate, int skip, int take)
        {
            var messageEntities = _smsMessageRepository.GetSmsMessagesList(startDate, endDate, skip, take);
            var messages = _mapper.Map<IEnumerable<SmsMessage>>(messageEntities);
            return messages;
        }

        public IEnumerable<SmsMessage> GetSmsMessages(DateTime startDate, DateTime endDate, IEnumerable<string> countryCodes)
        {
            var messageEntities = _smsMessageRepository.GetSmsMessagesList(startDate, endDate, countryCodes);
            var messages = _mapper.Map<IEnumerable<SmsMessage>>(messageEntities);
            return messages;
        }

        public IEnumerable<Country> GetCountries()
        {
            var countryEntities = _countryRepository.GetCountries();
            var countries = _mapper.Map<IEnumerable<Country>>(countryEntities);
            return countries;
        }
    }
}
