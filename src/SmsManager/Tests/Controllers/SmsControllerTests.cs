using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SmsManager.Controllers;
using SmsManager.Models;
using SmsManager.Services.Interfaces;
using SmsManager.Tests.Services;
using Xunit;

namespace SmsManager.Tests.Controllers
{
    public class SmsControllerTests : BaseTest
    {
        private readonly Mock<ISmsManagerService> _smsManagerServiceMock = new Mock<ISmsManagerService>();
        private SmsController _smsController;

        public SmsControllerTests()
        {
            _smsController = new SmsController(_smsManagerServiceMock.Object);
        }

        [Fact]
        public void Send_ReturnOk()
        {
            _smsManagerServiceMock.Setup(x => x.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(It.IsAny<SmsStatus>());

            var actionResult = _smsController.SendSms(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            Assert.NotNull(actionResult);
            var result = Assert.IsType<OkObjectResult>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void Send_Exception_ReturnInternalServerError()
        {
            _smsManagerServiceMock.Setup(x => x.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());

            var actionResult = _smsController.SendSms(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            Assert.NotNull(actionResult);
            var result = Assert.IsType<ObjectResult>(actionResult);
            Assert.IsType<ObjectResult>(actionResult);
            Assert.Equal(500, result.StatusCode);
            Assert.IsType<string>(result.Value);
        }

        [Fact]
        public void GetCountries_ReturnOk()
        {
            _smsManagerServiceMock.Setup(x => x.GetCountries()).Returns(countryList);

            var actionResult = _smsController.GetCountries(It.IsAny<string>());

            Assert.NotNull(actionResult);
            var result = Assert.IsType<OkObjectResult>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void GetCountries_Exception_ReturnInternalServerError()
        {
            _smsManagerServiceMock.Setup(x => x.GetCountries()).Throws(new Exception());

            var actionResult = _smsController.GetCountries(It.IsAny<string>());

            Assert.NotNull(actionResult);
            var result = Assert.IsType<ObjectResult>(actionResult);
            Assert.IsType<ObjectResult>(actionResult);
            Assert.Equal(500, result.StatusCode);
            Assert.IsType<string>(result.Value);
        }

        [Theory]
        [InlineData("", "", 10, 10)]
        [InlineData("123", "2015-03-01T11:30:20", 10, 10)]
        [InlineData("2015-03-01T11:30:20", "123", 10, 10)]
        public void Sent_InvalidInputDate_ReturnBadRequest(string dateTimeFrom, string dateTimeTo, int skip, int take)
        {
            _smsManagerServiceMock.Setup(x => x.GetSmsMessages(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<int>())).Returns(It.IsAny<IEnumerable<SmsMessage>>());

            var actionResult = _smsController.GetSentSMS(dateTimeFrom, dateTimeTo, skip, take, It.IsAny<string>());

            Assert.NotNull(actionResult);
            var result = Assert.IsType<ObjectResult>(actionResult);
            Assert.IsType<ObjectResult>(actionResult);
            Assert.Equal(400, result.StatusCode);
        }

        [Theory]
        [InlineData("2015-03-01T11:30:20", "2015-03-01T11:30:20", 10, 10)]
        public void Sent_ReturnOk(string dateTimeFrom, string dateTimeTo, int skip, int take)
        {
            _smsManagerServiceMock.Setup(x => x.GetSmsMessages(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<int>())).Returns(smsList);

            var actionResult = _smsController.GetSentSMS(dateTimeFrom, dateTimeTo, skip, take, It.IsAny<string>());

            Assert.NotNull(actionResult);
            var result = Assert.IsType<OkObjectResult>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(200, result.StatusCode);
        }

        [Theory]
        [InlineData("2015-03-01T11:30:20", "2015-03-01T11:30:20", 10, 10)]
        public void Sent_Exception_ReturnInternalServerError(string dateTimeFrom, string dateTimeTo, int skip, int take)
        {
            _smsManagerServiceMock.Setup(x => x.GetSmsMessages(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<int>())).Throws(new Exception());

            var actionResult = _smsController.GetSentSMS(dateTimeFrom, dateTimeTo, skip, take, It.IsAny<string>());

            Assert.NotNull(actionResult);
            var result = Assert.IsType<ObjectResult>(actionResult);
            Assert.IsType<ObjectResult>(actionResult);
            Assert.Equal(500, result.StatusCode);
            Assert.IsType<string>(result.Value);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("123", "2015-03-01T11:30:20")]
        [InlineData("2015-03-01T11:30:20", "123")]
        public void Statistics_InvalidInputDate_ReturnBadRequest(string dateTimeFrom, string dateTimeTo)
        {
            _smsManagerServiceMock.Setup(x => x.GetSmsMessages(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<IEnumerable<string>>())).Returns(It.IsAny<IEnumerable<SmsMessage>>());

            var actionResult = _smsController.GetStatistics(dateTimeFrom, dateTimeTo, countryCodes, It.IsAny<string>());

            Assert.NotNull(actionResult);
            var result = Assert.IsType<ObjectResult>(actionResult);
            Assert.IsType<ObjectResult>(actionResult);
            Assert.Equal(400, result.StatusCode);
        }

        [Theory]
        [InlineData("2015-03-01", "2015-03-01")]
        public void Statistics_ReturnOk(string dateTimeFrom, string dateTimeTo)
        {
            _smsManagerServiceMock.Setup(x => x.GetSmsMessages(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<IEnumerable<string>>())).Returns(smsList);

            var actionResult = _smsController.GetStatistics(dateTimeFrom, dateTimeTo, countryCodes, It.IsAny<string>());

            Assert.NotNull(actionResult);
            var result = Assert.IsType<OkObjectResult>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(200, result.StatusCode);
        }

        [Theory]
        [InlineData("2015-03-01", "2015-03-01")]
        public void Statisticst_Exception_ReturnInternalServerError(string dateTimeFrom, string dateTimeTo)
        {
            _smsManagerServiceMock.Setup(x => x.GetSmsMessages(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<IEnumerable<string>>())).Throws(new Exception());

            var actionResult = _smsController.GetStatistics(dateTimeFrom, dateTimeTo, countryCodes, It.IsAny<string>());

            Assert.NotNull(actionResult);
            var result = Assert.IsType<ObjectResult>(actionResult);
            Assert.IsType<ObjectResult>(actionResult);
            Assert.Equal(500, result.StatusCode);
            Assert.IsType<string>(result.Value);
        }
    }
}
