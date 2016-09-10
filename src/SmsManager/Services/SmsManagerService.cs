using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SmsManager.Data;
using SmsManager.Data.Entities;
using SmsManager.Models;
using SmsManager.Repositories;

namespace SmsManager.Services
{
    public interface ISmsManagerService
    {
        SmsStatus Send(string to, string from, string message);

        IEnumerable<SmsMessage> GetSmsMessages(DateTime startDate, DateTime endDate, int skip, int take);
    }

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


            SmsMessageEntity smsMessageEntity = _mapper.Map<SmsMessageEntity>(smsMessage);


            _smsMessageRepository.SaveSms(smsMessageEntity);

            return smsMessage.Status;

        }

        public IEnumerable<SmsMessage> GetSmsMessages(DateTime startDate, DateTime endDate, int skip, int take)
        {
            var messageEntities = _smsMessageRepository.GetSmsMessagesList(startDate, endDate, skip, take);
            IEnumerable<SmsMessage> messages = _mapper.Map<IEnumerable<SmsMessage>>(messageEntities);
            return messages;
        }
    }
}
