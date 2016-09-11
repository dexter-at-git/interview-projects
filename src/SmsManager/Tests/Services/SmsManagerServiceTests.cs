using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Moq;
using SmsManager.Data.Entities;
using SmsManager.Models;
using SmsManager.Repositories.Interfaces;
using SmsManager.Services;
using SmsManager.Services.Interfaces;
using Xunit;

namespace SmsManager.Tests.Services
{
    public class SmsManagerServiceTests : BaseTest
    {
        private readonly Mock<ICountryRepository> _countryRepositoryMock = new Mock<ICountryRepository>();
        private readonly Mock<ISmsMessageRepository> _smsMessageRepositoryMock = new Mock<ISmsMessageRepository>();
        private readonly Mock<ISmsSender> _smsSenderMock = new Mock<ISmsSender>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        ISmsManagerService _smsManagerService;

        public SmsManagerServiceTests()
        {
            _countryRepositoryMock.Setup(x => x.GetCountries()).Returns(countryEntityList);
            _mapperMock.Setup(m => m.Map<IEnumerable<Country>>(It.IsAny<List<CountryEntity>>())).Returns(countryList);

            _smsMessageRepositoryMock.Setup(x => x.GetSmsMessagesList(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<int>())).Returns(smsEntityList);
            _smsMessageRepositoryMock.Setup(x => x.GetSmsMessagesList(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<IEnumerable<string>>())).Returns(smsEntityList);
            _mapperMock.Setup(m => m.Map<IEnumerable<SmsMessage>>(It.IsAny<List<SmsMessageEntity>>())).Returns(smsList);

            _smsSenderMock.Setup(x => x.SendSms(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(smsList.First());

            _smsManagerService = new SmsManagerService(_smsMessageRepositoryMock.Object, _countryRepositoryMock.Object, _smsSenderMock.Object, _mapperMock.Object);
        }
        
        [Theory]
        [InlineData("The Sender", "+4917421293388", "Hello world")]
        public void Send_ShouldReturnStatusAndCallRepository(string from, string to, string text)
        {
            var smsStatus = _smsManagerService.Send(to, from, text);
            _smsMessageRepositoryMock.Verify(x => x.SaveSms(It.IsAny<SmsMessageEntity>()));
            Assert.Equal(smsList.First().Status, smsStatus);
        }

        [Fact]
        public void GetCountries_ShouldReturnValues()
        {
            var countries = _smsManagerService.GetCountries();
            _countryRepositoryMock.Verify(x => x.GetCountries());
            Assert.Equal(countryList, countries);
        }

        [Fact]
        public void GetSmsMessages_ShouldReturnValues()
        {
            var messages = _smsManagerService.GetSmsMessages(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<int>());
            _smsMessageRepositoryMock.Verify(x => x.GetSmsMessagesList(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<int>()));
            Assert.Equal(smsList, messages);
        }

        [Fact]
        public void GetSmsMessagesForStatistics_ShouldReturnValues()
        {
            var messages = _smsManagerService.GetSmsMessages(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<IEnumerable<string>>());
            _smsMessageRepositoryMock.Verify(x => x.GetSmsMessagesList(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<IEnumerable<string>>()));
            Assert.Equal(smsList, messages);
        }
    }
}
