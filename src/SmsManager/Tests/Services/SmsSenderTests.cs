using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using SmsManager.Data.Entities;
using SmsManager.Models;
using SmsManager.Repositories.Interfaces;
using SmsManager.Services;
using SmsManager.Services.Interfaces;
using Xunit;

namespace SmsManager.Tests.Services
{
    public class SmsSenderTests : BaseTest
    {
        private readonly Mock<ICountryRepository> _countryRepositoryMock = new Mock<ICountryRepository>();
        private readonly Mock<ILogger> _loggerMock = new Mock<ILogger>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly Mock<ILoggerFactory> _loggerFactory = new Mock<ILoggerFactory>();
        private readonly ISmsSender _smsSender;

        public SmsSenderTests()
        {
            _countryRepositoryMock.Setup(x => x.GetCountries()).Returns(countryEntityList);
            _mapperMock.Setup(m => m.Map<IList<Country>>(It.IsAny<List<CountryEntity>>())).Returns(countryList);
            _loggerFactory.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns(_loggerMock.Object);

            _smsSender = new SmsSender(_countryRepositoryMock.Object, _mapperMock.Object, _loggerFactory.Object);
        }
        
        [Theory]
        [InlineData("The Sender", "+917421293388", "Hello world")]
        [InlineData("The Sender", "+7917421293388", "Hello world")]
        public void IfCountryIsUnsupported_ThrowException(string from, string to, string text)
        {
            Assert.Throws<Exception>(() => _smsSender.SendSms(from, to, text));
        }
        
        [Theory]
        [InlineData("The Sender", "+4917421293388", "Hello world")]
        public void IfCountryIsSupported_CreateMessage(string from, string to, string text)
        {
            var smsMessage = _smsSender.SendSms(to, from, text);

            Assert.NotNull(smsMessage);
            Assert.Equal(from, smsMessage.From);
            Assert.Equal(to, smsMessage.To);
            Assert.Equal(text, smsMessage.Message);
        }
    }
}
